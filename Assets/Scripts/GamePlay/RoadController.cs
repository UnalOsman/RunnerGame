using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 3);
        }
    }
}
