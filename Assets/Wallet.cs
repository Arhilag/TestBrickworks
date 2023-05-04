using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private WalletView _view;
    private WalletModel _model;
    public static Wallet Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _model = new WalletModel();
        _model.OnChangePoints += _view.UpdateText;
        _view.OnClick += _model.AddPoint;
    }

    private void OnDestroy()
    {
        _model.OnChangePoints -= _view.UpdateText;
        _view.OnClick -= _model.AddPoint;
    }

    public bool SpendPoints(int cost)
    {
        return _model.SpendPoints(cost);
    }

    public void ReturnPoints(int count)
    {
        _model.AddPoint(count);
    }
}
