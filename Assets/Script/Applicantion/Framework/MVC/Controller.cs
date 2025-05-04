using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
    // ִ������ĳ��󷽷�����Ҫ����ʵ�־����߼�
    public abstract void Execute(object data);

    // ��ȡָ�����͵�ģ��ʵ��������������ת��
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    // ��ȡָ�����͵���ͼʵ��������������ת��
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }
    // ע����ͼ
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    // ע��ģ��
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    // ע�������
    protected void RegisterController(string eventname, Type controllerType)
    {
        MVC.RegisterController(eventname, controllerType);
    }
}
