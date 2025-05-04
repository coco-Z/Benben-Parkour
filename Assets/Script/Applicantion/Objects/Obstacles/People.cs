using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Obstacles
{
    private bool isHit = false;
    private bool isFly = false;
    private Animation anim;
    private GameModel gm;
    public float speed = 10.0f;


    public override void HitPlayer(Vector3 pos)
    {
        //创建特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        isHit = false;
        isFly = true;
        anim.Play("fly");
        StartCoroutine(StartUnSpawn());

    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        anim.Play("run");
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        anim.transform.localPosition = Vector3.zero;
        isHit = false;
        isFly = false;
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animation>();
        gm = MVC.GetModel<GameModel>();
    }

    public void HitTrigger()
    {
        isHit = true;
    }

    private void Update()
    {
        if (isHit && gm.Isplay && !gm.IsPause)
        {
            transform.position = transform.position - new Vector3(speed,0,0)*Time.deltaTime;
        }
        if(isFly && gm.Isplay && !gm.IsPause)
        {
            transform.position = transform.position + new Vector3(0, speed, speed) * Time.deltaTime;
        }
    }

    IEnumerator StartUnSpawn()
    {
        yield return new WaitForSeconds(3);

        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);

        //Destroy(gameObject);
        
    }
}
