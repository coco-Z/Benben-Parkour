using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{
    public Image img;
    public Sprite[] sprites;

    public override string Name
    {
        get
        {
            return Consts.V_Resume;
        }
    }

    public void StartCount()
    {
        Show();
        StartCoroutine(StartCountCor());
    }

    IEnumerator StartCountCor()
    {
        int i = 3;
        while(i>0)
        {
            img.sprite = sprites[i-1];
            i--;
            yield return new WaitForSeconds(1);
            if(i<=0)
            {
                break;
            }
        }
        Hide();
        SendEvent(Consts.E_ContinueGame);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void HandleEvent(string name, object data)
    {
        
    }
}
