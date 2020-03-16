using System;
using UnityEngine;

public class InGameMainMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
   
}
