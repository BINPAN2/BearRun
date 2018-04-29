using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BuyToolsController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIBuyTools tools = GetView<UIBuyTools>();

        BuyToolsArgs e = data as BuyToolsArgs;
        switch (e.kind)
        {
            case ItemKind.InvincibleItem:
                if (gm.PayCoin(e.Coin))
                {
                    gm.Invincible++;
                }
                break;
            case ItemKind.MagnetItem:
                if (gm.PayCoin(e.Coin))
                {
                    gm.Magnet++;
                }
                break;
            case ItemKind.MultiplyItem:
                if (gm.PayCoin(e.Coin))
                {
                    gm.Multiply++;
                }
                break;
            default:
                break;
        }
        tools.Init();
    }
}