using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model 
{
    //名字标识
    public abstract string Name { get; }

    //发送事件
    protected void SendEvent(string eventname,object data = null)
    {
        MVC.SendEvent(eventname, data);
    }
}
