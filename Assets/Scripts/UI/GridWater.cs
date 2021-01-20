using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWater : MonoBehaviour
{
    PlayerMovement playerMove;

    public float speedMove;

    private void CheckToSpeed()
    {

        playerMove = FindObjectOfType<PlayerMovement>();
        playerMove.speed -= speedMove;
        print("Trigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckToSpeed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMove.speed += speedMove;
        }
    }



}
