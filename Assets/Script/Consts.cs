using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    //事件名字
    public const string E_SaveGame = "E_SaveGame";
    public const string E_ExitScenes = "E_ExitScenes";
    public const string E_EnterScenes = "E_EnterScenes";
    public const string E_StartUp = "E_StartUp";
    public const string E_EndGame = "E_EndGame";
    public const string E_ContinueGame = "E_ContinueGame";
    public const string E_HitItem = "E_HitItem";
    public const string E_HitGoalTrigger = "E_HitGoalTrigger";
    public const string E_ClickGoalButton = "E_ClickGoalButton";
    public const string E_FinalShowUI = "E_FinalShowUI";
    public const string E_BriberyClickController = "E_BriberyClickController";
    public const string E_BuyItem = "E_BuyItem";

    public const string E_PauseGame = "E_PauseGame";
    public const string E_ResumeGame = "E_ResumeGame";
    //UI
    public const string E_UpdateDis = "E_UpdateDis";
    public const string E_UpdateCoin = "E_UpdateCoin";
    public const string E_UpdateGoal = "E_UpdateGoal";
    public const string E_ADDTime = "E_ADDTime";


    //model名字
    public const string M_GameModel = "M_GameModel";
    //view名字
    public const string V_PlayerMove = "V_PlayerMove";
    public const string V_PlayerAnim = "V_PlayerAnim";
    public const string V_Board = "V_Board";
    public const string V_Pause = "V_Pause";
    public const string V_Resume = "V_Resume";
    public const string V_Dead = "V_Dead";
    public const string V_FinalScore = "V_FinalScore";
    public const string V_MainMenu = "V_MainMenu";
    public const string V_UIShop = "V_UIShop";
    public const string V_BuyTools = "V_BuyTools";


    public enum InputDirection
    {
        Null,
        Right,
        Left,
        Up,
        Down,
    }

    public enum ItemKind
    {
        ItemMagnet,
        ItemMultiply,
        ItemInvincible

    }

}
