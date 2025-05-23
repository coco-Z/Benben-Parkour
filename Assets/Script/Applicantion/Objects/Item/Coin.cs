﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public float moveSpeed = 40;

    public override void HitPlayer(Transform pos)
    {
        //创建特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effectParent);
        go.transform.position = pos.position;

        //播放声音
        Game.Instance.sound.PlayEffect("Se_UI_JinBi");

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
       if (other.tag == "Player")
        {
            HitPlayer(other.transform);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
       if(other.tag == "MagnetCollider")
        {
            StartCoroutine(HitMagnet(other.transform));
        }
    }

    IEnumerator HitMagnet(Transform pos)
    {
        bool isLoop = true;
        while (isLoop)
        {
            transform.position = Vector3.Lerp(transform.position, pos.position, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position,pos.position)<0.5f)
            {
                isLoop = false;
                HitPlayer(pos);
                pos.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }
            yield return 0;
        }
    }
}
