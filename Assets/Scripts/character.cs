using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public GameObject Base1;
    Rigidbody rb;

    bool havadami = false;
    int hiz=3;
    Vector3 konum = Vector3.zero;


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

        if (havadami == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = new Vector3(0, 500f * Time.deltaTime, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x < 1.55f)
            {
                transform.position = new Vector3(transform.position.x + 1.55f, transform.position.y, transform.position.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x > -1.55f)
            {
                transform.position = new Vector3(transform.position.x - 1.55f, transform.position.y, transform.position.z);
            }
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


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ileri2"))
        {
            if(Base1.transform.position.z==33)
            {
                Base1.GetComponent<Transform>().transform.position = new Vector3( Base1.transform.position.x,Base1.transform.position.y, -26.82f);
            }
            else
            {
                Base1.GetComponent<Transform>().transform.position = new Vector3(Base1.transform.position.x,Base1.transform.position.y, 33f);
            }
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
