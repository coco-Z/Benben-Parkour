using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    // 定义了一个公共的抽象属性Name，子类需要提供getName的实现
    public abstract string Name { get; }

    // 定义了一个公共的属性AttentionList，用于存储关注的事件列表
    [HideInInspector]
    public List<string>AttentionList = new List<string>();

    // 注册对特定事件关注的方法，目前该方法为空，子类需要重写该方法以实现具体逻辑
    public virtual void RegisterAttentionEvent()
    { 

    }

    // 处理事件的抽象方法，子类需要实现具体逻辑，参数为事件名和传递的数据
    public abstract void HandleEvent(string name,object data);

    // 发送事件的方法，参数为事件名和传递的数据（可选）
    protected void SendEvent(string eventname,object data = null)
    {
        MVC.SendEvent(eventname, data);
    }

    // 获取指定类型的模型实例，并进行类型转换
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }
}
