using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Text victoryText;

    public string nextLevel = "leveltwo";

    public SceneFader sceneFader;

    

    void OnEnable()
    {
        victoryText.text = "You win";
    }

    public void NextLevel()
    {
        sceneFader.FadeTo(nextLevel);
    }
}
