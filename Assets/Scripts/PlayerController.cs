using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{

    bool cantJump;

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }
    void FixedUpdate()
    {
        //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
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
        if (Input.GetKey("left"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey("right"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f * Time.deltaTime, 0));
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (!Input.GetKey("up"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("jumping");
        }

        if (Input.GetKeyDown("up") && cantJump)
        {
            cantJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200f));
            gameObject.GetComponent<Animator>().SetTrigger("jumping");
        }
    }

    // Función para comprobar si hay una caja en frente del jugador
    bool isBoxInFrontOfPlayer()
    {
        // Obtener la posición del jugador
        Vector3 playerPosition = Player.transform.position;
        // Obtener la dirección en la que el jugador está mirando
        Vector3 playerDirection = Player.transform.forward;
        // Raycast desde la posición del jugador en la dirección en la que está mirando
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, playerDirection, out hit, distance))
        {
            // Si el Raycast choca con una caja, devolver true
            if (hit.collider.gameObject.tag == "Box")
            {
                return true;
            }
        }
        // De lo contrario, devolver false
        return false;
    }

    // Función para mover la caja
    void MoveBox()
    {
        // Si hay una caja en frente del jugador
        if (isBoxInFrontOfPlayer())
        {
            // Obtener la posición de la caja
            Vector3 boxPosition = box.transform.position;
            // Calcular la dirección en la que la caja debe moverse
            Vector3 moveDirection = player.transform.forward;
            // Mover la caja en la dirección calculada
            box.transform.position += moveDirection * Time.deltaTime;
        }
    }
}