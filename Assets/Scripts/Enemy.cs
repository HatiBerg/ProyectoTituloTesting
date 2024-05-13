using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Animación de daño
        gameObject.GetComponent<Animator>().SetTrigger("takeHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemigo muerto");

        // Animación de muerte
        gameObject.GetComponent<Animator>().SetBool("isDead", true);

        // Desactivar o destruir al enemigo
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
