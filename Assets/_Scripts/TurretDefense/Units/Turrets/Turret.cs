using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float maxFireAngle = 10f;

    private Transform target;
    private float timeSinceLastFire;

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else if (IsTargetInRange())
        {
            RotateTowardsTarget();
            FireBullet();
        }
        else
        {
            target = null;
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        if (target == null)
        {
            Debug.Log("Target is null");
        }
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void FireBullet()
    {
        // Limit firing rate to the set parameter
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire < 1 / fireRate) return;

        // Prevents firing if the turret gun isn't sufficiently pointed at the target
        Quaternion targetAngle = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90));
        if (targetAngle.eulerAngles.z - turretRotationPoint.eulerAngles.z > maxFireAngle) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Target(target);

        timeSinceLastFire = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
