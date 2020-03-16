using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    private GameManager gameManager;
    private DateTime dateTime;
    [SerializeField] private Text date;
    [SerializeField] private GameObject menu;
    
    private void Start()
    {
        dateTime = new DateTime(3300, 1, 1, new GregorianCalendar());
        gameManager = FindObjectOfType<GameManager>();
        date.text = dateTime.AddDays(gameManager.TurnId).ToString("yyyy MMMM dd");
    }

    public void ProcessTurnButton()
    {
        if (gameManager.TurnInProgress)
        {
            return;
        }
        StartCoroutine(gameManager.ProcessTurn());
        date.text =  dateTime.AddDays(gameManager.TurnId).ToString("yyyy MMMM dd");
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }
}
