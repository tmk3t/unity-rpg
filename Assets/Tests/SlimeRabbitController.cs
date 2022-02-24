using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeRabbitController : MonoBehaviour
{
    Animator animator;

    public Rigidbody rb;
    public Text hitPointText;
    public Transform viewPoint;

    public float moveSpeed = 0.1f;
    public float rotationSpeed = 8f;
    public float hitPoint = 100f;

    private float moveDirection = 0f;

    private bool isDead = false;



    private Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("HitPoint", hitPoint);
        playerCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        CameraUpdate();

        animator.SetBool("Move", false);
        animator.SetFloat("HitPoint", hitPoint);
        hitPointText.text = "HP: " + animator.GetFloat("HitPoint");

        if (isDead != true)
        {
            Move();
        }


        if (animator.GetFloat("HitPoint") <= 0f)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }

    }

    void CameraUpdate()
    {
        playerCamera.transform.position = viewPoint.position;
        playerCamera.transform.rotation = viewPoint.rotation;
    }


    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Move", true);
            transform.position = transform.position + transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Move", true);
            transform.position = transform.position + transform.forward * moveSpeed * -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Move", true);
            moveDirection = 2f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Move", true);
            moveDirection = -2f;
        }
        else
        {
            moveDirection = 0f;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + moveDirection, transform.rotation.eulerAngles.z);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DamageObject")
        {
            animator.SetBool("Damage", true);
            hitPoint -= 50f;
            Destroy(collision.gameObject);
        }
    }

}
