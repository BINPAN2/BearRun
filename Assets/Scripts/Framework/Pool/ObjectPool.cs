using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool> {

    //资源目录
    public string ResourceDir = "";
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    //取出物体
    public GameObject Spawn(string name ,Transform trans)
    {
        SubPool pool = null;
        if (!m_pools.ContainsKey(name))
        {
            RegisterNew(name,trans);
        }
        pool = m_pools[name];
        return pool.Spawn();
    }

    //回收物体
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach (var item in m_pools.Values)
        {
            if (item.Contain(go))
            {
                pool = item;
                break;
            }
        }
        pool.UnSpawn(go);
    }

    //回收所有物体
    public void UnSpawnAll()
    {
        foreach (var item in m_pools.Values)
        {
            item.UnSpawnAll();
        }
    }

    //新建一个池子
    void RegisterNew(string pathname,Transform trans)
    {   
        //资源目录
        string path = ResourceDir + "/" + pathname;
        //生成预制体
        GameObject go = Resources.Load<GameObject>(path);
        //新建一个池子
        SubPool pool = new SubPool(trans, go);
        m_pools.Add(pool.Name,pool);
    }

    public void Clear()
    {
        m_pools.Clear();
    }

}
