using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBurrel : MonoBehaviour
{
    Animator animator;

    public LayerMask DamageLayers;

    public float radiusBobmb = 7f;
    public int bombDmg = 10;

    //public GameObject explosionEffectPrefab;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO check tag Bullet
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        animator.SetTrigger("Explosion");
        Destroy(gameObject, 0.5f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusBobmb, DamageLayers);
        foreach (Collider2D coll in colliders)
        {
            coll.gameObject.SendMessage("UpdateHealth", bombDmg);
        }

        //Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusBobmb);
    }
}
