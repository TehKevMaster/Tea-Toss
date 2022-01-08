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

        timesUp.enabled = true;
        playAgain.enabled = true;
        playAgain.gameObject.GetComponent<Image>().enabled = true;


    }

    public void StartTossin()
    {

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(waitTime);
        LoadScene("Title Scene");

    }

    public void LoadScene(string s)
    {

        SceneManager.LoadScene(s);

    }

    public void Dead()
    {

        gameOver.enabled = true;
        StartCoroutine(Wait());

    }

}
