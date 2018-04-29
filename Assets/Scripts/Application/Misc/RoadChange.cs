using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange :MonoBehaviour {

    private GameObject RoadNow;
    private GameObject RoadNext;
    private GameObject parent;

    private void Start()
    {
        if (parent == null)
        {
            parent = new GameObject();
            parent.transform.position = new Vector3(0, 0, 0);
            parent.name = "Road";
        }

        RoadNow = Game.Instance.objectPool.Spawn("Pattern_1", parent.transform);
        RoadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        RoadNext.transform.position = RoadNow.transform.position + new Vector3(0, 0, 160);
        AddItem(RoadNow);
        AddItem(RoadNext);
    }


    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //回收旧跑道
        if (other.gameObject.tag == Tag.road)
        {
            Game.Instance.objectPool.UnSpawn(other.gameObject);

            //生成新跑道
            SpwanNewRoad();
        }


    }

    public void SpwanNewRoad()
    {
        int i = Random.Range(1, 5);
        RoadNow = RoadNext;
        RoadNext = Game.Instance.objectPool.Spawn("Pattern_"+i.ToString(), parent.transform);
        RoadNext.transform.position = RoadNow.transform.position + new Vector3(0, 0, 160);
        AddItem(RoadNext);
    }

    //添加障碍物与物品
    public void AddItem(GameObject obj)
    {
        GameObject itemChild = obj.transform.Find("Item").gameObject;
        if (itemChild !=null)
        {
            PatternManager patternManager = PatternManager.Instance;
            if (patternManager != null && patternManager.Patterns != null &&patternManager.Patterns.Count>0)
            {
                //随机取出一个方案
                Pattern pattern = patternManager.Patterns[Random.Range(0, patternManager.Patterns.Count)];
                if (pattern!=null && pattern.PatternItems !=null && pattern.PatternItems.Count>0)
                {
                    foreach (var item in pattern.PatternItems)
                    {
                        GameObject go = Game.Instance.objectPool.Spawn(item.PrefabName,itemChild.transform);
                        go.transform.parent = itemChild.transform;
                        go.transform.localPosition = item.pos;
                    }
                }
            }
        }
    }
}
