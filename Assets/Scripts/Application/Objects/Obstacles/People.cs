using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Obstacles
{
    private bool IsBlock = false;
    private bool IsFly = false;
    public float Speed = 10;
    Animation Anim;
    GameModel gm;

    public override void HitPlayer(Vector3 pos)
    {
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", EffectParent);
        go.transform.position = pos;
        IsBlock = false;
        IsFly = true;
        Anim.Play("fly");
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        Anim.Play("run");
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        Anim.transform.localPosition= Vector3.zero;
        IsBlock = false;
        IsFly = false;
    }

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponentInChildren<Animation>();
        gm = MVC.GetModel<GameModel>();
    }


    public void HitTrigger()
    {
        IsBlock = true;
    }

    private void Update()
    {
        if (IsBlock && !gm.IsPause && gm.IsPlay)
        {
            transform.position -= new Vector3(Speed, 0, 0) * Time.deltaTime;
        }
        if (IsFly && !gm.IsPause && gm.IsPlay)
        {
            transform.position += new Vector3(0, Speed, Speed) * Time.deltaTime;
        }

    }

}
