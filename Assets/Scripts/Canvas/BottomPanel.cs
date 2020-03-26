using System;
using System.Globalization;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Canvas
{
    public class BottomPanel : MonoBehaviour
    {
        private GameManager gameManager;
        private DateTime dateTime;
        private PlayerShip playerPlayerShip;
        [SerializeField] private Text date;
        [SerializeField] private Text freeSpace;
        [SerializeField] private Text money;
        [SerializeField] private GameObject menu;
    
        private void Start()
        {
            playerPlayerShip = GameObject.Find("Player").GetComponentInChildren<PlayerShip>();
            dateTime = new DateTime(3300, 1, 1, new GregorianCalendar());
            gameManager = FindObjectOfType<GameManager>();
            date.text = dateTime.AddDays(gameManager.TurnId).ToString("yyyy MMMM dd");
            freeSpace.text = playerPlayerShip.GetFreeSpace().ToString();
            money.text = playerPlayerShip.Money.ToString();
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
            menu.SetActive(true);
        }
    }
}
