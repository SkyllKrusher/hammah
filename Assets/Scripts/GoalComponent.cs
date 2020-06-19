using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalComponent : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HandlePlayerNailCollision();
        }
    }

    private void HandlePlayerNailCollision()
    {
        gameController.HandleLevelComplete();
    }
}
