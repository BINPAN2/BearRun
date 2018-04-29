using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//播放继续 游戏3 2 1 动画
public class ResumeGameController : Controller
{
    public override void Execute(object data)
    {
        UIResume resume = GetView<UIResume>();
        resume.StartCount();
    }
}