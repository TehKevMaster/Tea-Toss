using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    #region Variables

    public GameObject[] patrolPoints;          // Array of patrol points for guard to go to
    public Transform playerPos;                 // Player's current position

    [SerializeField]
    Transform foVTransform;
    FieldOfView fov;

    [SerializeField] float speed = 5f;          // Guard's movement speed
    [SerializeField] float waitTime = 1f;       // Guard's wait time before moving to next patrol point
    int currentPointIndex;

    bool once;
    public bool playerSpotted = false;                 // True if player is spotted by guard
    public bool playedOiSound = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponentInChildren<FieldOfView>();
        patrolPoints = GameObject.FindGameObjectsWithTag("Patrol Points");
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        foVTransform = FindChild();
    }

    // Update is called once per frame
    void Update()
    {

        if (fov.alerted == true)
        {
            playerSpotted = true;
            if (!playedOiSound)
            {

                AudioManager.instance.PlayOiSound();
                playedOiSound=true;

            }
        }

        if (playerSpotted == false)
        {
            MoveToPatrolPoint();                // Call function to move guard to next patrol point
        }
        else
        {
            MoveTowardPlayer();

            float distance = Vector2.Distance(playerPos.transform.position, transform.position);
            if(distance <= .1f)
            {

                GameOver gameOver = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameOver>();
                if(gameOver.ending == false)
                {

                    gameOver.Dead();

                }

            }


        }


    }

    // Coroutine Wait to have guard wait to move to next patrol point.
    // Increases index so guard will move to the next patrol point in the array
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        } else
        {
            currentPointIndex = 0;
        }
        once = false;

    }

    void RotateTowardsTarget()
    {

        Vector2 direction = patrolPoints[currentPointIndex].transform.position - foVTransform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        foVTransform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));

    }


    // Moves the guard toward  the next patrol point
    void MoveToPatrolPoint()
    {
        if (transform.position != patrolPoints[currentPointIndex].transform.position)
        {
            RotateTowardsTarget();
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }


    // Moves the guard toward the player
    void MoveTowardPlayer()
    {
        if (transform.position != playerPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
    }

    Transform FindChild()
    {

        GameObject body = transform.Find("British Body").gameObject;
        GameObject f = body.transform.Find("FoV").gameObject;
        
        if (f != null)
        {

            Transform t = f.transform;

            if(t != null)
            {

                return t;

            } else
            {

                return null;

            }

        } else
        {

            return null;

        }

    }

}
