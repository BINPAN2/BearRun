using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FinalScoreController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIBoard board = GetView<UIBoard>();
        UIDead dead = GetView<UIDead>();
        UIFinalScore finalscore = GetView<UIFinalScore>();
        board.Hide();
        dead.Hide();
        finalscore.Show();
        //更新Exp
        gm.Exp += (board.Coin*10 + board.Distance * (board.Goal + 1));
        //更新UI
        finalscore.UpdateUI(board.Distance, board.Coin, board.Goal,gm.Exp,gm.Level);
    }
}