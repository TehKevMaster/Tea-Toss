using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    float waitTime = 5f;
    [SerializeField]
    Image timesUp;
    [SerializeField]
    Button playAgain;
    [SerializeField]
    Image gameOver;
    public bool ending = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimesUp()
    {
        if (!ending)
        {
            ending = true;
            timesUp.enabled = true;
            playAgain.enabled = true;
            playAgain.gameObject.GetComponent<Image>().enabled = true;
            StartCoroutine(Wait("Title No Audio"));

        }


    }

    IEnumerator Wait(string scene)
    {

        yield return new WaitForSeconds(waitTime);
        LoadScene(scene);

    }

    public void LoadScene(string s)
    {

        SceneManager.LoadScene(s);

    }

    public void Dead()
    {
        if (!ending)
        {
            ending = true;
            gameOver.enabled = true;
            StartCoroutine(Wait("Title No Audio"));
        }

    }

}
