using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class character : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    bool havadami = false;
    float hiz=2f;
    bool sag = false;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hareket();
    }

    public void Hareket()
    {

        rb.MovePosition(rb.velocity);

        if (havadami == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                
                rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);

                anim.SetTrigger("up");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                anim.SetTrigger("down");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
             sag = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            sag= false;
        }


        if(sag)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(1.5f, transform.position.y,
                transform.position.z),Time.deltaTime*3f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-0.5f, transform.position.y,
                transform.position.z), Time.deltaTime * 3f);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if( other.CompareTag("floor") || other.CompareTag("ziplananEngel"))
        {
            havadami = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("floor") || other.CompareTag("ziplananEngel"))
        {
            havadami = true;
        }
    }


    

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("floor") || collision.collider.CompareTag("ziplananEngel"))
        {
            havadami = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("floor") || collision.collider.CompareTag("ziplananEngel"))
        {
            havadami = true;
        }
    }


}
