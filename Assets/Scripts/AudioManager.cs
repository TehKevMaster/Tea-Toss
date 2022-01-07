using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip thunder;
    public AudioSource mainSource;

    float randomNumber;
    bool thunderSoundTrig = true;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    //asdf
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thunderSoundTrig == true)
        {
            StartCoroutine("ThunderSounds");
            thunderSoundTrig = false;
        }
    }

    IEnumerator ThunderSounds()
    {

        randomNumber = Random.Range(0, 10);
        yield return new WaitForSeconds(randomNumber);
        mainSource.PlayOneShot(thunder);
        thunderSoundTrig = true;
    }

}
