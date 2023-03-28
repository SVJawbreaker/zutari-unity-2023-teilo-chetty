using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void loadLevel(string levelName)
    {
        if (levelName == "")
            SceneManager.LoadScene("Level_One");

        else
            SceneManager.LoadScene(levelName);
    }
}
