using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EndGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlay = false;

        //Todo 显示游戏结束的UI
        UIDead dead = GetView<UIDead>();
        dead.Show();

    }
}