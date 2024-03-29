using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class character : MonoBehaviour
{
    Animator anim;

    Rigidbody rb;
    float speed;
    float jumpSpeed;
    float gravitScale;
    float slideSpeed;
    int laneIndex;
    bool isWaiting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
        speed = 200f;
        jumpSpeed = 50f;
        gravitScale = 800f;
        slideSpeed = 150f;
        laneIndex = 0;
    }

    void Update()
    {

        rb.AddForce(Physics.gravity * gravitScale * Time.deltaTime, ForceMode.Acceleration);
        jump();
        yanYon();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * speed, ForceMode.Force);

    }


    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0f)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            anim.SetTrigger("up");
        }
    }

    void yanYon()
    {
        if (!isWaiting) // E�er bekleme i�lemi yoksa, yeni bir bekleme i�lemi ba�lat
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (laneIndex != 1)
                {
                    rb.AddForce(Vector3.left * slideSpeed, ForceMode.Impulse);
                    laneIndex++;
                    StartCoroutine(WaitForSecondsCoroutine(0.1f)); // Bekleme i�lemi ba�lat
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (laneIndex != -1)
                {
                    rb.AddForce(Vector3.right * slideSpeed, ForceMode.Impulse);
                    laneIndex--;
                    StartCoroutine(WaitForSecondsCoroutine(0.1f)); // Bekleme i�lemi ba�lat
                }
            }
        }
    }

    IEnumerator WaitForSecondsCoroutine(float duration)
    {
        isWaiting = true; // Bekleme i�lemi ba�lad���nda bayra�� true yap
        yield return new WaitForSeconds(duration);
        isWaiting = false; // Bekleme i�lemi bitti�inde bayra�� false yap
    }
}
