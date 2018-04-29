﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : Item
{

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    public override void HitPlayer(Transform pos)
    {
        //播放音效
        Game.Instance.sound.PlayEffect("Se_UI_Whist");

        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.player)
        {
            HitPlayer(other.transform);
            //other.SendMessage("HitInvincible", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.InvincibleItem);
        }
    }
}
