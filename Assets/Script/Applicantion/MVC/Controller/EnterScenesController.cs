using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScenesController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.ScenesIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2: 
                break;
            case 3:
                break;
            case 4:
                RegisterView(GameObject.FindWithTag("Player").GetComponent<PlayerMove>());
                RegisterView(GameObject.FindWithTag("Player").GetComponent<PlayerAnim>());
                RegisterView(GameObject.FindWithTag("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.FindWithTag("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                RegisterView(GameObject.FindWithTag("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                RegisterView(GameObject.FindWithTag("Canvas").transform.Find("UIDead").GetComponent<UIDead>());
                RegisterView(GameObject.FindWithTag("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());
                break;
            default:
                break;
        }
    }
}
