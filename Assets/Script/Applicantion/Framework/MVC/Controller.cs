using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
    // 执行命令的抽象方法，需要子类实现具体逻辑
    public abstract void Execute(object data);

    // 获取指定类型的模型实例，并进行类型转换
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    // 获取指定类型的视图实例，并进行类型转换
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }
    // 注册视图
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    // 注册模型
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    // 注册控制器
    protected void RegisterController(string eventname, Type controllerType)
    {
        MVC.RegisterController(eventname, controllerType);
    }
}
