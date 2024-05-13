using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //private static GameObject player;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    bool cantJump;

    void Start()
    {
        //player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Movement();
        Combat();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            cantJump = true;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && cantJump)
        {
            cantJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250f));
            gameObject.GetComponent<Animator>().SetTrigger("jump");
        }
    }

    void Combat()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.GetComponent<Animator>().SetTrigger("attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Enemigo golpeado:" + enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}