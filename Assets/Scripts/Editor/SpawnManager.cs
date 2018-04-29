using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : EditorWindow {

    [MenuItem("Tools/AutoPatternCreate")]

    static void PatternManager()
    {
        //Debug.Log("!!!");
        GameObject SpawnManager = GameObject.Find("PatternManager");
        if (SpawnManager != null)
        {
            PatternManager PatternManager = SpawnManager.GetComponent<PatternManager>();
            if (Selection.gameObjects.Length == 1)
            {
                var items = Selection.gameObjects[0].transform.Find("Item");
                if (items != null)
                {
                    Pattern pattern = new Pattern();
                    foreach (var child in items)
                    {
                        Transform childTrans = child as Transform;
                        if (childTrans != null)
                        {
                            //当前物体的预制体
                            var prefab = PrefabUtility.GetCorrespondingObjectFromSource(childTrans.gameObject);
                            if (prefab != null)
                            {
                                PatternItem patternItem = new PatternItem
                                {
                                    PrefabName = prefab.name,
                                    pos = childTrans.transform.localPosition
                                };
                                pattern.PatternItems.Add(patternItem);
                            }
                        }
                    }
                    PatternManager.Patterns.Add(pattern);
                }
            }
        }
    }
}
