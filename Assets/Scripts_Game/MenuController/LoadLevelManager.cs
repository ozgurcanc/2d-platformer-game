using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelManager : MonoBehaviour
{
    
    public Sprite[] levelImages;
    public Sprite[] abilityImages;
    public Image[] inGameImage; 

    public Image[] leftImages = new Image[5];
    public Image[] rightImages = new Image[5];

    public Text[] leftTexts = new Text[5];
    public Text[] rightTexts = new Text[5];

    public Text[] selectText = new Text[2];

    int currentIndex = 0;
    float waitTime=0.1f;

    List<Ability>[] levelsAbility = new List<Ability>[10];

    int levelLeft;
    int levelRight;

    public GameObject loadlevelUI;

    void Start()
    {
        StartCoroutine(StartUI());
    }

    void Update()
    {
        ChangeIndex();
        PickLevel();
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(waitTime);
        selectText[currentIndex].color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        selectText[currentIndex].color = Color.blue;
        yield return new WaitForSeconds(waitTime);
        selectText[currentIndex].color = Color.cyan;
        yield return new WaitForSeconds(waitTime);
        selectText[currentIndex].color = Color.red;

        StartCoroutine(ChangeColor());
    }

    void ChangeIndex()
    {
        int move=0;
        if(Input.GetKeyDown(KeyCode.RightArrow)) move = -1;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) move = 1;   

        if(move == 1)
        {
            if(currentIndex != 0)
            {
                currentIndex--;
                selectText[currentIndex+1].color = Color.black;
            }
        }
        else if(move == -1)
        {
            if(currentIndex != 1)
            {
                currentIndex++;
                selectText[currentIndex-1].color = Color.black;
            }
        }

    }

    enum Ability{
        Trampoline,
        Hook,
        RefreshDash,
        Key,
        Fly,
        Trophy,
        WallJump
    }

    void InitializeLevelAbility()
    {
        for(int i=0;i<10;i++)
        {
            levelsAbility[i]= new List<Ability>();
        }

        levelsAbility[0].Add(Ability.Fly);
        levelsAbility[0].Add(Ability.RefreshDash);

        levelsAbility[1].Add(Ability.Fly);
        levelsAbility[1].Add(Ability.Trampoline);

        levelsAbility[2].Add(Ability.Hook);
        levelsAbility[2].Add(Ability.WallJump);

        levelsAbility[3].Add(Ability.RefreshDash);

        levelsAbility[4].Add(Ability.RefreshDash);
        levelsAbility[4].Add(Ability.Trampoline);
        levelsAbility[4].Add(Ability.WallJump);

        levelsAbility[5].Add(Ability.Trampoline);
        
        levelsAbility[6].Add(Ability.Hook);
        
        levelsAbility[7].Add(Ability.RefreshDash);
        levelsAbility[7].Add(Ability.Key);
        levelsAbility[7].Add(Ability.Trampoline);

        levelsAbility[8].Add(Ability.RefreshDash);
        levelsAbility[8].Add(Ability.Hook);

        levelsAbility[9].Add(Ability.RefreshDash);
        levelsAbility[9].Add(Ability.Key);
        levelsAbility[9].Add(Ability.Trampoline);

    }

    void SetUpScene()
    {
        int levelsCount = GameManager.gameStats.uncompletedLevels.Count;
      
        levelLeft = (int) Random.Range(0f,levelsCount);
        levelRight = levelLeft;

        Debug.Log("currentLevel: "+GameManager.gameStats.currentLevel + 
                   "\n unComplevels: "+GameManager.gameStats.uncompletedLevels.Count +
                   "\n completeLevels: "+GameManager.gameStats.completedLevels.Count +
                  "\n levelsCount: "+levelsCount  +
                 "\n left level is : "+levelLeft +
                 "\n right level is : "+levelRight);


        while(levelRight == levelLeft)
        {
            levelRight = (int) Random.Range(0f,levelsCount);
        }

        
        levelLeft = GameManager.gameStats.uncompletedLevels[levelLeft];
        levelRight = GameManager.gameStats.uncompletedLevels[levelRight];

        inGameImage[0].sprite = levelImages[levelLeft-1];
        inGameImage[1].sprite = levelImages[levelRight-1];

        int index = 0;

        foreach(Ability aAbility in levelsAbility[levelLeft-1])
        {
            leftImages[index].enabled = true;
            leftTexts[index].enabled = true;

            switch (aAbility)
            {
                case Ability.Fly:
                    leftImages[index].sprite = abilityImages[0];
                    leftTexts[index].text = "Fly";
                    break;
                case Ability.Hook:
                    leftImages[index].sprite = abilityImages[1];
                    leftTexts[index].text = "Hook";
                    break;
                case Ability.Key:
                    leftImages[index].sprite = abilityImages[2];
                    leftTexts[index].text = "Key";
                    break;
                case Ability.RefreshDash:
                    leftImages[index].sprite = abilityImages[3];
                    leftTexts[index].text = "Refresh Dash";
                    break;
                case Ability.Trampoline:
                    leftImages[index].sprite = abilityImages[4];
                    leftTexts[index].text = "Trampoline";
                    break;
                case Ability.Trophy:
                    leftImages[index].sprite = abilityImages[5];
                    leftTexts[index].text = "Trophy";
                    break;
                case Ability.WallJump:
                    leftImages[index].sprite = abilityImages[6];
                    leftTexts[index].text = "WallJump";
                    break;                        
            }

            index++; 
        }
        
        for( ; index<5;index++)
        {
            leftImages[index].enabled = false;
            leftTexts[index].enabled = false;
        }

        index = 0;


        foreach(Ability aAbility in levelsAbility[levelRight-1])
        { 
            rightImages[index].enabled = true;
            rightTexts[index].enabled = true;

            switch (aAbility)
            {
                case Ability.Fly:
                    rightImages[index].sprite = abilityImages[0];
                    rightTexts[index].text = "Fly";
                    break;
                case Ability.Hook:
                    rightImages[index].sprite = abilityImages[1];
                    rightTexts[index].text = "Hook";
                    break;
                case Ability.Key:
                    rightImages[index].sprite = abilityImages[2];
                    rightTexts[index].text = "Key";
                    break;
                case Ability.RefreshDash:
                    rightImages[index].sprite = abilityImages[3];
                    rightTexts[index].text = "Refresh Dash";
                    break;
                case Ability.Trampoline:
                    rightImages[index].sprite = abilityImages[4];
                    rightTexts[index].text = "Trampoline";
                    break;
                case Ability.Trophy:
                    rightImages[index].sprite = abilityImages[5];
                    rightTexts[index].text = "Trophy";
                    break;
                case Ability.WallJump:
                    rightImages[index].sprite = abilityImages[6];
                    rightTexts[index].text = "WallJump";
                    break;                        
            }
            index++; 
        }
        
        for( ; index<5;index++)
        {
            rightImages[index].enabled = false;
            rightTexts[index].enabled = false;
        }

    }

    void PickLevel()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(currentIndex == 0) GameManager.gameStats.currentLevel = levelLeft;
            else if(currentIndex == 1) GameManager.gameStats.currentLevel = levelRight;
            loadlevelUI.SetActive(false);

            GameManager.saveManager.SaveGame(GameManager.gameStats);
            GameManager.sceneManager.LoadScene("Level"+GameManager.gameStats.currentLevel.ToString());

        }
    }


    IEnumerator StartUI()
    {
        loadlevelUI.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        loadlevelUI.SetActive(true);
        InitializeLevelAbility();
        SetUpScene();

        foreach(Image aImage in leftImages)
        {
            aImage.enabled = false;
        }
        foreach(Image aImage in rightImages)
        {
            aImage.enabled = false;
        }

        StartCoroutine(ChangeColor());
    }
}
