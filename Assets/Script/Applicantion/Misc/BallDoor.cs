using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Ball")
        {
            other.transform.parent.SendMessage("HitBallDoor", SendMessageOptions.RequireReceiver);
            transform.parent.parent.SendMessage("GetBall", SendMessageOptions.RequireReceiver);
        }
    }
}
