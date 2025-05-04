using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpController : Controller
{
    public override void Execute(object data)
    {
        //注册所有的其他控制器
        RegisterController(Consts.E_EnterScenes, typeof(EnterScenesController));
        RegisterController(Consts.E_ExitScenes, typeof(ExitScenesController));
        RegisterController(Consts.E_EndGame, typeof(EndGameController));
        RegisterController(Consts.E_PauseGame,typeof(PauseGameController));
        RegisterController(Consts.E_ResumeGame, typeof(ResumeGameController));
        RegisterController(Consts.E_ContinueGame, typeof(ContinueGameController));
        RegisterController(Consts.E_HitItem, typeof(HitItemController));
        RegisterController(Consts.E_FinalShowUI, typeof(FinalShowUIController));
        RegisterController(Consts.E_BriberyClickController, typeof(BriberyClickController));
        RegisterController(Consts.E_BuyItem,typeof(BuyItemController));
        RegisterController(Consts.E_SaveGame, typeof(SaveGameController));
        
        //注册模型
        RegisterModel(new GameModel());

        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Init();
        gm.LoadGame();
    }
}
