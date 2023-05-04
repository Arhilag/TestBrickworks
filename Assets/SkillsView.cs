using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsView : MonoBehaviour
{
    [SerializeField] private ButtonData[] _points;
    public ButtonData[] Points => _points;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _forgetButton;
    [SerializeField] private GameObject _outline;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _forgetAllButton;

    public Action OnClickBuy;
    public Action OnClickForget;
    public Action OnClickForgetAll;
    
    private void Awake()
    {
        _buyButton.onClick.AddListener(ClickBuy);
        _forgetButton.onClick.AddListener(ClickForget);
        _forgetAllButton.onClick.AddListener(ClickForgetAll);
        foreach (var point in _points)
        {
            point.OnClickButton += OnClickButton;
        }
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(ClickBuy);
        _forgetButton.onClick.RemoveListener(ClickForget);
        _forgetAllButton.onClick.RemoveListener(ClickForgetAll);
        foreach (var point in _points)
        {
            point.OnClickButton -= OnClickButton;
        }
    }

    private void OnClickButton(ButtonData obj)
    {
        _outline.transform.position = obj.transform.position;
        if (_outline.activeSelf == false)
        {
            _outline.SetActive(true);
        }
    }

    private void ClickBuy()
    {
        OnClickBuy?.Invoke();
    }
    
    private void ClickForget()
    {
        OnClickForget?.Invoke();
    }
    
    private void ClickForgetAll()
    {
        OnClickForgetAll?.Invoke();
    }

    public void ActivateBuyButton()
    {
        _buyButton.interactable = true;
        _forgetButton.interactable = false;
    }

    public void ActivateForgetButton()
    {
        _forgetButton.interactable = true;
        _buyButton.interactable = false;
    }

    public void UpdateText(int cost)
    {
        _costText.text = $"Cost: {cost}";
    }
}
