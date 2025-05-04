using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    [Header("Coin")]
    public Text textCoin;
    [Header("material")]
    public SkinnedMeshRenderer playerSkml;
    public Material material1;
    public Material material2;
    public Material material3;
    [Header("clotheButton")]
    public Button clothe1fitButton;
    public Button clothe2fitButton;
    public Button clothe3fitButton;
    public Button clothe2Buy;
    public Button clothe3Buy;
    GameModel gm;
    public override string Name
    {
        get
        {
            return Consts.V_UIShop;
        }
    }

    public override void HandleEvent(string name, object data)
    {
       
    }

    public void OnPlayClick()
    {
        Game.Instance.LoadLevel(3);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }
    public void OnReturnClick()
    {
        Game.Instance.LoadLevel(1);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }

    private void Start()
    {
        gm = GetModel<GameModel>();
        TClick1();
    }
    private void Update()
    {
        textCoin.text = gm.MyCoin.ToString();
    }

    public void TClick1()
    {
        AllButtonfalse();
        playerSkml.material = material1;
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if(!gm.clothe1fit)
        {
            clothe1fitButton.gameObject.SetActive(true);
        }
        
    }
    public void TClick2()
    {
        AllButtonfalse();
        playerSkml.material = material2;
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if (!gm.clothe2fit&&gm.clothe2)
        {
            clothe2fitButton.gameObject.SetActive(true);
        }
        else if(!gm.clothe2fit)
        {
            clothe2Buy.gameObject.SetActive(true);
        }
    }
    public void TClick3()
    {
        AllButtonfalse();
        playerSkml.material = material3;
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if (!gm.clothe3fit && gm.clothe3)
        {
            clothe3fitButton.gameObject.SetActive(true);
        }
        else if (!gm.clothe3fit)
        {
            clothe3Buy.gameObject.SetActive(true);
        }
    }
    public void AllButtonfalse()
    {
        clothe1fitButton.gameObject.SetActive(false);
        clothe2fitButton.gameObject.SetActive(false);
        clothe3fitButton.gameObject.SetActive(false);
        clothe2Buy.gameObject.SetActive(false);
        clothe3Buy.gameObject.SetActive(false);
    }

    public void clothe2BuyClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if(gm.HasEnMoney(600))
        {
            AllButtonfalse();
            clothe2fitButton.gameObject.SetActive(true);
            gm.clothe2 = true;
        }
        

    }

    public void clothe3BuyClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if(gm.HasEnMoney(1200))
        {
            AllButtonfalse();
            clothe3fitButton.gameObject.SetActive(true);
            gm.clothe3 = true;
        }    
    }

    public void clothe1fitClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        gm.clothe1fit = true;
        gm.clothe2fit = false;
        gm.clothe3fit = false;
        AllButtonfalse();
    }
    public void clothe2fitClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        gm.clothe1fit = false;
        gm.clothe2fit = true;
        gm.clothe3fit = false;
        AllButtonfalse();
    }
    public void clothe3fitClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        gm.clothe1fit = false;
        gm.clothe2fit = false;
        gm.clothe3fit = true;
        AllButtonfalse();
    }
}
