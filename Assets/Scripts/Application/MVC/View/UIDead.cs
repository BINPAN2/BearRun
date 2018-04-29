using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    public Text txtBriberyCoin;
    public int BriberyTime = 1 ;
    public override string Name
    {
        get
        {
            return Const.V_Dead;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void OnCancleClick()
    {
        SendEvent(Const.E_ShowFinal);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        txtBriberyCoin.text = (BriberyTime * 500).ToString();
        gameObject.SetActive(true);
    }

    public void OnBriberyClick()
    {
        CoinArgs e = new CoinArgs
        {
            Coin = BriberyTime * 500
        };
        SendEvent(Const.E_BriberyClick,e);
    }

    private void Awake()
    {
        BriberyTime = 1;
    }
}
