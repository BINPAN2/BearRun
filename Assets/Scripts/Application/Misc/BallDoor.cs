﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.ball)
        {
            other.transform.parent.parent.SendMessage("HitBallDoor");
            gameObject.transform.parent.parent.SendMessage("ShootAGoal",(int)other.transform.position.x);
        }
    }
}
