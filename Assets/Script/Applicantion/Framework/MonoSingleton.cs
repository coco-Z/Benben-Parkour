using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_instance;// ��������ʵ���ľ�̬�ֶ�

    public static T Instance { get => m_instance; }// ����ʵ����ֻ������

    protected virtual void Awake()
    {
        m_instance = this as T;// �� Awake �н���ǰʵ����ֵ����̬�ֶΣ�ȷ��ֻ��һ��ʵ��
    }
}
