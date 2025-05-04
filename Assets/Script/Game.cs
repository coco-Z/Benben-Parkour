using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// 需要以下组件的引用
[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]

public class Game : MonoSingleton<Game> 
{
    // 隐藏在Inspector面板中的字段，用于访问全局的对象池、音效和静态数据
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public Sound sound;
    [HideInInspector]
    public StaticData staticData;

    public bool save = false;
    void Start()
    {
        // 确保游戏对象在场景加载时不被销毁
        DontDestroyOnLoad(gameObject);

        // 获取全局对象池、音效和静态数据的单例实例
        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;

        //注册开始控制器
        RegisterController(Consts.E_StartUp,typeof(StartUpController));

        //游戏启动发送消息
        SendEvent(Consts.E_StartUp);

        Invoke("Load", 0.5f);

    }

    private void Update()
    {
        if(!save)
        {
            save = true;
            SendEvent(Consts.E_SaveGame);
            Invoke("SaveGamebool",5.0f);
        }
    }

    public void SaveGamebool()
    {
        save = false;
    }
    public void Load()
    {
        //跳转场景
        Game.Instance.LoadLevel(1);
    }
    public void LoadLevel(int level)
    {
        ScenesArgs args = new()
        {
            ScenesIndex = SceneManager.GetActiveScene().buildIndex
        };

        SendEvent(Consts.E_ExitScenes, args); // 发送退出场景事件，参数为当前场景的索引

        // 加载指定索引的场景，加载模式为单一加载
        SceneManager.LoadScene(level, LoadSceneMode.Single);

    }


    private void OnLevelWasLoaded(int level)
    {
        ScenesArgs args = new()
        {
            ScenesIndex = level
        };

        SendEvent(Consts.E_EnterScenes, args);// 发送进入场景事件，参数为加载的场景索引
    }


    // 发送事件的方法，参数为事件名和传递的数据（可选）
    protected void SendEvent(string eventname, object data = null)
    {
        MVC.SendEvent(eventname, data);
    }

    // 注册控制器
    protected void RegisterController(string eventname, Type controllerType)
    {
        MVC.RegisterController(eventname, controllerType);
    }
}
