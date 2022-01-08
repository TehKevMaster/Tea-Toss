using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    #region Variables

    public Transform[] patrolPoints;            // Array of patrol points for guard to go to
    public Transform playerPos;                 // Player's current position

    [SerializeField] float speed = 5f;          // Guard's movement speed
    [SerializeField] float waitTime = 1f;       // Guard's wait time before moving to next patrol point
    int currentPointIndex;

    bool once;
    public bool playerSpotted = false;                 // True if player is spotted by guard

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpotted == false)
        {
            MoveToPatrolPoint();                // Call function to move guard to next patrol point
        }
        else
        {
            MoveTowardPlayer();
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


    // Moves the guard toward  the next patrol point
    void MoveToPatrolPoint()
    {
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
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

}
