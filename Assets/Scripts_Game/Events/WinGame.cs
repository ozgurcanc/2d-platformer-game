using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cyclone
{
public class WinGame : EventGenerator
{
    public int id;

    private PColliderGenerator targetCollider;
    public Animator anim;
    bool isActive = true;

    string upText = "A Game by";
    string downText =  "Özgürcan Çalışkan";

    
    public Text up;
    public Text down;

    string upText2 = "-Special Thanks-";
    string downText2 =  "You, the Player!";

    public Text up2;
    public Text down2;


    int index = 0;

    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);

            anim = gameObject.GetComponent<Animator>();
        }

        if(target)
        {
            targetCollider = (PColliderGenerator) target.GetComponent<PBoxCollider2D>();
        }
    }

    void OnDisable()
    {
        World.eventRegistry.Remove((EventGenerator) this);
    }

    public override void ExecuteEvent(float duration)
    {
        bool isIntersect = IntersectionTests.IntersectionTest(targetCollider,(EventGenerator) this);

        if(isIntersect && isActive)
        {
            isActive = false;
            StartCoroutine(FinishGame());
        }
    }

    public override bool Equals(object other)
    {
        if(!(other is WinGame))
            return false;
        WinGame key_ = (WinGame) other;
        if(this.owner.Equals(key_.owner) && this.id.Equals(key_.id) && this.target.Equals(key_.target)) return true;
        return false;
    }


    IEnumerator FinishGame()
    {
        target.GetComponent<SpriteRenderer>().enabled = false;
        target.GetComponent<CharController>().enabled = false;
        target.GetComponent<Particle2D>().enabled = false;
        anim.Play("win");
        yield return new WaitForSeconds(1.7f);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        for(int i=0;i<9;i++)
        {
            up.text += upText[i];
            yield return new WaitForSeconds(0.15f);
        }

        for(int i=0;i<17;i++)
        {
            down.text += downText[i];
            yield return new WaitForSeconds(0.15f);
        }
        
        for(int i=0;i<16;i++)
        {
            up2.text += upText2[i];
            yield return new WaitForSeconds(0.15f);
        }

        for(int i=0;i<16;i++)
        {
            down2.text += downText2[i];
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);
        GameManager.sceneManager.LoadScene("StartMenu");
    }

}


}

