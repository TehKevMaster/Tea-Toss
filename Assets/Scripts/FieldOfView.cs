using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    Collider2D FoVCollider;

    [Range(0f, 1f)]
    public float alphaFloat;
    [SerializeField]
    float maxDistance;
    [SerializeField]
    Color maxDistanceColor;
    [SerializeField]
    float midDistance;
    [SerializeField]
    Color midDistanceColor;
    [SerializeField]
    float minDistance;
    [SerializeField]
    Color minDistanceColor;

    private GameObject player;
    private Collider2D targetCollider;
    private float distance;
    private SpriteRenderer sprite;
    public bool alerted;
    public GameObject alert;

    private bool inBarrel;

    private void Start()
    {

        sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {

            targetCollider = player.GetComponent<Collider2D>();

        }

        alerted = false;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPos = player.transform.position;
        Vector2 selfPos = transform.position;

        distance = Vector2.Distance(playerPos, selfPos);

        if (FoVCollider != null && targetCollider != null)
        {

            ColliderCheck();

            if (!alerted)
            {

                // Colliders are valid
                if (distance > maxDistance)
                {
                    // Too far away, make FoV sprite transparent
                    setColor(Color.clear);


                }
                else if (distance <= maxDistance && distance > midDistance)
                {
                    // Within Green distance, set FoV color to green and reduce transparency
                    setColor(maxDistanceColor);


                }
                else if (distance <= midDistance && distance > minDistance)
                {
                    // Within Yellow distance, set FoV color to yellow and reduce transparency if needed
                    setColor(midDistanceColor);


                }
                else
                {
                    // Within Red distance, set FoV color to red and reduce transparency if needed
                    setColor(minDistanceColor);

                }
            } else
            {

                if (sprite.enabled)
                {

                    sprite.enabled = false;

                }

            }


        }
        
    }

    private void ColliderCheck()
    {

        if (FoVCollider.IsTouching(targetCollider))
        {

            if (!alerted)
            {

                SetAlerted();

            }

            alphaFloat = 1f;
            //Debug.Log("Touching");

        } else
        {
            
            alphaFloat = 1f - (distance / maxDistance);
            //Debug.Log("Not Touching");

        }

    }

    private void setColor(Color c)
    {

        float alpha = sprite.color.a;
        Color currentColor = sprite.color;
        Color color = new Color(c.r, c.g, c.b, alphaFloat);

        if (currentColor != color)
        {

            sprite.color = color;

        }

    }

    public void SetAlerted()
    {
        alerted = true;

        if (!alert.activeInHierarchy)
        {

            alert.SetActive(true);

        }

    }

}
