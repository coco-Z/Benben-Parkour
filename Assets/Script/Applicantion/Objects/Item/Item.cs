using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ReusableObject
{
    public float speed = 60.0f;
    public Transform effectParent;

    public override void OnSpawn()
    {
        
    }

    public override void OnUnSpawn()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void Awake()
    {
        effectParent = GameObject.Find("EffectParent").transform;
    }

    public virtual void HitPlayer(Transform pos)
    {

    }
}
