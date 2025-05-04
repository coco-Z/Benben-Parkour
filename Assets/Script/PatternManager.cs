using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternManager : MonoSingleton<PatternManager>
{
    //许多方案
    public List<Pattern> Patterns = new List<Pattern>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //一个物体
    [Serializable]
    public class PatternItem
    {
        public string prefabName;
        public Vector3 pos;
    }
    //一套方案
    [Serializable]
    public class Pattern
    {
        public List<PatternItem> PatternItems = new List<PatternItem>();
    }
}
