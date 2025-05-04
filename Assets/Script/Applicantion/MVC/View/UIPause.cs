using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Text TextScore;
    public Text TextCoin;
    public Text TextDistance;
    public override string Name
    {
        get
        {
            return Consts.V_Pause;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnResumeClick()
    {
        Hide();
        SendEvent(Consts.E_ResumeGame);
    }
    public void HomeClick()
    {
        Game.Instance.LoadLevel(1);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }
    public override void HandleEvent(string name, object data)
    {
        
    }
}
