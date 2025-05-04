using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    private int briberyTime = 1;

    public Text briberyTimeText;

    public override string Name
    {
        get
        {
            return Consts.V_Dead;
        }
    }

    public override void HandleEvent(string name, object data)
    {
        
    }

    public void Hide() 
    { 
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        briberyTimeText.text = (briberyTime * 500).ToString();
    }

    public void OnCancleClick()
    {
        SendEvent(Consts.E_FinalShowUI);
    }

    public void OnbriberyClick() 
    {  
        SendEvent(Consts.E_BriberyClickController, 500*briberyTime);
        briberyTime += 1;
        GameModel gm = GetModel<GameModel>();
        Debug.Log(gm.MyCoin);
    }
}
