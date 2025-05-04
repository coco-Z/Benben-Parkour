using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ReusableObject
{
    public Animation goalKeeper;
    public bool isFly = false;

    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {
        goalKeeper.Play("Deceleration");
        isFly = false;
        goalKeeper.gameObject.transform.localPosition = Vector3.zero;

    }

    public void HitGoalKeeper()
    {
        isFly = true;
        goalKeeper.Play("fly");
    }
    private void Update()
    {
        if (isFly)
        {
            goalKeeper.gameObject.transform.position += new Vector3(0, 10, 10) * Time.deltaTime; 
        }
    }
    public void GetBall()
    {
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject,0.5f);
    }
}
