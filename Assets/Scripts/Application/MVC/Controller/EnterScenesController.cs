using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnterScenesController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.SceneIndex)
        {
            case 1:
                Game.Instance.sound.PlayBG("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("MainMenu").GetComponent<UIMainMenu>());
                break;
            case 2:
                Game.Instance.sound.PlayBG("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("UIShop").GetComponent<UIShop>());
                break;
            case 3:
                Game.Instance.sound.PlayBG("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBuyTools").transform.Find("BuyToolsBG").GetComponent<UIBuyTools>());
                break;
            case 4:
                Game.Instance.sound.PlayBG("Bgm_ZhanDou");
                RegisterView(GameObject.FindWithTag(Tag.player).GetComponent<PlayerMove>());
                RegisterView(GameObject.FindWithTag(Tag.player).GetComponent<PlayerAnim>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIDead").GetComponent<UIDead>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());
                GameModel gm = GetModel<GameModel>();
                gm.IsPause = false;
                gm.IsPlay = true;
                break;
            default:
                break;
        }
    }
}
