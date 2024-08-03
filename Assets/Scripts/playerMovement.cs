using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public float jumpForce = 10f;
    public float slideDuration = 1f;
    public float speed = 3f;
    private bool isSliding = false;
    bool isJumping = false;
    float gravityScale;

    int laneIndex;
    public float slideSpeed = 2f;
    bool isWaiting = false;
    Vector3 targetPosition;

    public int hitPoints = 3;
    public float invisibleDuration = 2f;
    private bool isInvisible = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing from the player object.");
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the player object.");
        }



        animator.SetBool("IsRunning", true);
        laneIndex = 0;
        gravityScale = 400f;
        targetPosition = transform.position;
    }

    void Update()
    {
        rb.AddForce(Physics.gravity * gravityScale * Time.deltaTime, ForceMode.Acceleration);
        targetPosition += Vector3.forward * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && !isWaiting)
        {
            if (laneIndex < 1)
            {
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !isWaiting)
        {
            if (laneIndex > -1)
            {
                MoveRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && !isSliding)
        {
            StartCoroutine(Slide());
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3f);
        /*
        if (hitPoints <= 0)
        {
            Die();
        }
        */
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }
    /*
    public void HitObstacle()
    {
        if (!isInvisible)
        {
            hitPoints--;
            StartCoroutine(Blink());
            StartCoroutine(TemporaryInvisible());
        }
    }
    */

    void MoveLeft()
    {
        animator.SetFloat("Direction", -1);
        laneIndex++;
        targetPosition += Vector3.left * slideSpeed;

        StartCoroutine(ResetDirection());
        StartCoroutine(WaitForSecondsCoroutine(0.1f)); // Bekleme iþlemi baþlat
    }

    void MoveRight()
    {
        animator.SetFloat("Direction", 1);
        laneIndex--;
        targetPosition += Vector3.right * slideSpeed;
        StartCoroutine(ResetDirection());
        StartCoroutine(WaitForSecondsCoroutine(0.1f)); // Bekleme iþlemi baþlat
    }

    void Jump()
    {
        if (rb.velocity.y == 0f && !isJumping)
        {
            isJumping = true;
            animator.SetTrigger("IsJumping");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);
        yield return new WaitForSeconds(slideDuration);
        animator.SetBool("IsSliding", false);
        isSliding = false;
    }
/*
    private IEnumerator Blink()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();

        if(renderer != null)
        {
            for (int i = 0; i < 10 ; i++)
            {
                renderer.enabled = !renderer.enabled;

                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            Debug.Log("Renderer bulunamadý!");
        }
        renderer.enabled = true;
    }

    private IEnumerator TemporaryInvisible()
    {
        isInvisible = true;
        yield return new WaitForSeconds(invisibleDuration);
        isInvisible = false;
    }

    private void Die()
    {
        Debug.Log("öldük");
    }
*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

        }
    }

    private IEnumerator ResetDirection()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Direction", 0);
    }

    IEnumerator WaitForSecondsCoroutine(float duration)
    {
        isWaiting = true; // Bekleme iþlemi baþladýðýnda bayraðý true yap
        yield return new WaitForSeconds(duration);
        isWaiting = false; // Bekleme iþlemi bittiðinde bayraðý false yap
    }

}
