using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static  class MVC {
    //资源
    public static Dictionary<string ,Model> Models= new Dictionary<string ,Model>();//名字--Model 
    public static Dictionary<string, View> Views = new Dictionary<string, View>();//名字--View
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();//事件名字--类型

    //注册
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        //判断不要重复注册view  
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }
        view.RegisterAttentionList();
        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName , Type ControllerType)
    {
        CommandMap[eventName] = ControllerType;
    }

    //获取
    public static T GetModel<T>()
        where T:Model
    {
        foreach (var item in Models.Values)
        {
            if (item is T)
            {
                return (T)item;
            }
        }
        return null;
    }

    public static T GetView<T>()
        where T : View
    {
        foreach (var item in Views.Values)
        {
            if (item is T)
            {
                return (T)item;
            }
        }
        return null;
    }


    //发送事件
    public static void SendEvent(string eventName,object data = null)
    {
        //Controller接收
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller ;
            c.Execute(data);
        }

        //View 接收
        foreach (var item in Views.Values)
        {
            if (item.AttentionList.Contains(eventName))
            {
                item.HandleEvent(eventName, data);
            }
        }

    }
}
