using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model 
{
    //���ֱ�ʶ
    public abstract string Name { get; }

    //�����¼�
    protected void SendEvent(string eventname,object data = null)
    {
        MVC.SendEvent(eventname, data);
    }
}
