using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    Player player;
    Animator animator;
    ZombieMovement movement;
    CircleCollider2D coll2D;

    [Header("Zones")]
    public float attackRadius = 4f;
    public float moveRadius = 10f;
    public float saveZone = 17f;

    [Header("Zombie")]
    public int healthZombie = 100;
    public float hitRotate;
    public int bullDamageZombie;
    float hitNexAttack;


    Vector3 startPosZombie;
    float dictanceToPlayer;

    ZombieState activeState;
    enum ZombieState //проверка состояний зомби
    {
        STAND,
        RETURN,
        MOVE_TO_PLAYER,
        ATTACK
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        coll2D = GetComponent<CircleCollider2D>();
        movement = GetComponent<ZombieMovement>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();

        startPosZombie = transform.position; //запоменаем стартовую позицию зомби

        ChangeState(ZombieState.STAND); //Делаем активный стейт
    }

    private void Update()
    {
        DistanceZombie();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            UpdateHealth(player.bullDamagePlayer);
        }
    }

    public void UpdateHealth(int amount)
    {
        healthZombie -= amount;
    
        if (healthZombie <= 0)
        {
            animator.SetTrigger("Death");
            coll2D.enabled = false;
            return;
        }
    }


    public void DistanceZombie()
    {
        dictanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (healthZombie > 0)
        {
            switch (activeState)
            {
                case ZombieState.STAND:
                    DoStand();
                    break;
                case ZombieState.MOVE_TO_PLAYER:
                    DoMove();
                    break;
                case ZombieState.ATTACK:
                    DoAttack();
                    break;
                case ZombieState.RETURN:
                    DoReturn();
                    break;
            }
        }
        else
        {
            movement.OnDisable();
            movement.enabled = false;
            return;
        }
    }

    private void ChangeState(ZombieState newState)
    {
        switch (newState)
        {
            case ZombieState.STAND:
                movement.enabled = false;
                break;

            case ZombieState.MOVE_TO_PLAYER:
                movement.enabled = true;
                break;

            case ZombieState.ATTACK:
                movement.enabled = false;
                break;

            case ZombieState.RETURN:
                movement.targetPos = startPosZombie;
                movement.enabled = true;
                break;
        }
        activeState = newState;
    }

    private void DoStand()
    {
        if (dictanceToPlayer < moveRadius)
        {
            ChangeState(ZombieState.MOVE_TO_PLAYER);
        }
    }

    private void DoReturn()
    {
        if (dictanceToPlayer < moveRadius)
        {
            ChangeState(ZombieState.MOVE_TO_PLAYER);
            return;
        }

        float distanseToStart = Vector3.Distance(transform.position, startPosZombie);
        if (distanseToStart <= 0.1f)
        {
            ChangeState(ZombieState.MOVE_TO_PLAYER);
            return;
        }
    }

    private void DoMove()
    {
        if (dictanceToPlayer < attackRadius)
        {
            ChangeState(ZombieState.ATTACK);
            return;
        }
        if (dictanceToPlayer > saveZone)
        {
            ChangeState(ZombieState.RETURN);
            return;
        }
        movement.targetPos = player.transform.position;
    }
    private void DoAttack()
    {
        if (dictanceToPlayer > attackRadius)
        {
            ChangeState(ZombieState.MOVE_TO_PLAYER);
            return;
        }

        hitNexAttack -= Time.deltaTime;
        if (hitNexAttack < 0)
        {
            animator.SetTrigger("Attack");
            hitNexAttack = hitRotate;
        }
    }

    private void DamageToPlayer()
    {
        if (dictanceToPlayer > attackRadius)
        {
            return;
        }
        player.UpdateHealth(bullDamageZombie);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, saveZone);
    }
}
