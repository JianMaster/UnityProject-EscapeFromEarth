using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    End
}

public class GameController : MonoBehaviour {

    public static GameState gameState = GameState.Menu;
    public GameObject startImage;

    void Update () {
		if(gameState == GameState.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                gameState = GameState.Playing;
                startImage.SetActive(false);
            }
        }

        if (gameState==GameState.End )
        {

        }
	}
}
