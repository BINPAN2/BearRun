using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EquipFootballController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        BuyFootballArgs e = data as BuyFootballArgs;
        gm.TakeonFootball = e.Index;
        shop.UpdateUI();
        shop.UpdateFootballBuyBtn(e.Index);
        //更新Gizmo
        shop.UpdateFootBallGizmo();
    }
}