using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    public Text[] textRight = new Text[11];
    public Text[] textLeft = new Text[12];
    
    float waitTime = 0.1f;
    int currentIndex = 0;

    float smoothConst = 50f;
    public GameObject test;

    public GameObject panel;
    public Text panelText;
    
    bool isActive = true;

    float pressedTime = 0f;
    float afterTime = 0f;
    

    void Start()
    {
        StartCoroutine(ChangeColor());
        panel.SetActive(false);

        for(int i=0;i<11;i++)
        {
            textRight[i].text = AdjustKey(GameManager.inputManager[i]);
        }
    }

    
    void Update()
    {
        if(isActive) ChangeIndex();
        
        if(isActive && Input.GetKeyDown(KeyCode.Escape) && afterTime >0.4f)
        {
            GameManager.sceneManager.LoadScene("StartMenu");
        } 
    }


    IEnumerator ChangeColor()
    {
        textLeft[currentIndex].color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        textLeft[currentIndex].color = Color.green;
        yield return new WaitForSeconds(waitTime);
        textLeft[currentIndex].color = Color.cyan;
        yield return new WaitForSeconds(waitTime);

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
                textLeft[currentIndex+1].color = Color.white;
                transform.position += new Vector3(0,25,0);
                panel.transform.position += new Vector3(0,25,0);
            }
        }
        else if(move == -1)
        {
            if(currentIndex != 11)
            {
                currentIndex++;
                
                textLeft[currentIndex-1].color = Color.white;
                transform.position += new Vector3(0,-25,0);
                panel.transform.position += new Vector3(0,-25,0);          
            }
        }
    }

    void ManageInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isActive = false;
            panelText.text = textLeft[currentIndex].text;
            panel.SetActive(true);
        }
    }

    
    void OnGUI()
    {
        afterTime += Time.deltaTime;
        if(pressedTime != 0.0f) pressedTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Return) && isActive && afterTime>0.4f)
        {
            isActive = false;
            panelText.text = textLeft[currentIndex].text;
            panel.SetActive(true);
            pressedTime += Time.deltaTime;

            if(currentIndex == 11)
            {
                GameManager.inputManager.InitDefault();
                GameManager.saveManager.SaveInput(GameManager.inputManager);
                GameManager.sceneManager.LoadScene("StartMenu");
                return; 
            }
        }
        Event keyEvet = Event.current;
        
        if(keyEvet!=null && keyEvet.isKey && pressedTime >0.4f && currentIndex!=11)
        {
            KeyCode newKey = keyEvet.keyCode;
            GameManager.inputManager[currentIndex] = newKey;
            GameManager.saveManager.SaveInput(GameManager.inputManager); 
            textRight[currentIndex].text = AdjustKey(newKey);
            isActive = true;
            panel.SetActive(false);
            pressedTime = 0f;
            afterTime = 0.0f;
        }
    }

    string AdjustKey(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.RightArrow:
                return "Right";
            case KeyCode.LeftArrow:
                return "Left";
            case KeyCode.UpArrow:
                return "Up";
            case KeyCode.DownArrow:
                return "Down";
            case KeyCode.Return:
                return "Enter";
            default:
                return key.ToString();
        }  
    }

}
