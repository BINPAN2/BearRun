using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const{
    //事件名字
    public const string E_ExitScenes = "E_ExitScenes"; //ScenesArgs
    public const string E_EnterScenes = "E_EnterScenes";//ScenesArgs
    public const string E_StartUp = "E_StartUp";
    public const string E_EndGame = "E_EndGame";
    public const string E_PauseGame = "E_PauseGame";
    public const string E_ResumeGame = "E_ResumeGame";//暂停后，继续，播放 3 2 1 动画

    /// <summary>
    /// UI相关
    /// </summary>
    public const string E_UpdateDis = "E_UpdateDis";//DistanceArgs
    public const string E_UpdateCoin = "E_UpdateCoin";//CoinArgs
    public const string E_HitAddtime = "E_HitAddtime";
    public const string E_HitItem = "E_HitItem";//ItemArgs

    //射门
    public const string E_HitGoalTrigger = "E_HitGoalTrigger";
    public const string E_GoalBtnClick = "E_GoalBtnClick";
    //球进了
    public const string E_ShootGoal = "E_ShootGoal";

    //显示最终得分UI
    public const string E_ShowFinal = "E_ShowFinal";
    //贿赂
    public const string E_BriberyClick = "E_BriberyClick";//CoinArgs
    public const string E_ContinueGame = "E_ContinueGame";

    //买道具
    public const string E_BuyTools = "E_BuyTools";
    //view名字
    public const string V_PlayerMove = "V_PlayerMove";
    public const string V_PlayerAnim = "V_PlayerAnim";
    public const string V_Board = "V_Board";
    public const string V_Pause = "V_Pause";
    public const string V_Resume = "V_Resume";
    public const string V_Dead = "V_Dead";
    public const string V_Final = "V_Final";
    public const string V_MainMenu = "V_MainMenu";

    public const string V_BuyTools = "V_BuyTools";
    //Model名字
    public const string M_GameModel = "M_GameModel";

}

public enum InputDirection
{
    NULL,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum ItemKind
{
    InvincibleItem,
    MagnetItem,
    MultiplyItem
}
