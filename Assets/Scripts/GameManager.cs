using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float TurnDuration = 3f;

    public int TurnId { get; private set; } = 0;

    public bool TurnInProgress { get; private set; }
    public bool PlayerIsTargeting { get; set; }

    public IEnumerator ProcessTurn()
    {
        TurnId++;
        TurnInProgress = true;
        var turnEnd = Time.time + TurnDuration;
        while (Time.time < turnEnd)
        {
            yield return null;
        }
        TurnInProgress = false;
    }
}