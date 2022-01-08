using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    #region Variables
    Rigidbody2D body;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float moveLimiter = 0.7f;

    [SerializeField] float speed = 20.0f;

    [SerializeField]
    CircleCollider2D itemCheckCollider;
    [SerializeField]
    LayerMask interactableLayerMask;
    [SerializeField]
    Transform attachedObject;
    private Transform bodyObj;
    [SerializeField]
    Collider2D[] colliders;
    [SerializeField]
    Collider2D areaCollider;

    public Animator anim;
    public ParticleSystem footprints;

    #endregion

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bodyObj = transform.Find("Body");
        itemCheckCollider = GetComponentInChildren<CircleCollider2D>();
        GameObject area = GameObject.FindGameObjectWithTag("Throwable Area");
        areaCollider = area.GetComponent<Collider2D>();

    }

    void Update()
    {
        GrabMovementInputValues();

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Interact();

        }

        if (horizontalInput == 0 && verticalInput == 0)
        {

            anim.SetBool("isWalking", false);

        }
        else
        {

            anim.SetBool("isWalking", true);
            footprints.Emit(1);

            if (horizontalInput < 0)
            {

                Vector3 newRotation = new Vector3(transform.rotation.x, 1, transform.rotation.z);
                transform.eulerAngles = newRotation;

                Vector3 newScale = new Vector3(-0.5f, 0.5f, 0.5f);
                footprints.transform.localScale = newScale;


            }
            else if (horizontalInput > 0)
            {

                Vector3 newRotation = new Vector3(transform.rotation.x, -1, transform.rotation.z);
                transform.eulerAngles = newRotation;

                Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
                footprints.transform.localScale = newScale;

            }

        }
    }

    void FixedUpdate()
    {

        MovePlayer();

    }

    void GrabMovementInputValues()
    {
        // Gives a value between -1 and 1
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 is left, 1 is right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 is down, 1 is up
    }

    void MovePlayer()
    {
        if (horizontalInput != 0 && verticalInput != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
    }

    void Interact()
    {

        //Debug.Log("Interact");
        //Check if we already have an item
        if (bodyObj.childCount > 0)
        {
            // Drop item
            Debug.Log("We have a child");
            attachedObject = bodyObj.GetChild(0);

            if (itemCheckCollider.IsTouching(areaCollider))
            {
                // Toss
                GameObject tea = attachedObject.gameObject;
                //items.Remove(tea.transform);
                tea.GetComponent<TeaBehavior>().toss();

            }

            // Drop
            bodyObj.transform.DetachChildren();



        } else
        {
            // Check for interactable item
            colliders = Physics2D.OverlapCircleAll(itemCheckCollider.gameObject.transform.position, 0.15f, interactableLayerMask);
            List<Transform> items = new List<Transform> ();

            if (colliders == null)
            {

                //Debug.Log("No objects in area");

            } else
            {

                //Debug.Log("Object in area");
                foreach (Collider2D collider in colliders)
                {
                    GameObject i = collider.gameObject;
                    //Debug.Log(i.name);
                    items.Add(i.transform);

                }

                Transform target = ClosestItem(items.ToArray());
                //Debug.Log("Target: " + target.name);

                if(target.parent != null)
                {
                    if (target.parent.name == "Crate Stack")
                    {

                        //Debug.Log("Stack");
                        target.parent.GetComponent<SpawnHandler>().GiveCrate();

                    }
                    

                } else 
                {

                    AttachObject(target);

                }

        

                
                //Debug.Log(items.Count);


            }

        }

    }

    Transform ClosestItem(Transform[] items)
    {

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in items)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;

    }

    void AttachObject(Transform target)
    {

        target.SetParent(bodyObj);

    }

}
