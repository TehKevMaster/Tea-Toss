using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{

    Vector2 screenBounds;
    float objectWidth = 0;
    float objectHeight = 0;

    [SerializeField]
    float minOffsetX = -0.25f;
    [SerializeField]
    float maxOffsetX = 0.25f;
    [SerializeField]
    float minOffsetY = -0.15f;
    [SerializeField]
    float maxOffsetY = 4.5f;



    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        //objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y  * -1 + objectHeight, screenBounds.y - objectHeight);
        //transform.position = viewPos;

        float minX = screenBounds.x * -1 + objectWidth;
        float maxX = screenBounds.x + objectWidth;

        float minY = screenBounds.y * -1 + objectHeight;
        float maxY = screenBounds.y - objectHeight;

        viewPos.x = Mathf.Clamp(viewPos.x, minX - minOffsetX, maxX - maxOffsetX);
        viewPos.y = Mathf.Clamp(viewPos.y, minY - minOffsetY, maxY - maxOffsetY);
        transform.position = new Vector2(viewPos.x, viewPos.y);

    }

}
