using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MVC
{
    // 存储模型对象的字典
    public static Dictionary<string,Model>Models = new Dictionary<string,Model>();
    // 存储视图对象的字典
    public static Dictionary<string,View>Views = new Dictionary<string,View>();
    // 存储事件名称与控制器类型映射关系的字典
    public static Dictionary<string,Type> CommandMap = new Dictionary<string,Type>();
    // 注册模型，将模型对象存储在Models字典中
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }
    // 注册视图，将视图对象存储在Views字典中
    public static void RegisterView(View view)
    {
        //防止重复注册
        if(Views.ContainsKey(view.name))
        {
            Views.Remove(view.name);
        }

        view.RegisterAttentionEvent();
        Views[view.Name] = view;
    }
    // 注册控制器，将事件名称及控制器类型存储在CommandMap字典中
    public static void RegisterController(string eventname,Type controllerType)
    {
        CommandMap[eventname] = controllerType;
    }
    // 获取指定类型的模型对象
    public static T GetModel<T>() where T : Model
    {
        foreach(var m in Models.Values) 
        { 
            if(m is T)// 判断模型是否符合指定类型T
            {
                return(T)m;// 返回符合条件的模型对象
            }
        }
        return null;// 如果未找到符合条件的模型对象，返回null
    }
    // 获取指定类型的视图对象
    public static T GetView<T>() where T : View
    {
        foreach (var v in Views.Values)
        {
            if (v is T)// 判断视图是否符合指定类型T
            {
                return (T)v;// 返回符合条件的视图对象
            }
        }
        return null;// 如果未找到符合条件的视图对象，返回null
    }
    // 发送事件
    public static void SendEvent(string eventName, object data = null)
    {
        // 控制器执行
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            // 通过反射实例化控制器
            Controller c = Activator.CreateInstance(t) as Controller;

            c.Execute(data);
        }
        // 视图处理
        foreach (var v in Views.Values)
        {
            if(v.AttentionList.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }
}
