using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : Controller
{
    public override void Execute(object data)
    {
        PauseArgs args = (PauseArgs)data;
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = true;
        UIPause pause = GetView<UIPause>();
        pause.Show();
        pause.TextCoin.text = args.coin.ToString();
        pause.TextDistance.text = args.Distance.ToString();
        pause.TextScore.text = args.score.ToString();
    }
}
