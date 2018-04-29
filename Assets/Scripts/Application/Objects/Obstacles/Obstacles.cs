using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : ReusableObject
{
    protected Transform EffectParent;

    protected virtual void Awake()
    {
        EffectParent = GameObject.Find("EffectParent").transform;
    }

    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {

    }

    public virtual void HitPlayer(Vector3 pos)
    {
        //生成特效
        GameObject go =  Game.Instance.objectPool.Spawn("FX_ZhuangJi",EffectParent);
        go.transform.position = pos;

        //回收障碍物
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }
}
