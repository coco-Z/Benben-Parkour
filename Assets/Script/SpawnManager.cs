using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpawnManager : EditorWindow
{
    [MenuItem("Tools/Test")]// 在 Unity 菜单中添加一个选项，点击时调用 PatternSystem 方法
    static void PatternSystem()
    {
        GameObject spawnManger = GameObject.Find("PatternManager");// 在场景中查找名为 "PatternManager" 的游戏对象
        if (spawnManger != null)// 如果找到了 "PatternManager" 对象
        {
            var PetternManger = spawnManger.GetComponent<PatternManager>();// 获取 "PatternManager" 对象上的 PatternManager 组件
            if (Selection.objects.Length == 1)  // 检查当前是否选中了一个对象
            {
                var item = Selection.gameObjects[0].transform.Find("Items");// 在选中的对象中查找名为 "Items" 的子对象
                if (item != null)// 如果找到了 "Items" 子对象
                {
                    PatternManager.Pattern pattern1 = new PatternManager.Pattern(); // 创建一个新的 Pattern 对象
                    foreach (var child in item)// 遍历 "Items" 子对象的所有子对象
                    {
                        Transform childTrans = child as Transform;// 确保当前遍历的子对象是一个 Transform
                        if (childTrans != null)
                        {
                            var prefab = PrefabUtility.GetCorrespondingObjectFromSource(childTrans.gameObject);// 获取子对象的原始 Prefab
                            if (prefab != null)// 如果子对象的 Prefab 存在
                            {
                                PatternManager.PatternItem PatternItem1 = new PatternManager.PatternItem// 创建一个新的 PatternItem 对象，并设置其属性
                                {
                                    pos = childTrans.localPosition,// 设置相对位置
                                    prefabName = prefab.name,// 设置 Prefab 名称
                                };
                                pattern1.PatternItems.Add(PatternItem1); // 将 PatternItem 添加到 pattern1 的 PatternItems 列表中
                            }
                        }
                    }
                    PetternManger.Patterns.Add(pattern1);// 将创建的 Pattern 添加到 PatternManager 的 Patterns 列表中
                }
            }
        }
    }
}
