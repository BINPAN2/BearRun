using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//主要更改 ispause  isplay 
public class ContinueGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = false;
        gm.IsPlay = true;
    }
}
