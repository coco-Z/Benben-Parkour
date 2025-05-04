using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    public Text textCoin;
    public Text invincibleText;
    public Text magnetText;
    public Text multiplyText;
    GameModel gm;

    public override string Name
    {
        get
        {
            return Consts.V_BuyTools;
        }
    }

    public override void HandleEvent(string name, object data)
    {
        
    }

    public void OnPlayClick()
    {
        Game.Instance.LoadLevel(4);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }
    public void OnReturnClick()
    {
        Game.Instance.LoadLevel(1);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }

    public void BuyRandomClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        int random1 = Random.Range(1, 4);
        ItemArgs e = new()
        {
            CostMoney = 200,
            hitCount = 1
        };
        switch (random1)
        {
            case 1:
                e.kind = Consts.ItemKind.ItemInvincible;
                break;
            case 2:
                e.kind = Consts.ItemKind.ItemMagnet;
                break;
            case 3:
                e.kind = Consts.ItemKind.ItemMultiply;
                break;
            default:
                e.kind = Consts.ItemKind.ItemInvincible;
                break;
        }
        SendEvent(Consts.E_BuyItem, e);
    }

    public void BuyinvincibleClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        ItemArgs e = new()
        {
            CostMoney = 200,
            kind = Consts.ItemKind.ItemInvincible,
            hitCount = 1
        };
        SendEvent(Consts.E_BuyItem, e);
    }

    public void BuymagnetClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        ItemArgs e = new()
        {
            CostMoney = 100,
            kind = Consts.ItemKind.ItemMagnet,
            hitCount = 1
        };
        SendEvent(Consts.E_BuyItem, e);
    }

    public void BuymultiplyClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        ItemArgs e = new()
        {
            CostMoney =200,
            kind = Consts.ItemKind.ItemMultiply,
            hitCount = 1
        };
        SendEvent(Consts.E_BuyItem, e);
    }


    private void Start()
    {
        gm = GetModel<GameModel>();
        textCoin.text = gm.MyCoin.ToString();
    }
    private void Update()
    {
        textCoin.text = gm.MyCoin.ToString();
        invincibleText.text =gm.Invincible.ToString();
        magnetText.text = gm.Magnet.ToString();
        multiplyText.text = gm.Multiply.ToString();
    }
}
