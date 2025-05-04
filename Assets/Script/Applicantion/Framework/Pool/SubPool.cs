using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    // 存储一组 GameObject 对象
    [HideInInspector]
    List<GameObject> m_objects = new List<GameObject>();

    // 存储一个 GameObject 预设对象
    GameObject m_prefab;

    // 返回预设对象的名称
    public string Name
    {
        get { return m_prefab.name; }
    }

    // 存储父物体的位置
    Transform m_Parent;

    // 构造函数，接受父物体位置和预设对象作为参数
    public SubPool(Transform parent,GameObject go)
    {
        m_Parent = parent;
        m_prefab = go;
    }

    // 生成对象的方法
    public GameObject Spawn()
    {
        GameObject go = null;
        // 检查是否存在未使用的对象
        foreach (var obj in m_objects)
        {
            if(!obj.activeSelf)
            {
                go = obj;
                break; // 找到一个未使用的对象后立即退出循环
            }
        }
        // 如果不存在未使用的对象，则实例化一个新的对象并添加到集合中
        if (go == null) 
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_Parent;
            m_objects.Add(go);
        }
        // 设置对象为激活状态，并调用其 OnSpawn 方法（如果存在）
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    // 回收对象的方法
    public void UnSpawn(GameObject go) 
    {
        // 检查集合中是否包含该对象，如果是，则将其设置为非激活状态，并调用其 OnUnSpawn 方法（如果存在）
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    // 回收所有对象的方法
    public void UnSpawnAll()
    {
        // 遍历集合中的所有对象，将激活状态的对象都回收
        foreach (var obj in m_objects)
        {
            if(obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }

    // 检查集合中是否包含该对象
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }

}
