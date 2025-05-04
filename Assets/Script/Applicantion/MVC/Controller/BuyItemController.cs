using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = (ItemArgs)data;
        GameModel gm = GetModel<GameModel>();
        if (gm.HasEnMoney(e.CostMoney))
        {
               switch (e.kind)
                {
                    case Consts.ItemKind.ItemMagnet:
                        gm.Magnet = gm.Magnet + e.hitCount;
                        break;

                    case Consts.ItemKind.ItemMultiply:
                        gm.Multiply = gm.Multiply + e.hitCount;
                        break;

                    case Consts.ItemKind.ItemInvincible:
                        gm.Invincible = gm.Invincible + e.hitCount;
                        break;
                }
            
        }
       
    }
}
