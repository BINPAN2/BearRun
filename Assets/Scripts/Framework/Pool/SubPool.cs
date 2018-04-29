using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubPool
{
    //游戏对象
    List<GameObject> m_objects = new List<GameObject>();
    //对象预制体
    GameObject m_prefab;
    //子池子中预制体的名字
    public string Name
    {
        get
        {
            return m_prefab.name;
        }
    }

    //父物体（方便管理）
    Transform m_parent;

    //构造函数
    public SubPool(Transform parent , GameObject go)
    {
        m_prefab = go;
        m_parent = parent;
    }

    //取出物体
    public GameObject Spawn()
    {
        GameObject go = null;
        foreach (var item in m_objects)
        {
            if (!item.activeSelf)
            {
                go = item;
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.SetParent(m_parent);
            m_objects.Add(go);
        }
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }
    //回收物体
    public void UnSpawn(GameObject go)
    {
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收所有物体
    public void UnSpawnAll()
    {
        foreach (var item in m_objects)
        {
            if (item.activeSelf)
            {
                UnSpawn(item);
            }
        }
    }

    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
