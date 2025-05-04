using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    // �洢һ�� GameObject ����
    [HideInInspector]
    List<GameObject> m_objects = new List<GameObject>();

    // �洢һ�� GameObject Ԥ�����
    GameObject m_prefab;

    // ����Ԥ����������
    public string Name
    {
        get { return m_prefab.name; }
    }

    // �洢�������λ��
    Transform m_Parent;

    // ���캯�������ܸ�����λ�ú�Ԥ�������Ϊ����
    public SubPool(Transform parent,GameObject go)
    {
        m_Parent = parent;
        m_prefab = go;
    }

    // ���ɶ���ķ���
    public GameObject Spawn()
    {
        GameObject go = null;
        // ����Ƿ����δʹ�õĶ���
        foreach (var obj in m_objects)
        {
            if(!obj.activeSelf)
            {
                go = obj;
                break; // �ҵ�һ��δʹ�õĶ���������˳�ѭ��
            }
        }
        // ���������δʹ�õĶ�����ʵ����һ���µĶ�����ӵ�������
        if (go == null) 
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_Parent;
            m_objects.Add(go);
        }
        // ���ö���Ϊ����״̬���������� OnSpawn ������������ڣ�
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    // ���ն���ķ���
    public void UnSpawn(GameObject go) 
    {
        // ��鼯�����Ƿ�����ö�������ǣ���������Ϊ�Ǽ���״̬���������� OnUnSpawn ������������ڣ�
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    // �������ж���ķ���
    public void UnSpawnAll()
    {
        // ���������е����ж��󣬽�����״̬�Ķ��󶼻���
        foreach (var obj in m_objects)
        {
            if(obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }

    // ��鼯�����Ƿ�����ö���
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }

}
