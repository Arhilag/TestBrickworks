using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointText;
    [SerializeField] private Button _addPointButton;

    public Action OnClick;

    private void Awake()
    {
        _addPointButton.onClick.AddListener(ClickButton);
    }

    private void OnDestroy()
    {
        _addPointButton.onClick.RemoveListener(ClickButton);
    }

    private void ClickButton()
    {
        OnClick?.Invoke();
    }

    public void UpdateText(int newPoints)
    {
        _pointText.text = $"Points: {newPoints}";
    }
}
