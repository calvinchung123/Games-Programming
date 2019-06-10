using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text winnerText;

    void OnEnable ()
    {
        if(RedPlayerStats.Lives < 0)
        {
            roundsText.text = RedPlayerStats.Rounds.ToString();
            winnerText.text = "You Lose!";
        }

        

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        RedPlayerStats.Rounds = 0;
       
        Enemy.starthealth = 100;
       
        Enemy.speed = 20f;
        

    }
}
