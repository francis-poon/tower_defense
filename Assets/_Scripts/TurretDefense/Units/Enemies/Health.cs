using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 2;

    private bool isDestroyed = false;

    public void Damage(int _dmg)
    {
        health -= _dmg;
        if (health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            gameObject.GetComponent<Drops>().DropResources();
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
