using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacles
{
    public bool canMove = false;
    private bool isBlock = true;
    public float speed = 10;
    GameModel gm;

    protected override void Awake()
    {
        base.Awake();
        gm = MVC.GetModel<GameModel>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    public void HitTrigger()
    {
        isBlock =false;
    }

    private void Update()
    {
        if (!isBlock && canMove && !gm.IsPause && gm.Isplay)
        { 
            transform.Translate(-transform.forward* speed*Time.deltaTime);
        }
    }

}
