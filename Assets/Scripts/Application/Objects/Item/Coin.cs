using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    protected Transform EffectParent;
    public float moveSpeed = 10;

    private void Awake()
    {
        EffectParent = GameObject.Find("EffectParent").transform;
    }

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
        //生成特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", EffectParent);
        go.transform.position = pos.position;

        //播放音效
        Game.Instance.sound.PlayEffect("Se_UI_JinBi");

        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
        else if (other.tag == Tag.magnetcollider)
        {
            StartCoroutine(HitMagnet(other.transform));
        }
    }

    IEnumerator HitMagnet(Transform pos)
    {
        bool isloop = true;
        while (isloop)
        {
            transform.position = Vector3.Lerp(transform.position, pos.transform.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position,pos.position)<0.05f)
            {
                isloop = false;
                HitPlayer(pos.transform);
                pos.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }
            yield return 0;
        }
    }
}
