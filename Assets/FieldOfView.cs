using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    Collider2D FoVCollider;

    [SerializeField]
    float greenDistance;
    [SerializeField]
    float yellowDistance;
    [SerializeField]
    float redDistance;

    private GameObject player;
    private Collider2D targetCollider;
    private SpriteRenderer sprite;

    private void Start()
    {

        sprite = GetComponentInParent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {

            targetCollider = player.GetComponent<Collider2D>();

        }

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPos = player.transform.position;
        Vector2 selfPos = transform.position;

        float distance = Vector2.Distance(playerPos, selfPos);

        if (FoVCollider != null && targetCollider != null)
        {
            // Colliders are valid
            if (distance > greenDistance)
            {
                // Too far away, make FoV sprite transparent
                setColor(Color.clear);


            }else if (distance <= greenDistance && distance > yellowDistance)
            {
                // Within Green distance, set FoV color to green and reduce transparency
                setColor(Color.green);


            } else if (distance <= yellowDistance && distance > redDistance)
            {
                // Within Yellow distance, set FoV color to yellow and reduce transparency if needed
                setColor(Color.yellow);


            } else
            {
                // Within Red distance, set FoV color to red and reduce transparency if needed
                setColor(Color.red);


                // Check if player is within the collider
                if (FoVCollider.IsTouching(targetCollider))
                {

                    Debug.Log("Touching");

                }
                else
                {

                    Debug.Log("Not Touching");

                }


            }


        }
        
    }

    private void setColor(Color c)
    {

        float alpha = sprite.color.a;
        Color color = sprite.color;

        if (color != c)
        {

            sprite.color = c;

        }

    }

}
