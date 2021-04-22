using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
   
    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Time.timeScale = 1f;

        Application.Quit();
    }

    
}
