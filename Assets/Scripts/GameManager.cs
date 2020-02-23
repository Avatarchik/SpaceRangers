using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float turnDuration = 3f;
    private int turnId = 0;
    public bool TurnInProgress { get; private set; }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !TurnInProgress)
        {
            StartCoroutine(ProcessTurn());
        }
    }

    private IEnumerator ProcessTurn()
    {
        turnId++;
        TurnInProgress = true;
        Debug.Log($"Turn {turnId} Start: {Time.time}");
        var turnEnd = Time.time + turnDuration;
        while (Time.time < turnEnd)
        {
            yield return null;
        }

        TurnInProgress = false;
        Debug.Log($"Turn {turnId} End: {Time.time}");
    }
}