using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{

    Rigidbody rb;

    bool havadami = false;
    int hiz=3;



    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Hareket();
    }

    public void Hareket()
    {

        transform.Translate(new Vector3(0,0,hiz*Time.deltaTime));


        

        if(havadami==false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity=new Vector3 (0,600f*Time.deltaTime,0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("floor"))
        {
            havadami= false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.CompareTag("floor"))
        {
            havadami = true;
        }
    }


}
