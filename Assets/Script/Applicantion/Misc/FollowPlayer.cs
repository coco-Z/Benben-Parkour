using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform m_player;
    Vector3 m_offset;
    public float speed = 20.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_player = GameObject.FindWithTag("Player").transform;
        m_offset = transform.position - m_player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPoosition = new Vector3(m_offset.x + m_player.position.x,
            transform.position.y,  m_offset.z+m_player.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPoosition, speed * Time.deltaTime);
    }
}
