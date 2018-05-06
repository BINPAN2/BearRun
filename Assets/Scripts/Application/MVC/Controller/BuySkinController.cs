using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuySkinController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        BuySkinArgs e = data as BuySkinArgs;
        if (gm.PayCoin(e.Coin))
        {
            //把当前id加入已购买列表
            gm.BuySkin.Add(e.ID);
            foreach (var item in gm.BuySkin)
            {
                Debug.Log("买了" + item.SkinID + "号皮肤"+item.ClothID+"号衣服");
            }
            //更新UI
            shop.UpdateUI();
            shop.UpdateSkinBuyBtn(e.ID.SkinID);

            //更新GIzmo
            shop.UpdateSkinGizmo();
        }
    }
}
