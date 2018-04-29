using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(-0.4f, 0, -2.5f);
        transform.localScale = new Vector3(1.66f,1.66f,1.66f);
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
