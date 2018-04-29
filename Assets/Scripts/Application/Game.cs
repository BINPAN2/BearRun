using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : MonoSingleton<Game>
{
    //全局访问
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public Sound sound;
    [HideInInspector]
    public StaticData staticData;


    //初始化
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;
        //注册startupcontroller
        RegisterController(Const.E_StartUp, typeof(StartupController));

        //游戏启动
        SendEvent(Const.E_StartUp);
        Debug.Log("startup");

        //完成场景跳转
        Game.Instance.LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        ScenesArgs e = new ScenesArgs();
        //获取当前场景索引
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        //发送退出场景事件
        SendEvent(Const.E_ExitScenes, e);
        //发送进入新场景事件
        SceneManager.LoadScene(level,LoadSceneMode.Single);

    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("进入新场景" + level);
        ScenesArgs e = new ScenesArgs();
        //获取当前场景索引
        e.SceneIndex = level;
        //发送退出场景事件
        SendEvent(Const.E_EnterScenes, e);
    }

    protected void SendEvent(string eventName, object data=null)
    {
        MVC.SendEvent(eventName, data);
    }

    //注册Controller(在这里只为注册StartUpController)
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}
