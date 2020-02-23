using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject star;
    private Vector3 target;

    private void Start()
    {
        star = GameObject.Find("Star");
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.TurnInProgress)
        {
            transform.RotateAround(star.transform.position, star.transform.forward, 5 * Time.deltaTime);
            transform.localRotation = Quaternion.identity;
        }
    }
}