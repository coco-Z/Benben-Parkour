using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Consts;

public class PlayerAnim : View
{
    Animation anim;
    Action PlayAnim;
    GameModel gm;

    public override string Name
    {
        get { return Consts.V_PlayerAnim; }
    }

    public override void HandleEvent(string name, object data)
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animation>();
        PlayAnim = PlayRun;
        gm =GetModel<GameModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.IsPause && gm.Isplay)
        {
            PlayAnim();
        }
        else
        {
            anim.Stop();
        }
    }

    public void PlayShootMessage()
    {
        PlayAnim = PlayShoot;
    }

    public void AnimManager(InputDirection direction)
    {
        switch (direction)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Left:
                PlayAnim = PlayLeft;
                break;
            case InputDirection.Right:
                PlayAnim = PlayRight;
                break;
            case InputDirection.Up:
                PlayAnim = PlayJump;
                break;
            case InputDirection.Down:
                PlayAnim = PlayRoll;
                break;
        }
    }

    void PlayRun()
    {
        anim.Play("run");
    }

    void PlayLeft()
    {
        anim.Play("left_jump");
        if (anim["left_jump"].normalizedTime>0.95)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayRight()
    {
        anim.Play("right_jump");
        if (anim["right_jump"].normalizedTime > 0.95)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayRoll()
    {
        anim.Play("roll");
        if (anim["roll"].normalizedTime > 0.95)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayJump()
    {
        anim.Play("jump");
        if (anim["jump"].normalizedTime > 0.95)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayShoot()
    {
        anim.Play("Shoot01");
        if (anim["Shoot01"].normalizedTime > 0.95)
        {
            PlayAnim = PlayRun;
        }
    }

}
