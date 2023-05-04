using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    [SerializeField] private Button _button;
    public Button Button => _button;
    [SerializeField] private TMP_Text _buttonText;
    public TMP_Text ButtonText => _buttonText;
    [SerializeField] private Image _image;
    [SerializeField] private Color _buyingColor;
    private Color _normalColor;

    public Action<ButtonData> OnClickButton;

    public void SetName(string name)
    {
        _buttonText.text = name;
    }

    public void Active(bool isActive)
    {
        _button.interactable = isActive;
    }
    
    public void Activate()
    {
        Active(true);
    }
    
    public void Deactivate()
    {
        Active(false);
    }

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
        _normalColor = _image.color;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        OnClickButton?.Invoke(this);
    }

    public void SwitchColors(int[] xyi)
    {
        _image.color = _image.color == _normalColor ? _buyingColor : _normalColor;
    }
}
