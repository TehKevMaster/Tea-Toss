using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehavior : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    public bool attachedToPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        // Check if we have a parent object
        if (transform.parent != null)
        {
            // We have a parent object, check if it is the player
            if (transform.parent.tag == "Player")
            {
                // Parent is the player
                //Debug.Log("We are attached to the player");
                attachedToPlayer = true;

            } else
            {
                // Parent is not the player
                //Debug.Log("We are attached to something other than the player : " + transform.parent.name);
                attachedToPlayer = false;

            }

        } else
        {
            // We have no parent
            //Debug.Log("We have no parent");
            attachedToPlayer=false;

        }

    }
}
