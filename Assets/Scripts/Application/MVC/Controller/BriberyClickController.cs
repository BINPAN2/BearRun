using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BriberyClickController : Controller
{
    public override void Execute(object data)
    {
        CoinArgs e = data as CoinArgs;
        UIDead dead = GetView<UIDead>();
        //TODO
        /**
         * if(花钱成功)
         * dead.briberytime++
         */
        UIBoard board = GetView<UIBoard>();
        GameModel gm = GetModel<GameModel>();
        if (gm.PayCoin(e.Coin))
        {
            if (board.Times < 0.1f)
            {
                board.Times += 20;
            }
            dead.BriberyTime ++;
            dead.Hide();
            UIResume resume = GetView<UIResume>();
            resume.StartCount();
        }

    }
}
