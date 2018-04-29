using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatternManager : MonoSingleton<PatternManager> {

    public List<Pattern> Patterns = new List<Pattern>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}


[Serializable]
//每套方案里的一个物品
public class PatternItem
{
    public string PrefabName;
    public Vector3 pos;
}

[Serializable]
//一个方案
public class Pattern
{
    public List<PatternItem> PatternItems = new List<PatternItem>();
}
