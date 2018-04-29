using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacles
{
    public bool canMove = false;
    private bool IsBlock = false; //是否撞击了beforetrigger
    public float Speed = 10;
    GameModel gm;

    public override void HitPlayer(Vector3 pos)
    {
        base.HitPlayer(pos);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    protected override void Awake()
    {
        base.Awake();
        gm = MVC.GetModel<GameModel>();
    }

    public void HitTrigger()
    {
        IsBlock = true;
    }

    private void Update()
    {
        if (IsBlock&&canMove&&!gm.IsPause&&gm.IsPlay)
        {
            transform.Translate(-transform.forward * Speed * Time.deltaTime);
        }
    }
}
