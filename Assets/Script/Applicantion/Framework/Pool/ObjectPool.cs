using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    //资源目录
    public string ResourceDir = "";
    //储存不同类型subpool的字典
    Dictionary<string,SubPool> m_pools = new Dictionary<string,SubPool>();
    //取出指定名称的物体
    public GameObject Spawn(string name,Transform trans)
    {
        SubPool pool = null;
        if(!m_pools.ContainsKey(name))// 如果字典中不包含指定名称的子池子
        {
            RegieterNew(name, trans);// 调用注册新的子池子方法
        }
        pool = m_pools[name];// 获取指定名称的子池子
        return pool.Spawn();// 从子池子中取出物体
    }

    //注册一个新的子池子
    public void RegieterNew(string name, Transform trans)
    {
        string path = ResourceDir + "/" + name;// 构建资源路径

        GameObject go = Resources.Load<GameObject>(path);// 从资源路径加载对应的游戏对象

        SubPool pool = new SubPool(trans,go); // 创建新的子池子对象

        m_pools.Add(pool.Name, pool); // 将新创建的子池子对象添加到字典中
    }

    // 回收指定的游戏对象
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach(var p in m_pools.Values)// 遍历所有子池子
        {
            if (p.Contain(go))// 如果当前子池子包含指定的游戏对象
            {
                pool = p;// 将当前子池子赋值给变量 pool
                break;
            } 
        }
        if (pool != null)
        {
            pool.UnSpawn(go);// 调用该子池子的 UnSpawn 方法回收指定游戏对象
        }
    }

    // 回收所有游戏对象
    public void UnSpawnAll()
    {
        foreach(var p in m_pools.Values)// 遍历所有子池子
        {
            p.UnSpawnAll();// 调用每个子池子的 UnSpawnAll 方法回收所有游戏对象
        }
    }

    public void Clear()
    {
        m_pools.Clear();
    }
}
