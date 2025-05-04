using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScenesController : Controller
{
    public int LastScene = 0;
    public override void Execute(object data)
    {
        
        ScenesArgs args = (ScenesArgs)data;
        switch(args.ScenesIndex)
        {   case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Game.Instance.objectPool.Clear();
                GameModel gm =GetModel<GameModel>();
                gm.IsPause = false;
                gm.Isplay = true;
                gm.Invincible = 0;
                gm.Magnet = 0;
                gm.Multiply = 0;
                break;
        }
        LastScene = args.ScenesIndex;
    }
}
