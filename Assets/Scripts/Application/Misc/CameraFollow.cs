using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform m_player;
    private Vector3 offset;
    public float speed = 20;


    private void Awake()
    {
        m_player = GameObject.FindWithTag(Tag.player).transform;
        offset = transform.position - m_player.position;

    }
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, m_player.position + offset, speed * Time.deltaTime);
	}
}
