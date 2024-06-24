using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    float xEksen;
    public float jumpSpeed,speed;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        xEksen = 0f;
    }

    void FixedUpdate()
    {
        movement();
    }

    void jump()
    {
        rb.AddForce(Vector3.up*jumpSpeed,ForceMode.Impulse);
        anim.SetTrigger("up");
    }

    void slide()
    {
         anim.SetTrigger("down");
    }
    void movement()
    {
        rb.AddForce(Vector3.forward * speed);

        if(Input.GetKeyDown(KeyCode.A))
        {
            xEksen = -1f;
            anim.SetFloat("yatayEksen", xEksen);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            xEksen = 1f;
            anim.SetFloat("yatayEksen", xEksen);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
           slide();
        }
        xEksen=0f;
    }
}
