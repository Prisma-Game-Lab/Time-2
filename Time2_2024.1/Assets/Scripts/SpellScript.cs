using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    protected float damage;
    public string targetTag;

    public void SetUp(string enemyTag,float spellDamage, Vector2 speed, float destructionTimer) 
    {
        damage = spellDamage;
        Destroy(gameObject, destructionTimer);
        GetComponent<Rigidbody2D>().velocity = speed;
        targetTag = enemyTag;
        if (targetTag == "Player") 
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag) 
        {
            case "Enemy":
                if (targetTag == "Enemy")
                {
                    EnemyBase enemy = collision.GetComponent<EnemyBase>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                    else
                    {
                        collision.GetComponent<BossController>().TakeDamage(damage);
                    }
                    Destroy(gameObject);
                }
                break;

            case "Player":
                if (targetTag == "Player")
                {
                    collision.GetComponent<PlayerController>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                break;

            case "Wall":
                Destroy(gameObject);
                break;
        }
    }
}
