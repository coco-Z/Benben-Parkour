using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MVC
{
    // �洢ģ�Ͷ�����ֵ�
    public static Dictionary<string,Model>Models = new Dictionary<string,Model>();
    // �洢��ͼ������ֵ�
    public static Dictionary<string,View>Views = new Dictionary<string,View>();
    // �洢�¼����������������ӳ���ϵ���ֵ�
    public static Dictionary<string,Type> CommandMap = new Dictionary<string,Type>();
    // ע��ģ�ͣ���ģ�Ͷ���洢��Models�ֵ���
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }
    // ע����ͼ������ͼ����洢��Views�ֵ���
    public static void RegisterView(View view)
    {
        //��ֹ�ظ�ע��
        if(Views.ContainsKey(view.name))
        {
            Views.Remove(view.name);
        }

        view.RegisterAttentionEvent();
        Views[view.Name] = view;
    }
    // ע������������¼����Ƽ����������ʹ洢��CommandMap�ֵ���
    public static void RegisterController(string eventname,Type controllerType)
    {
        CommandMap[eventname] = controllerType;
    }
    // ��ȡָ�����͵�ģ�Ͷ���
    public static T GetModel<T>() where T : Model
    {
        foreach(var m in Models.Values) 
        { 
            if(m is T)// �ж�ģ���Ƿ����ָ������T
            {
                return(T)m;// ���ط���������ģ�Ͷ���
            }
        }
        return null;// ���δ�ҵ�����������ģ�Ͷ��󣬷���null
    }
    // ��ȡָ�����͵���ͼ����
    public static T GetView<T>() where T : View
    {
        foreach (var v in Views.Values)
        {
            if (v is T)// �ж���ͼ�Ƿ����ָ������T
            {
                return (T)v;// ���ط�����������ͼ����
            }
        }
        return null;// ���δ�ҵ�������������ͼ���󣬷���null
    }
    // �����¼�
    public static void SendEvent(string eventName, object data = null)
    {
        // ������ִ��
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            // ͨ������ʵ����������
            Controller c = Activator.CreateInstance(t) as Controller;

            c.Execute(data);
        }
        // ��ͼ����
        foreach (var v in Views.Values)
        {
            if(v.AttentionList.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }
}
