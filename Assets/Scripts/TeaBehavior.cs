using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaBehavior : MonoBehaviour
{

    [SerializeField]
    Collider2D areaCollider;
    private Collider2D teaCollider;

    [SerializeField]
    bool tossable;

    [SerializeField]
    int scoreValue = 1;

    public SpawnHandler spawnHandler;

    [SerializeField]
    bool buttonPressed = false;

    private RipplePostProcessor camRipple;
    public float z;

    public ParticleSystem smoke;
    public ParticleSystem splashEffect;
    public AudioClip splashAudio;
    public float volume;
    public AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {



        camRipple = Camera.main.GetComponent<RipplePostProcessor>();

        GameObject area = GameObject.FindGameObjectWithTag("Throwable Area");
        areaCollider = area.GetComponent<Collider2D>();

        tossable = false;

        GameObject spawner = GameObject.Find("Crate Stack");
        spawnHandler = spawner.GetComponent<SpawnHandler>();

        //splashEffect = GetComponentInChildren<ParticleSystem>();

        if (GetComponent<Collider2D>() != null)
        {

            teaCollider = GetComponent<Collider2D>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (teaCollider != null && areaCollider != null)
        {

            ColliderCheck();

        }

        if(transform.parent != null)
        {

            Vector3 parentPos = transform.parent.position;
            transform.position = new Vector3(parentPos.x, parentPos.y + 0.25f, z);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, Time.deltaTime);

        }

    }

    private void ColliderCheck()
    {

        if (teaCollider.IsTouching(areaCollider))
        {

            tossable = true;

        } else
        {

            tossable = false;

        }

    }

    public void toss()
    {

        Vector2 loc = new Vector2(transform.position.x, transform.position.y + 1.5f);
        camRipple.RippleEffect(loc);
        buttonPressed = false;
        spawnHandler.score += scoreValue;
        Scored();

    }

    void Scored ()
    {

        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        splashEffect.Emit(3);
        audioSource.PlayOneShot(splashAudio, volume);

        StartCoroutine(DestroyTea());

    }

    IEnumerator DestroyTea()
    {

        yield return new WaitForSeconds(2.5f);
        GameObject.Destroy(transform.gameObject);

    }

}
