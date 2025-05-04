using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriberyClickController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        if(gm.HasEnMoney((int)data))
        {
            UIResume resume = GetView<UIResume>();
            resume.StartCount();
            UIDead uIDead = GetView<UIDead>();
            uIDead.Hide();
            UIBoard board = GetView<UIBoard>();
            if (board.Times <= 0.01f)
            {
                board.Times += 20.0f;
            }
        }
        
        
    }
}
