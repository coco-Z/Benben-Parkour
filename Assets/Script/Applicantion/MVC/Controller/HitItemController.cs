using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = (ItemArgs)data;
        PlayerMove player = GetView<PlayerMove>();
        GameModel gm = GetModel<GameModel>();
        UIBoard ui = GetView<UIBoard>();
        if (!gm.IsPause&&gm.Isplay)
        {
            switch (e.kind)
            {
                case Consts.ItemKind.ItemMagnet:
                    player.HitMagnet();
                    ui.HitMagnet();
                    gm.Magnet = gm.Magnet - e.hitCount;
                    break;

                case Consts.ItemKind.ItemMultiply:
                    ui.HitMultiply();
                    player.HitMultiply();
                    gm.Multiply = gm.Multiply - e.hitCount;
                    break;

                case Consts.ItemKind.ItemInvincible:
                    player.HitInvincible();
                    ui.HitInvincible();
                    gm.Invincible = gm.Invincible - e.hitCount;
                    break;
            }
            ui.UpdateUI();
        }
        
    }
}
