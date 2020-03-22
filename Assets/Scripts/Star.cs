using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Star : MonoBehaviour, IObjectData
{
    private GameManager gameManager;
    private List<GameObject> damagedObjects = new List<GameObject>();
    private int damagePerTurn = 50;
    private List<GameObject> planets;
    private String starInfo;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        planets = FindObjectsOfType<Planet>().ToArray().Select(it => it.gameObject).ToList();
        starInfo = "Planets: " + string.Join(",", planets.Select(it => it.name).ToList());
    }

    private void Update()
    {
        if (!gameManager.TurnInProgress)
        {
            damagedObjects.Clear();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.TurnInProgress && !damagedObjects.Contains(other.gameObject))
        {
            var takeDamage = other.GetComponent<ITakeDamage>();
            if (takeDamage != null)
            {
                Debug.Log($"Star dealt {damagePerTurn} damage to {other.gameObject.name}");
                takeDamage.TakeDamage(damagePerTurn);
                damagedObjects.Add(other.gameObject);
            }
        }
    }

    public string GetObjectData()
    {
        return starInfo;
    }
}
