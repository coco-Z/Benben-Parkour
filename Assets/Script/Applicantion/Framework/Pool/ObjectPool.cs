using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    //��ԴĿ¼
    public string ResourceDir = "";
    //���治ͬ����subpool���ֵ�
    Dictionary<string,SubPool> m_pools = new Dictionary<string,SubPool>();
    //ȡ��ָ�����Ƶ�����
    public GameObject Spawn(string name,Transform trans)
    {
        SubPool pool = null;
        if(!m_pools.ContainsKey(name))// ����ֵ��в�����ָ�����Ƶ��ӳ���
        {
            RegieterNew(name, trans);// ����ע���µ��ӳ��ӷ���
        }
        pool = m_pools[name];// ��ȡָ�����Ƶ��ӳ���
        return pool.Spawn();// ���ӳ�����ȡ������
    }

    //ע��һ���µ��ӳ���
    public void RegieterNew(string name, Transform trans)
    {
        string path = ResourceDir + "/" + name;// ������Դ·��

        GameObject go = Resources.Load<GameObject>(path);// ����Դ·�����ض�Ӧ����Ϸ����

        SubPool pool = new SubPool(trans,go); // �����µ��ӳ��Ӷ���

        m_pools.Add(pool.Name, pool); // ���´������ӳ��Ӷ�����ӵ��ֵ���
    }

    // ����ָ������Ϸ����
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach(var p in m_pools.Values)// ���������ӳ���
        {
            if (p.Contain(go))// �����ǰ�ӳ��Ӱ���ָ������Ϸ����
            {
                pool = p;// ����ǰ�ӳ��Ӹ�ֵ������ pool
                break;
            } 
        }
        if (pool != null)
        {
            pool.UnSpawn(go);// ���ø��ӳ��ӵ� UnSpawn ��������ָ����Ϸ����
        }
    }

    // ����������Ϸ����
    public void UnSpawnAll()
    {
        foreach(var p in m_pools.Values)// ���������ӳ���
        {
            p.UnSpawnAll();// ����ÿ���ӳ��ӵ� UnSpawnAll ��������������Ϸ����
        }
    }

    public void Clear()
    {
        m_pools.Clear();
    }
}
