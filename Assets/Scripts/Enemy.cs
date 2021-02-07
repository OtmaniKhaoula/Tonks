using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public float attackRadius;
    public Transform target;
    public LootTable thisLoot;
    //public Animator animator;

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            Die();
        }
        print(health);
    }

    private void Die()
    {
        //animator.SetTrigger("Death");
        Destroy(gameObject);
        MakeLoot();
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            GameObject current = thisLoot.LootObject();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    /*void OnMouseOver()
    {
        print("I'm over " + gameObject);
        if (Input.GetMouseButtonDown(0))
        {
                PlayerMovement.instance.Attack();
        }
    }*/
}
