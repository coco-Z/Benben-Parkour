using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    [Header("material")]
    public SkinnedMeshRenderer playerSkml;
    public Material material1;
    public Material material2;
    public Material material3;

    public override string Name
    {
        get
        {
            return Consts.V_MainMenu;
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
    public void OnShopClick()
    {
        Game.Instance.LoadLevel(2);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }

    public void Start() 
    {
        Game.Instance.sound.PlayBG("Bgm_JieMian");
        GameModel gm = GetModel<GameModel>();
        if(gm.clothe1fit)
        {
            playerSkml.material = material1;
        }
        else if(gm.clothe2fit)
        {
            playerSkml.material = material2;
        }
        else if(gm.clothe3fit)
        {
            playerSkml.material = material3;
        }   
    }
    
}
