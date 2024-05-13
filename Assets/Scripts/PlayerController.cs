using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //private static GameObject player;
    bool cantJump;
    bool inFall;

    void Start()
    {
        //player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        
    }

    void Update()
    {
        Movement();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
        {
            cantJump = true;
        }
    }

    void Movement()
    {
        if (Input.GetKey("a"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey("d"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKeyDown("w") && cantJump)
        {
            cantJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250f));
            gameObject.GetComponent<Animator>().SetBool("jump", true);
        }

        if (cantJump)
        {
            gameObject.GetComponent<Animator>().SetBool("jump", false);
        }
    }
}