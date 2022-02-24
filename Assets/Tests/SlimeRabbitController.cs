using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeRabbitController : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 0.1f;
    public float rotationSpeed = 8f;
    public float hitPoint = 100f;

    private float moveDirection;
    public Rigidbody rb;
    public Text hitPointText;

    private Camera playerCamera;
    public Transform viewPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("HitPoint", hitPoint);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;


        //fightLayer = animator.GetLayerIndex("Fight");


    }

    // Update is called once per frame
    void Update()
    {
        playerCamera.transform.position = viewPoint.position;
        playerCamera.transform.rotation = viewPoint.rotation;

        animator.SetBool("Move", false);
        animator.SetFloat("HitPoint", hitPoint);
        hitPointText.text = "HP: " + animator.GetFloat("HitPoint");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Move", true);
            transform.position = transform.position + transform.forward * moveSpeed;
            //transform.position = transform.forward * moveSpeed;
            // animator.SetFloat("Speed", 0.2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Move", true);
            transform.position = transform.position + transform.forward * moveSpeed * -1;

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Move", true);
            //transform.Rotate(0f, 10f, 0f);
            moveDirection = 2f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Move", true);
            //transform.Rotate(0f, -10f, 0f);
            moveDirection = -2f;

        }
        else
        {
            moveDirection = 0f;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + moveDirection, transform.rotation.eulerAngles.z);

        if (animator.GetFloat("HitPoint") <= 0f)
        {
            animator.SetTrigger("Death");
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit! 1");
        if (collision.gameObject.tag == "DamageObject")
        {
            Debug.Log("Damage!");
            //rb.constraints = RigidbodyConstraints.FreezePositionY;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            animator.SetBool("Damage", true);
            hitPoint -= 50f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Hit! 3");
        if (collision.gameObject.tag == "DamageObject")
        {
            Destroy(collision.gameObject);
        }


    }


}
