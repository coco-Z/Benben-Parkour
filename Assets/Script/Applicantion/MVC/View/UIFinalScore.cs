using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    public Text TextScore; 
    public Text TextDis;
    public Text TextCoin;
    public Text TextGoal;
    public Text TextGrade;
    public Text TextExp;
    public Slider TextSlider;

    public override string Name
    {
        get
        {
            return Consts.V_FinalScore;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void HomeClick()
    {
        Game.Instance.LoadLevel(1);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }
    public void ShopClick()
    {
        Game.Instance.LoadLevel(2);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }
    public void PlayClick()
    {
        Game.Instance.LoadLevel(4);
        Game.Instance.sound.PlayEffect("Se_UI_Button");
    }

    public void UpdateUI(int dis,int coin, int goal,int exp, int garde)
    {
        TextScore.text = ((dis+ coin+ goal)*2).ToString();
        TextDis.text = dis.ToString();
        TextCoin.text = coin.ToString();
        TextGoal.text = goal.ToString();

    }

    public override void HandleEvent(string name, object data)
    {
       
    }
}
