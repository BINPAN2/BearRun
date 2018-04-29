using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PauseGameController : Controller
{
    public override void Execute(object data)
    {
        PauseArgs e = data as PauseArgs;
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = true;
        UIPause pause = GetView<UIPause>();
        pause.txtCoins.text = e.coin.ToString();
        pause.txtDistance.text = e.distance.ToString();
        pause.txtScore.text = e.score.ToString();
        pause.Show();
    }
}