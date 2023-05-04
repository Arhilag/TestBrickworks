using System;
using UnityEngine;

public class SkillsPresenter : MonoBehaviour
{
    [SerializeField] private SkillList _configList;
    [SerializeField] private SkillsView _skillsView;
    private SkillModel[] _skillModels;
    private SkillModel _activeModel;

    private void Awake()
    {
        var count = _configList.SkillConfigs.Length;
        if (count > _skillsView.Points.Length)
        {
            count = _skillsView.Points.Length;
        }

        _skillModels = new SkillModel[count];
        
        for (int i = 0; i < count; i++)
        {
            int[] con = new int[_configList.SkillConfigs[i].SkillsConnection.Length];
            for (int j = 0; j < con.Length; j++)
            {
                con[j] = Array.IndexOf(_configList.SkillConfigs, _configList.SkillConfigs[i].SkillsConnection[j]);
            }
            _skillModels[i] = new SkillModel(_configList.SkillConfigs[i].IsBase, _configList.SkillConfigs[i].Cost, con);
            
            _skillsView.Points[i].SetName(_configList.SkillConfigs[i].Name);
            _skillModels[i].OnActivate += _skillsView.Points[i].Activate;
            _skillModels[i].OnDeactivate += _skillsView.Points[i].Deactivate;
            _skillModels[i].OnBuy += _skillsView.Points[i].SwitchColors;
            _skillModels[i].OnForget += _skillsView.Points[i].SwitchColors;
            _skillModels[i].OnBuy += ActivateConnects;
            _skillModels[i].OnForget += OnForget;
            _skillsView.Points[i].OnClickButton += OnChangeSkill;
        }

        _skillsView.OnClickBuy += OnClickBuy;
        _skillsView.OnClickForget += OnClickForget;
        _skillsView.OnClickForgetAll += ForgetAll;
        CheckBase();
    }

    private void Start()
    {
        _skillModels[0].BuySkill();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _skillModels.Length; i++)
        {
            _skillModels[i].OnActivate -= _skillsView.Points[i].Activate;
            _skillModels[i].OnDeactivate -= _skillsView.Points[i].Deactivate;
            _skillModels[i].OnBuy -= _skillsView.Points[i].SwitchColors;
            _skillModels[i].OnForget -= _skillsView.Points[i].SwitchColors;
            _skillModels[i].OnBuy -= ActivateConnects;
            _skillModels[i].OnForget -= OnForget;
            _skillsView.Points[i].OnClickButton -= OnChangeSkill;
        }
        _skillsView.OnClickBuy -= OnClickBuy;
        _skillsView.OnClickForget -= OnClickForget;
        _skillsView.OnClickForgetAll -= ForgetAll;
    }

    private void OnClickForget()
    {
        if (CheckToCanForget(_activeModel))
        {
            Wallet.Instance.ReturnPoints(_activeModel.Cost);
            _activeModel.Forget();
        }
    }

    private void OnChangeSkill(ButtonData buttonData)
    {
        for (int i = 0; i < _skillsView.Points.Length; i++)
        {
            if (_skillsView.Points[i] == buttonData)
            {
                _activeModel = _skillModels[i];
                _skillsView.UpdateText(_activeModel.Cost);
                if (_activeModel.IsBuying)
                {
                    _skillsView.ActivateForgetButton();
                }
                else if (_activeModel.IsActive)
                {
                    _skillsView.ActivateBuyButton();
                }
                return;
            }
        }
    }

    private void OnClickBuy()
    {
        if (Wallet.Instance.SpendPoints(_activeModel.Cost))
        {
            _activeModel.BuySkill();
            _skillsView.ActivateForgetButton();
        }
    }

    private void ActivateConnects(int[] obj)
    {
        foreach (var num in obj)
        {
            _skillModels[num].Activate();
        }
    }

    private void CheckBase()
    {
        foreach (var model in _skillModels)
        {
            if (model.IsBase)
            {
                model.Activate();
            }
        }
    }
    
    private bool CheckToCanForget(SkillModel model)
    {
        int allLineBase = model.Connects.Length;

        foreach (var connect in model.Connects)
        {
            if (!_skillModels[connect].IsBuying)
            {
                allLineBase--;
                continue;
            }
            if (_skillModels[connect].IsBase)
            {
                allLineBase--;
                continue;
            }
            if (CheckToCanForget(model, _skillModels[connect]))
            {
                allLineBase--;
            }
        }

        if (allLineBase == 0)
        {
            return true;
        }
        
        return false;
    }
    
    private bool CheckToCanForget(SkillModel sender, SkillModel model)
    {
        if (model.Connects.Length <= 1)
        {
            return false;
        }
        foreach (var connect in model.Connects)
        {
            if (_skillModels[connect] == sender)
            {
                continue;
            }
            if (!_skillModels[connect].IsBuying)
            {
                continue;
            }
            if (_skillModels[connect].IsBase)
            {
                return true;
            }

            if (CheckToCanForget(model, _skillModels[connect]))
            {
                return true;
            }
        }

        return false;
    }
    
    private void OnForget(int[] obj)
    {
        _skillsView.ActivateBuyButton();
        foreach (var num in obj)
        {
            if (!CheckBuyingInConnections(_skillModels[num].Connects))
            {
                _skillModels[num].Deactivate();
            }
        }
    }

    private bool CheckBuyingInConnections(int[] obj)
    {
        foreach (var num in obj)
        {
            if (_skillModels[num].IsBuying)
            {
                return true;
            }
        }
        return false;
    }

    private void ForgetAll()
    {
        for (int i = _skillModels.Length-1; i > 0; i--)
        {
            if (_skillModels[i].IsBuying)
            {
                Wallet.Instance.ReturnPoints(_skillModels[i].Cost);
                _skillModels[i].Forget();
            }
        }
    }
}
