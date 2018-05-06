using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View {
    public Text txtDis;
    public Text txtCoin;
    public Text txtGoal;
    public Text txtScore;
    public Text txtExp;
    public Text txtLevel;

    public Slider sliExp;


    public override string Name
    {
        get
        {
            return Const.V_Final;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    //更新UI
    public void UpdateUI(int dis,int coin,int goal,int exp,int level)
    {
        //1距离
        txtDis.text = dis.ToString();
        //2金币
        txtCoin.text = coin.ToString();
        //3进球
        txtGoal.text = goal.ToString();
        //4得分
        txtScore.text = (dis * (goal + 1)+coin*10).ToString();
        //5经验txt
        txtExp.text = exp.ToString()+"/"+(500+level*100);
        //6经验slider
        sliExp.value = (float)exp / (500 + level * 100);
        //7等级
        txtLevel.text = level.ToString()+"级";

    }

    public void OnPlayClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(4);
    }

    public void OnHomeClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(1);
    }

    public void OnShopClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(2);
    }
}
