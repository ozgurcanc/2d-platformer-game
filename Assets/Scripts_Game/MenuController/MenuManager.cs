using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    public Text[] texts = new Text[3];
    public float waitTime;
    int currentIndex = 0;
    
    float delayTime = 3f;
    

    void Start()
    {
        StartCoroutine(ChangeColor());
        Cursor.visible = false;
    }

    void Update()
    {
        ChangeIndex();
        LoadScene();
    }

    IEnumerator ChangeColor(){
        yield return new WaitForSeconds(waitTime);
        texts[currentIndex].color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        texts[currentIndex].color = Color.blue;
        yield return new WaitForSeconds(waitTime);
        texts[currentIndex].color = Color.cyan;
        yield return new WaitForSeconds(waitTime);
        texts[currentIndex].color = Color.red;

        StartCoroutine(ChangeColor());
    }

    void ChangeIndex()
    {
        int move=0;
        if(Input.GetKeyDown(KeyCode.DownArrow)) move = -1;
        if(Input.GetKeyDown(KeyCode.UpArrow)) move = 1;   

        if(move == 1)
        {
            if(currentIndex != 0)
            {
                currentIndex--;
                texts[currentIndex+1].color = Color.green;
            }
        }
        else if(move == -1)
        {
            if(currentIndex != 2)
            {
                currentIndex++;
                texts[currentIndex-1].color = Color.green;
            }
        }
    }

    void LoadScene()
    {
        if(Input.GetKeyDown(GameManager.inputManager.confirm))
        {
            if(currentIndex == 0) Climb();
            else if(currentIndex == 1) Options();
            else if(currentIndex == 2) Exit();
        }
    }

    void Climb()
    {
        GameManager.sceneManager.LoadScene("LoadMenu");
    }

    void Options()
    {
        GameManager.sceneManager.LoadScene("OptionsMenu");
    }

    void Exit()
    {
        Application.Quit();
    }


}
