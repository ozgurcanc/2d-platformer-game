using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuManager : MonoBehaviour
{
    public GameObject[] saveImages;
    public GameObject[] loadImages;
    
    Animator[] saveAnims = new Animator[3];
    Animator[] loadAnims = new Animator[3];

    Text[] saveTexts = new Text[3];
    Text[] loadTexts = new Text[3];

    int currentIndex = 0;
    
    void Start()
    {   
        for(int i=0;i<=2;i++)
        {
            saveAnims[i] = saveImages[i].GetComponent<Animator>();
            loadAnims[i] = loadImages[i].GetComponent<Animator>();

            saveTexts[i] = saveImages[i].GetComponentInChildren<Text>();
            loadTexts[i] = loadImages[i].GetComponentInChildren<Text>();

        }
        for(int i=0;i<=2;i++)
        {
            GameStats loadedGame = GameManager.saveManager.LoadGame(i);
            if(loadedGame == null)
            {
                saveTexts[i].text = "Empty";
                loadTexts[i].text = "Start New Game";
            }
            else if(loadedGame != null)
            {
                saveTexts[i].text = "Player" + (i+1).ToString();
                loadTexts[i].text = loadedGame.deathCount.ToString();
            }
        }

        saveAnims[0].SetTrigger("open");
        loadAnims[0].SetTrigger("open");
    }

    void Update()
    {
        ChangeIndex(); 
        ManageInput();
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
                saveAnims[currentIndex].SetTrigger("open");
                loadAnims[currentIndex].SetTrigger("open");
                
                saveAnims[currentIndex+1].SetTrigger("close");
                loadAnims[currentIndex+1].SetTrigger("close");
            }
        }
        else if(move == -1)
        {
            if(currentIndex != 2)
            {
                currentIndex++;
                saveAnims[currentIndex].SetTrigger("open");
                loadAnims[currentIndex].SetTrigger("open");
                
                saveAnims[currentIndex-1].SetTrigger("close");
                loadAnims[currentIndex-1].SetTrigger("close");
            }
        }
    }

    void ManageInput()
    {
        if(Input.GetKeyDown(GameManager.inputManager.confirm))
        {
            GameStats loadedGame = GameManager.saveManager.LoadGame(currentIndex);

            if(loadedGame != null)
            {
                GameManager.gameStats = loadedGame;
                GameManager.sceneManager.LoadScene("Level"+loadedGame.currentLevel.ToString());
            }
            else if(loadedGame == null)
            {
                GameStats newGame = new GameStats();
                newGame.InitNewGame(currentIndex);
                
                GameManager.gameStats = newGame;
                GameManager.sceneManager.LoadScene("Level"+newGame.currentLevel.ToString());
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Delete))
        {
            GameStats loadedGame = GameManager.saveManager.LoadGame(currentIndex);

            if(loadedGame != null) 
            {
                GameManager.saveManager.DeleteGame(loadedGame);
            
                GameManager.sceneManager.LoadScene("LoadMenu");
            }  
        }

        if(Input.GetKeyDown(GameManager.inputManager.back))
        {
            GameManager.sceneManager.LoadScene("StartMenu");
        }
    }
}
