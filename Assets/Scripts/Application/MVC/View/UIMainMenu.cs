﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    public override string Name
    {
        get
        {
           return Const.V_MainMenu;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
}
