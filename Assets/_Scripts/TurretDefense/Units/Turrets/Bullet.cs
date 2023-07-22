using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 1;

    private Transform target;

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target == null) return;
        
        
        if (collision.transform == target)
        {
            target.gameObject.GetComponent<Health>().Damage(damage);
            Destroy(gameObject);
        }
    }

    public void Target(Transform _target)
    {
        target = _target;
    }
}
