using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState
{
    Idle,
    Run,
    Jump,
    Slide,
    Death
}
public class PlayAnimation : MonoBehaviour {

    public static AnimationState animState;
    private PlayerMove playerMove;
    private Animation anim;
    private bool isDead = false;
	// Use this for initialization
	void Start () {
        animState = AnimationState.Idle;
        anim = GetComponentInChildren<Animation>();
        playerMove = GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (GameController .gameState)
        {
            case GameState.Menu:
                animState = AnimationState.Idle;
                break;
            case GameState.Playing:
                animState = AnimationState.Run;
                break;
            case GameState.End:
                animState = AnimationState.Death;
                break;
        }

        if (playerMove.isSlide == TouchDir.Bottom && GameController.gameState != GameState.End )
        {
            animState = AnimationState.Slide;
        }
        if (playerMove.isJump  == TouchDir.Top&& GameController.gameState != GameState.End)
        {
            animState = AnimationState.Jump;
        }

        switch (animState)
        {
            case AnimationState.Idle:
                anim.Play("Idle_1");
                break;
            case AnimationState.Jump:
                anim.Play("jump");
                break;
            case AnimationState.Run:
                anim.Play("run");
                break;
            case AnimationState.Slide:
                anim.Play("slide");
                break;
            case AnimationState.Death:
                if (!isDead)
                {
                    isDead = true;
                    anim.Play("death");
                }
                break;
        }
	}
}
