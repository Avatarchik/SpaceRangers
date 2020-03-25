using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMainMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
   
}
