using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public static SceneTransition sceneManager = new SceneTransition();
    public static SaveManager saveManager = new SaveManager();
    public static GameStats gameStats = null;
    public static List<int> allLevels = new List<int>();
    public static InputManager inputManager = null;

    public int count = 0;
    public int levelCount=10;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        for(int i=1; i<=levelCount; i++)
        {
            allLevels.Add(i);
        }
 
    }
    
    void Start()
    {
        InputManager loadedInput = saveManager.LoadInput();

        if(loadedInput!= null)
        {
            inputManager = loadedInput;
        }
        else if(loadedInput == null)
        {
            InputManager defaultInput = new InputManager();
            defaultInput.InitDefault();
            inputManager = defaultInput;
            saveManager.SaveInput(inputManager);
        }
    }



}
