using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    public GameModel gm;

    public Text txtCoin;
    public Text txtGizmoMultiply;
    public Text txtGizmoMagnet;
    public Text txtGizmoInvincible;


    public override string Name
    {
        get
        {
            return Const.V_BuyTools;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        gm = GetModel<GameModel>();
        txtCoin.text = gm.Coin.ToString();
        ShowOrHide(gm.Invincible, txtGizmoInvincible);
        ShowOrHide(gm.m_Magnet, txtGizmoMagnet);
        ShowOrHide(gm.Multiply, txtGizmoMultiply);
    }

    public void OnMagnetClick(int i =300)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.MagnetItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnInvincibleClick(int i = 500)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.InvincibleItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnMultiplyClick(int i = 100)
    {
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.MultiplyItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnRandomClick(int i = 300)
    {
        int t = Random.Range(0, 3);
        switch (t)
        {
            case 0:
                OnInvincibleClick(i);
                break;
            case 1:
                OnMagnetClick(i);
                break;
            case 2:
                OnMultiplyClick(i);
                break;
            default:
                break;
        }
    }

    public void ShowOrHide(int i, Text txt)
    {
        if (i > 0)
        {
            txt.transform.parent.gameObject.SetActive(true);
            txt.text = i.ToString();
        }
        else
        {
            txt.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnPlayClick()
    {
        Game.Instance.LoadLevel(4);
    }

}
