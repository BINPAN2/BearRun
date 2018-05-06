using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EquipSkinController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        BuySkinArgs e = data as BuySkinArgs;
        gm.TakeonSkin = e.ID;
        //更新UI
        shop.UpdateUI();
        shop.UpdateSkinBuyBtn(e.ID.SkinID);
        //更新Gizmo
        shop.UpdateSkinGizmo();
    }
}