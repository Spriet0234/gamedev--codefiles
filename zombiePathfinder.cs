using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePathfinder : MonoBehaviour
{
    private MovementController2D movementController; 
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        movementController = GetComponent<MovementController2D>();
        if (movementController == null)
        {
            Debug.LogError("ZombiePathfinder requires a MovementController2D component on the same GameObject.");
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Make sure your player is tagged correctly.");
        }
    }

    void Update()
    {
        // Ensure we have a player and movement controller reference before attempting to move
        if (player != null && movementController != null)
        {
            // Continuously try to move towards the player
            movementController.GetMoveCommand(player.transform.position);
        }
    }
}
