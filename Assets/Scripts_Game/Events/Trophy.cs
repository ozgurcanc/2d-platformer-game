using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class Trophy : EventGenerator
{
    public int id;
    private PColliderGenerator targetCollider;

    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);
        }

        if(target){
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

        if(isIntersect)
        {
            GameManager.gameStats.trophyCount++;
            Destroy(gameObject);
        }
    }

    public override bool Equals(object other)
    {
        if(!(other is Trophy))
            return false;
        Trophy trophy_ = (Trophy) other;
        if(this.owner.Equals(trophy_.owner) && this.id.Equals(trophy_.id) && this.target.Equals(trophy_.target)) return true;
        return false;
    }

}

}

