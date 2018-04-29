using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HitItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = data as ItemArgs;
        PlayerMove Player = GetView<PlayerMove>();
        GameModel gm = GetModel<GameModel>();
        UIBoard ui = GetView<UIBoard>();

        switch (e.itemkind)
        {
            case ItemKind.InvincibleItem:
                gm.m_Invincible -= e.itemCount;
                Player.HitInvincible();
                ui.HitInvincible();
                break;
            case ItemKind.MagnetItem:
                gm.m_Magnet -= e.itemCount;
                Player.HitMagnet();
                ui.HitMagnet();
                break;
            case ItemKind.MultiplyItem:
                gm.m_Multiply -= e.itemCount;
                Player.HitDouble();
                ui.HitDouble();
                break;
            default:
                break;
        }
        ui.UpdateUI();
    }
}