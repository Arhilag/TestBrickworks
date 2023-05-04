using System;

public class SkillModel
{
    private bool _isActive;
    public bool IsActive => _isActive;
    private bool _isBuying;
    public bool IsBuying => _isBuying;
    private bool _isBase;
    public bool IsBase => _isBase;
    private int _cost;
    public int Cost => _cost;
    private int[] _connects;
    public int[] Connects => _connects;

    public Action OnActivate;
    public Action OnDeactivate;
    public Action<int[]> OnBuy;
    public Action<int[]> OnForget;
    
    public SkillModel(bool isBase, int cost, int[] connects)
    {
        _isBase = isBase;
        _cost = cost;
        _connects = connects;
    }

    public void Activate()
    {
        _isActive = true;
        OnActivate?.Invoke();
    }
    
    public void Deactivate()
    {
        if (!_isBuying && !_isBase)
        {
            _isActive = false;
            OnDeactivate?.Invoke();
        }
    }

    public void BuySkill()
    {
        _isBuying = true;
        OnBuy?.Invoke(_connects);
    }

    public void Forget()
    {
        _isBuying = false;
        OnForget?.Invoke(_connects);
    }
}
