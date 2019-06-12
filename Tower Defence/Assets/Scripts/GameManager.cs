using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool GameEnded;

    public GameObject gameOverUI;
    public GameObject LevelCompleteUI;
	
    void Start()
    {
        GameEnded = false;

    }

	// Update is called once per frame
	void Update ()
    {
        if (GameEnded)
            return;

        if(RedPlayerStats.Lives <= 0)
        {
            EndGame();
        }

        if(Input.GetKeyDown("r"))
        {
            EndGame();
        }

	}

    void EndGame()
    {
        GameEnded = true;
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
    }

    public void Win ()
    {
        GameEnded = true;
        Debug.Log("Level Complete");
        LevelCompleteUI.SetActive(true);
    }
}

