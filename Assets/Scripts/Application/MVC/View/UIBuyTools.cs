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

    public MeshRenderer ballMesh;

    public SkinnedMeshRenderer SkinMeshRender;

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
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
    }

    public void OnMagnetClick(int i =300)
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.MagnetItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnInvincibleClick(int i = 500)
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.InvincibleItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnMultiplyClick(int i = 100)
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        BuyToolsArgs e = new BuyToolsArgs
        {
            kind = ItemKind.MultiplyItem,
            Coin = i
        };

        SendEvent(Const.E_BuyTools, e);
    }

    public void OnRandomClick(int i = 300)
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
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
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(4);
    }

    public void OnReturnClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(gm.lastsecenIndex);
    }
}
