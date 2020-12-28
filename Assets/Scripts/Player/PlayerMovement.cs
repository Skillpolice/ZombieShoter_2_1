using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Player player;

    [Header("Speed Player")]
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update() // Движение персонажа
    {
        if (player.healthPlayer > 0)
        {
            Move();
            Rotate();
        }
        else
        {
            return;
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(inputX, inputY);

        if (direction.magnitude > 1) //если длинна вектора > 1, нормалезуем дистанцию 
        {
            direction = direction.normalized; //direction.magnitude - длина вектора
        }
        rb.velocity = direction * speed;
        animator.SetFloat("Walk", direction.magnitude);
    }

    public void Rotate()
    {
        Vector3 playerPos = transform.position;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = playerPos - mouseWorldPos;

        direction.z = 0;
        transform.up = direction;
    }
}
