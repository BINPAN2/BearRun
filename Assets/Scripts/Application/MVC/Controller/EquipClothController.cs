using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EquipClothController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        BuySkinArgs e = data as BuySkinArgs;
        gm.TakeonSkin = e.ID;
        //更新UI
        shop.UpdateUI();
        shop.UpdateClothBuyBtn(e.ID.ClothID);

        //更新GIzmo
        shop.UpdateClothGizmo();
    }
}