using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obstaclesScript : MonoBehaviour
{
    private float deActivisionDistance = 30f;

    private Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > deActivisionDistance)
        {
            gameObject.SetActive(false);
        }
    }
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
