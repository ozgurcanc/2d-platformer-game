using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cyclone;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject[] menuItems;

    public CharController maya;

    Animator[] menuAnims = new Animator[4];
    Text[] menuTexts = new Text[4];

    float waitTime = 0.1f;
    int currentIndex = 0;

    bool isActive = false;

    
    void Start()
    {
        for(int i=0;i<=3;i++)
        {
            menuAnims[i] = menuItems[i].GetComponent<Animator>();
            menuTexts[i] = menuItems[i].GetComponentInChildren<Text>();
        }

        pauseMenu.SetActive(false);
        StartCoroutine(ChangeColor());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            OpenAndCloseMenu();
        }
        
        if(isActive)
        {
            ChangeIndex();
            ManageInput();  
        }
        
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
                menuTexts[currentIndex+1].color = Color.white;
                menuAnims[currentIndex].SetTrigger("selected");
            }
        }
        else if(move == -1)
        {
            if(currentIndex != 3)
            {
                currentIndex++;
                
                menuTexts[currentIndex-1].color = Color.white;
                menuAnims[currentIndex].SetTrigger("selected");
                
            }
        }
    }

    IEnumerator ChangeColor()
    {
        menuTexts[currentIndex].color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        menuTexts[currentIndex].color = Color.green;
        yield return new WaitForSeconds(waitTime);
        menuTexts[currentIndex].color = Color.cyan;
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(ChangeColor());

    }

    void ManageInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(currentIndex == 0) OpenAndCloseMenu();
            else if(currentIndex == 1) Retry();
            else if(currentIndex == 2) Options();
            else if(currentIndex == 3) SaveAndQuit();
        }
        
    }

    void OpenAndCloseMenu()
    {
        menuTexts[currentIndex].color = Color.white;
        currentIndex = 0;
        isActive = !isActive;
        pauseMenu.SetActive(isActive);

        World.isPaused = !World.isPaused;        
    }

    void Retry()
    {    
        OpenAndCloseMenu();
        maya.DieChar();
    }

    void Options()
    {
        GameManager.saveManager.SaveGame(GameManager.gameStats);
        GameManager.sceneManager.LoadScene("OptionsMenu");
    }

    void SaveAndQuit()
    {
        GameManager.saveManager.SaveGame(GameManager.gameStats);
        GameManager.sceneManager.LoadScene("StartMenu");
    }


}
