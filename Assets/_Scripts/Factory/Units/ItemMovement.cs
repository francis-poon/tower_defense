using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Boolean hasTarget;

    private void Update()
    {
        if (!hasTarget) return;

        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            hasTarget = false;
            rb.velocity = Vector2.zero;
            FactoryLevelManager.main.ProcessItem(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!hasTarget) return;

        Vector2 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    //public void SetTarget(Transform _target)
    //{
    //    targetPosition = _target.position;
    //    hasTarget = true;
    //}

    public void MoveTo(Transform _target, float _moveSpeed)
    {
        targetPosition = _target.position;
        moveSpeed = _moveSpeed;
        hasTarget = true;
    }
}
