using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase1 : MonoBehaviour
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private string enemyClass;
    [SerializeField]
    private float health;
    [SerializeField]
    private int enemyAttack;

    private void Awake()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}