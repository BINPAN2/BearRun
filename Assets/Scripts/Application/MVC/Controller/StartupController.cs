using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : Controller
{
    public override void Execute(object data)
    {
        //注册controller
        RegisterController(Const.E_EnterScenes,typeof(EnterScenesController));
        RegisterController(Const.E_ExitScenes, typeof(ExitScenesController));
        RegisterController(Const.E_EndGame, typeof(EndGameController));
        RegisterController(Const.E_PauseGame, typeof(PauseGameController));
        RegisterController(Const.E_ResumeGame, typeof(ResumeGameController));
        RegisterController(Const.E_HitItem, typeof(HitItemController));
        RegisterController(Const.E_ShowFinal, typeof(FinalScoreController));
        RegisterController(Const.E_ContinueGame, typeof(ContinueGameController));
        RegisterController(Const.E_BriberyClick, typeof(BriberyClickController));
        RegisterController(Const.E_BuyTools, typeof(BuyToolsController));
        RegisterController(Const.E_BuyFootball, typeof(BuyFootballController));
        RegisterController(Const.E_EquipFootball, typeof(EquipFootballController));
        RegisterController(Const.E_BuySkin, typeof(BuySkinController));
        RegisterController(Const.E_EquipSkin, typeof(EquipSkinController));
        RegisterController(Const.E_BuyCloth, typeof(BuyClothController));
        RegisterController(Const.E_EquipCloth, typeof(EquipClothController));
        //注册model
        RegisterModel(new GameModel());

        GameModel gm = GetModel<GameModel>();
        gm.Init();
        
    }
}
