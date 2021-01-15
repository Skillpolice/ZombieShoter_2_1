using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zombie : MonoBehaviour
{
    Player player;
    Animator animator;
    AIPath aiPath;
    AIDestinationSetter aIDestinationSetter;
    CircleCollider2D coll2D;

    public Action HealthChange = delegate { }; //delegate {} - пустое действие ,что бы не было ошибки в случае, если никто не подпишеться

    [Header("Zones UI")]
    public float attackRadius = 4f;
    public float moveRadius = 10f;
    public float saveZone = 17f;
    public int viewAngle = 90;

    [Header("Zombie")]
    public GameObject pickaupPrefab;
    public int healthZombie = 100;
    public float hitRotate;
    public int bullDamageZombie;
    float hitNexAttack;


    Vector3 startPosZombie;
    float distanceToPlayer;

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
        aiPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }


    void Start()
    {
        player = FindObjectOfType<Player>();

        startPosZombie = transform.position; //запоменаем стартовую позицию зомби
        ChangeState(ZombieState.STAND); //Делаем активный стейт

        player.OnDeath += PlayerIsDied;
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
            ChangeState(ZombieState.MOVE_TO_PLAYER);
        }
    }

    public void DistanceZombie()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

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

    private void ChangeState(ZombieState newState)
    {
        switch (newState)
        {
            case ZombieState.STAND:
                aiPath.enabled = false;
                break;

            case ZombieState.MOVE_TO_PLAYER:
                aIDestinationSetter.target = player.transform;
                aiPath.enabled = true;
                break;

            case ZombieState.ATTACK:
                aiPath.enabled = false;
                break;

            case ZombieState.RETURN:
                aiPath.transform.position = startPosZombie;
                aiPath.enabled = true;
                break;
        }
        activeState = newState;
    }

    private void DoStand()
    {
        if (player.healthPlayer > 0)
        {
            CheckMoveToPlayer();
        }
    }

    private void DoReturn()
    {
        if (player.healthPlayer > 0 && CheckMoveToPlayer())
        {
            return;
        }

        float distanseToStart = Vector3.Distance(transform.position, startPosZombie);
        if (distanseToStart <= 0.1f)
        {
            ChangeState(ZombieState.STAND);
            return;
        }
    }

    private bool CheckMoveToPlayer()
    {
        //проверяем радиус
        if (distanceToPlayer > moveRadius)
        {
            return false;
        }

        //проверяем препядствия
        Vector3 directionToPlayer = player.transform.position - transform.position; //получаем направление от зомби к игроку
        Debug.DrawRay(transform.position, directionToPlayer, Color.red); //Рисует линию от точки А до точки B

        float angle = Vector3.Angle(-transform.up, directionToPlayer);
        if (angle > viewAngle / 2)
        {
            return false;
        }


        LayerMask layerMask = LayerMask.GetMask("Walls");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, layerMask);
        if (hit.collider != null) //проверка на столкновение с преградой
        {
            //есть коллайдер
            return false;
        }

        //бежать за игроком
        ChangeState(ZombieState.MOVE_TO_PLAYER);
        return true;
    }

    private void DoMove()
    {
        if (distanceToPlayer < attackRadius)
        {
            ChangeState(ZombieState.ATTACK);
            return;
        }
        if (distanceToPlayer > saveZone)
        {
            ChangeState(ZombieState.RETURN);
            return;
        }
    }
    private void DoAttack()
    {

        if (distanceToPlayer > attackRadius)
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
        if (distanceToPlayer > attackRadius)
        {
            return;
        }
        player.UpdateHealth(bullDamageZombie);
    }
    public void UpdateHealth(int amount)
    {
        healthZombie -= amount;

        if (healthZombie <= 0)
        {
            animator.SetTrigger("Death");
            Instantiate(pickaupPrefab, transform.position * 1.03f, Quaternion.identity);
            coll2D.enabled = false;

            player.OnDeath -= PlayerIsDied;

            return;
        }
        HealthChange(); //высоз события
    }

    private void PlayerIsDied()
    {
        ChangeState(ZombieState.RETURN);
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
