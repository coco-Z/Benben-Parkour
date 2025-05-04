using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalShowUIController : Controller
{
    public override void Execute(object data)
    {
        UIDead dead = GetView<UIDead>();
        dead.Hide();
        UIFinalScore final = GetView<UIFinalScore>();
        final.Show();
        UIBoard board = GetView<UIBoard>();
        final.UpdateUI(board.Distance, board.Coin, board.Goal, 1, 0);
        GameModel gm = GetModel<GameModel>();
        gm.MyCoin = gm.MyCoin + board.Coin;
    }
}
