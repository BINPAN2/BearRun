using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View :MonoBehaviour
{
    //名字标识 
    public abstract string Name
    {
        get;
    }

    //关注的事件
    [HideInInspector]
    public List<string> AttentionList = new List<string>();

    //注册关注的事件
    public virtual void RegisterAttentionList()
    {

    }

    //处理事件
    public abstract void HandleEvent(string eventName , object data);

    //发送事件
    protected void SendEvent(string eventName,object data=null)
    {
        MVC.SendEvent(eventName, data);
    }

    //获取Model
    protected T GetModel<T>()
        where T:Model
    {
        return MVC.GetModel<T>() as T;
    }
}

