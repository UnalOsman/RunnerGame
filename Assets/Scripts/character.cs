using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    int hiz=10;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Hareket();
    }

    public void Hareket()
    {
        transform.position =new Vector3(transform.position.x,transform.position.y,transform.position.z*hiz*Time.deltaTime);


        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position=new Vector3(transform.position.x,transform.position.y + 0.62f,transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.14f, transform.position.z);
            transform.Rotate(new Vector3(transform.rotation.x - 80f,transform.rotation.y,transform.rotation.z),1f);
        }
    }


}
