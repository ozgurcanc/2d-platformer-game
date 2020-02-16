using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class CompleteLevel : EventGenerator
{
    
    public int id;

    private PColliderGenerator targetCollider;

    bool isActive = true;
    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);
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
            
            GameManager.gameStats.completedLevels.Add(GameManager.gameStats.currentLevel);
            GameManager.gameStats.uncompletedLevels.Remove(GameManager.gameStats.currentLevel);

            if(GameManager.gameStats.completedLevels.Count == 6)
            {
                GameManager.sceneManager.LoadScene("Win");
            }
            else 
            {
                GameManager.sceneManager.LoadScene("LoadLevel");
            }
        }
    }

    public override bool Equals(object other)
    {
        if(!(other is CompleteLevel))
            return false;
        CompleteLevel key_ = (CompleteLevel) other;
        if(this.owner.Equals(key_.owner) && this.id.Equals(key_.id) && this.target.Equals(key_.target)) return true;
        return false;
    }
}
   
}

