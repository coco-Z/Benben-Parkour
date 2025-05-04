using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_instance;// 保留单例实例的静态字段

    public static T Instance { get => m_instance; }// 单例实例的只读属性

    protected virtual void Awake()
    {
        m_instance = this as T;// 在 Awake 中将当前实例赋值给静态字段，确保只有一个实例
    }
}
