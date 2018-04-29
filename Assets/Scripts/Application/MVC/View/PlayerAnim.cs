using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnim :View {

    private Animation Anim;
    private Action PlayAnim;
    public GameModel gm;

    public override string Name
    {
        get
        {
            return Const.V_PlayerAnim;
        }
    }

    private void Awake()
    {
        Anim = GetComponent<Animation>();
        PlayAnim = PlayRun;
        gm = GetModel<GameModel>();
    }

    void Update () {

        if (PlayAnim!=null)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                PlayAnim();
            }
            else
            {
                Anim.Stop();
            }

        }
		
	}

    private void PlayRun()
    {
        Anim.Play("run");
    }

    private void PlayRight()
    {
        Anim.Play("right_jump");
        if (Anim["right_jump"].normalizedTime>0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    private void PlayLeft()
    {
        Anim.Play("left_jump");
        if (Anim["left_jump"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    private void PlayRoll()
    {
        Anim.Play("roll");
        if (Anim["roll"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    private void PlayJump()
    {
        Anim.Play("jump");
        if (Anim["jump"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    private void PlayShoot()
    {
        Anim.Play("Shoot01");
        if (Anim["Shoot01"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }


    public void MassageShoot()
    {
        PlayAnim = PlayShoot;
    }


    private void AnimationManager(InputDirection Dir)
    {
        switch (Dir)
        {
            case InputDirection.NULL:
                break;
            case InputDirection.UP:
                PlayAnim = PlayJump;
                break;
            case InputDirection.DOWN:
                PlayAnim = PlayRoll;
                break;
            case InputDirection.LEFT:
                PlayAnim = PlayLeft;
                break;
            case InputDirection.RIGHT:
                PlayAnim = PlayRight;
                break;
            default:
                break;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        throw new NotImplementedException();
    }
}
