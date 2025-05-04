using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : Item
{
    public override void HitPlayer(Transform pos)
    {
        //创建特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effectParent);
        go.transform.position = pos.position;

        //播放声音
        Game.Instance.sound.PlayEffect("Se_UI_Stars");

        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);

        //Destroy(gameObject);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HitPlayer(other.transform);
            other.SendMessage("HitItem", Consts.ItemKind.ItemMultiply);
            /*other.SendMessage("HitMultiply", SendMessageOptions.RequireReceiver);*/
        }
    }
}
