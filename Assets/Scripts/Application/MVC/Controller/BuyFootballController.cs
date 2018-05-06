using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuyFootballController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        BuyFootballArgs e = data as BuyFootballArgs;
        if (gm.PayCoin(e.Coin))
        {
            //把当前id加入已购买列表
            gm.BuyFootball.Add(e.Index);
            foreach (var item in gm.BuyFootball)
            {
                Debug.Log("买了" + item + "号");
            }
            //更新UI
            shop.UpdateUI();
            shop.UpdateFootballBuyBtn(e.Index);
            //更新GIzmo
            shop.UpdateFootBallGizmo();
        }
    }
}
