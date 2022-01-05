using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    [SerializeField]
    Animator anim;

    [SerializeField]
    ParticleSystem footprints;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        footprints = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0 && moveInput.y == 0)
        {

            anim.SetBool("isWalking", false);

        } else
        {

            anim.SetBool("isWalking", true);
            footprints.Emit(1);

            if (moveInput.x < 0)
            {

                Vector3 newRotation = new Vector3(transform.rotation.x, 1, transform.rotation.z);
                transform.eulerAngles = newRotation;

                Vector3 newScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
                footprints.transform.localScale = newScale;


            } else if (moveInput.x > 0)
            {

                Vector3 newRotation = new Vector3(transform.rotation.x, -1, transform.rotation.z);
                transform.eulerAngles = newRotation;

                Vector3 newScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
                footprints.transform.localScale = newScale;

            }

        }

    }

    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

    }

}
