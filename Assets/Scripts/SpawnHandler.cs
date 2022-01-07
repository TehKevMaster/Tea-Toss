using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHandler : MonoBehaviour
{

    [SerializeField]
    GameObject[] teaCratesToSpawn;
    public GameObject[] teaCratesActive;
    public float teaSpawnTime = 15f;

    [SerializeField]
    GameObject[] guardsToSpawn;
    public GameObject[] guardsActive;

    [SerializeField]
    GameObject[] spawningLocations;

    [SerializeField]
    Text teaActiveText;
    [SerializeField]
    Text guardsActiveText;
    [SerializeField]
    Text scoreText;
    [HideInInspector]
    public int score = 0;

    private GameObject player;
    [SerializeField]
    LayerMask spawnerLayerMask;

    [SerializeField]
    Camera mainCamera;
    private Vector2 centerOfScreen;

    [SerializeField]
    int currentCrate = 0;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Initializing...");

        //Debug.Log("Finding center point of screen");
        centerOfScreen = mainCamera.transform.position;

        //Debug.Log("Finding Player object");
        player = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log("Finding all spawning locations");
        spawningLocations = GameObject.FindGameObjectsWithTag("Spawn Point");

        //Debug.Log("Starting Spawn Tea Coroutine");
        StartCoroutine(SpawnTea());

    }

    // Update is called once per frame
    void Update()
    {

        teaCratesActive = findOjects("Tea");
        guardsActive = findOjects("Guard");
        UpdateUI();
        CountCrates();
    }

    void Spawn(GameObject obj)
    {
        List<GameObject> possiblePoints = new List<GameObject>();

        // Find Player Position
        Vector2 playerPos = player.transform.position;
        //Debug.Log("Player Position: " + playerPos);

        // Find every point with an acceptable position
        foreach (GameObject p in spawningLocations)
        {

            Vector2 pointPos = p.transform.position;
            float distance = Vector2.Distance(playerPos, pointPos);

            if (distance >= 2)
            {

                Collider2D[] colliders = Physics2D.OverlapCircleAll(pointPos, 0.5f, spawnerLayerMask);

                if (colliders.Length == 0)
                {

                    // Find which half of screen the player is on
                    if (playerPos.x >= centerOfScreen.x)
                    {
                        // We are on the right side of the screen
                        if (pointPos.x < centerOfScreen.x)
                        {

                            possiblePoints.Add(p);

                        }

                    }
                    else
                    {
                        // We are on the left side of the screen
                        if (pointPos.x > centerOfScreen.x)
                        {

                            possiblePoints.Add(p);

                        }

                    }

                }
                
            }

        }

        if (possiblePoints.Count > 0)
        {

            GameObject point = possiblePoints[Random.Range(0, possiblePoints.Count - 1)];
            Vector2 spawnPos = point.transform.position;

            Debug.Log("Spawning " + obj.name + " on " + point.name + " : Position - " + spawnPos);

            Instantiate(obj, spawnPos, Quaternion.identity);

        } else
        {

            Debug.Log("No acceptable spawn positions");

        }

       
    }

    GameObject[] findOjects(string tag)
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        if (objects != null)
        {

            // Debug.Log("We found " + objects.Length + " objects with the tag: " + tag);
            return objects;

        } else
        {

            return null;

        }


    }

    void UpdateUI()
    {
        scoreText.text = "Tea Tossed - " + score;
        teaActiveText.text = "Tea Active - " + teaCratesActive.Length;
        guardsActiveText.text = "Guards Active - " + guardsActive.Length;

    }

    void ResetStack()
    {

        Debug.Log("Reseting Crate Stack");

        int children = transform.childCount;

        //Debug.Log("Children found: " + children);
        for (int i = 0; i < children; i++)
        {

            GameObject crate = transform.GetChild(i).gameObject;
            SpriteRenderer sprite = crate.GetComponent<SpriteRenderer>();

            //Debug.Log("Child: " + crate.name);

            if (!sprite.enabled)
            {
                //Debug.Log("Enabling Sprite Renderer");
                sprite.enabled = true;

            } else
            {

                //Debug.Log("Sprite Renderer is enabled");

            }

        }

        teaSpawnTime -= 2.5f;

        SpawnGuards();

        Debug.Log("New Tea Spawn Time is " + teaSpawnTime + " seconds");

        currentCrate = 0;

    }

    IEnumerator SpawnTea()
    {

        while (true)
        {

            int i = Random.Range(0, teaCratesToSpawn.Length);
            //Debug.Log("Spawning Tea " + i);
            Spawn(teaCratesToSpawn[i]);

            Debug.Log("Tea Spawner : Waiting " + teaSpawnTime + " seconds");
            yield return new WaitForSeconds(teaSpawnTime);

        }

    }

    void SpawnGuards()
    {

        int i = Random.Range(0, guardsToSpawn.Length);
        //Debug.Log("Spawning Guard " + i);
        Spawn(guardsToSpawn[i]);

    }

    void CountCrates()
    {
        int disabledCrates = 0;

        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {

            GameObject crate = transform.GetChild(i).gameObject;
            SpriteRenderer sprite = crate.GetComponent<SpriteRenderer>();

            // Debug.Log("Child: " + crate.name);

            if (!sprite.enabled)
            {

                disabledCrates++;

            }

        }

        if (disabledCrates == children)
        {

            //Debug.Log("All crates are disabled");
            ResetStack();

        }

    }

    public void GiveCrate()
    {

        Transform body = player.GetComponentInChildren<BoxCollider2D>().gameObject.transform;
        int i = Random.Range(0, teaCratesToSpawn.Length);
    
        GameObject tea = Instantiate(teaCratesToSpawn[i], body.transform.position, Quaternion.identity);
        tea.transform.SetParent(body);

        GameObject crate = transform.GetChild(currentCrate).gameObject;
        SpriteRenderer sprite = crate.GetComponent<SpriteRenderer>();

        sprite.enabled = false;
        currentCrate++;

    }

}
