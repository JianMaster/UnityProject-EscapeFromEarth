using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag (Tags .obstacle)&&PlayAnimation .animState !=AnimationState.Slide&& PlayAnimation.animState!=AnimationState.Death)
        {
                GameController.gameState = GameState.End;
        }
    }
}
