using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel model = GetModel<GameModel>();
        model.SaveGame();
    }
}
