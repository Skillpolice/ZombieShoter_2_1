using Pathfinding;
using UnityEngine;

public class GridWater : MonoBehaviour
{
    PlayerMovement playerMove;

    public float speedMove;

    private void Awake()
    {

    }

    private void CheckToSpeed()
    {

        playerMove = FindObjectOfType<PlayerMovement>();
        playerMove.speed -= speedMove;
        print("Player speed +1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckToSpeed();
        }

        if (collision.gameObject.CompareTag("Zombie"))
        {
            AIPath aIpath = collision.GetComponent<AIPath>();
            aIpath.maxSpeed -= speedMove;
            print("Speed zombie -1");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMove.speed += speedMove;

            print("Player speed -1");


        }
        if (collision.gameObject.CompareTag("Zombie"))
        {
            AIPath aIpath = collision.GetComponent<AIPath>();
            aIpath.maxSpeed += speedMove;

            print("Speed zombie -1");
        }
    }



}
