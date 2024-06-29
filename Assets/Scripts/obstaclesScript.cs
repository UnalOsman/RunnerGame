using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obstaclesScript : MonoBehaviour
{
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerMovement player=other.GetComponent<playerMovement>();

            if(player != null)
            {
                player.HitObstacle();
            }
        }
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement player = collision.gameObject.GetComponent<playerMovement>();

            if (player != null)
            {
                player.HitObstacle();
            }
        }
    }
}
