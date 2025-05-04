using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange : MonoBehaviour
{
    [HideInInspector]
    public GameObject roadNow;
    [HideInInspector]
    public GameObject roadNext;
    [HideInInspector]
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        if(parent == null)
        {
            parent = new GameObject();
            parent.transform.position = Vector3.zero;
            parent.name = "Road";
        }
        roadNow = Game.Instance.objectPool.Spawn("Pattern_1",parent.transform);
        roadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        roadNext.transform.position += new Vector3(0, 0, 160);
        AddItem(roadNow);
        AddItem(roadNext);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Road")
        {
            Game.Instance.objectPool.UnSpawn(other.gameObject);

            SpawnNewRoad();
        }
    }

    private void SpawnNewRoad()
    {
        int i = Random.Range(1, 5);

        roadNow = roadNext;
        roadNext = Game.Instance.objectPool.Spawn("Pattern_" + i, parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);
        AddItem(roadNext);
    }

    public void AddItem(GameObject obj)
    {
        var itemChild = obj.transform.Find("Items");// 在传入的 GameObject 中查找名为 "Items" 的子对象
        var PatternManager1 = PatternManager.Instance; // 获取单例模式的 PatternManager 实例
        var pattern1 = PatternManager1.Patterns[Random.Range(0,PatternManager1.Patterns.Count)];// 从 PatternManager 的 Patterns 列表中随机选择一个 Pattern
        foreach (var itemList in pattern1.PatternItems)// 遍历选中 Pattern 中的所有 PatternItems
        {
            GameObject go = Game.Instance.objectPool.Spawn(itemList.prefabName, itemChild);  // 从对象池中生成一个对象，该对象的 Prefab 名称为 itemList.prefabName，并将其设置为 itemChild 的子对象
            go.transform.parent = itemChild;// 设置生成对象的父对象为 itemChild
            go.transform.localPosition = itemList.pos;// 设置生成对象的本地位置为 itemList.pos

        }

    }

}
