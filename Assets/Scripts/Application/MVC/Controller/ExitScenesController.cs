using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExitScenesController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.SceneIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Game.Instance.objectPool.Clear();
                break;
            default:
                break;
        }
    }
}
