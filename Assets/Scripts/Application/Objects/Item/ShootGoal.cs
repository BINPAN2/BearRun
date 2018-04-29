using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ReusableObject
{

    public Animation GoalKeeper;
    public Animation Door;
    public float speed=30;

    public GameObject net;

    private bool m_isFly = false;



    public override void OnSpawn()
    {
        
    }

    public override void OnUnSpawn()
    {
        Door.Play("QiuMen_St");
        net.SetActive(true);
        GoalKeeper.Play("standard");
        GoalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(true);
        m_isFly = false;
        StopAllCoroutines();
        GoalKeeper.transform.localPosition = Vector3.zero;
    }

    //进球了
    public void ShootAGoal(int i )
    {
        
        switch (i)
        {
            case -2:
                GoalKeeper.Play("left_flutter");
                break;
            case 0:
                GoalKeeper.Play("flutter");
                break;
            case 2:
                GoalKeeper.Play("right_flutter");
                break;
        }

        StartCoroutine(GoalkeeperSave());
    }

    IEnumerator GoalkeeperSave()
    {
        yield return new WaitForSeconds(1);
        GoalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(false);

    }

    public void HitGoalkeeper()
    {
        m_isFly = true;
        GoalKeeper.Play("fly");
    }

    public void HitDoor(int i)
    {
        //球门动画
        switch (i)
        {
            case 0:
                Door.Play("QiuMen_RR");
                break;
            case 1:
                Door.Play("QiuMen_St");
                break;
            case 2:
                Door.Play("QiuMen_LR");
                break;
        }
        //球网消失
        net.SetActive(false);
    }
    private void Update()
    {
        if (m_isFly)
        {
            GoalKeeper.transform.position += new Vector3(0, speed, speed) * Time.deltaTime;
        }
    }
}
