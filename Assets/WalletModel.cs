using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletModel
{
    private int _points;
    public Action<int> OnChangePoints;
    
    public void AddPoint()
    {
        _points++;
        OnChangePoints?.Invoke(_points);
    }
    
    public void AddPoint(int count)
    {
        _points += count;
        OnChangePoints?.Invoke(_points);
    }
    
    public bool SpendPoints(int cost)
    {
        if (_points >= cost)
        {
            _points -= cost;
            OnChangePoints?.Invoke(_points);
            return true;
        }
        return false;
    }
}
