using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class RefreshDash : EventGenerator
{   
    public int id;
    private CharController targetController;
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

        if(target)
        {
            targetController = target.GetComponent<CharController>();
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

        if(!isIntersect) return;

        targetController.RefreshDash();

        Destroy(gameObject);
    }

    public override bool Equals(object other)
    {
        if(!(other is RefreshDash))
            return false;
        RefreshDash rDash_ = (RefreshDash) other;
        if(this.owner.Equals(rDash_.owner) && this.id.Equals(rDash_.id) && this.target.Equals(rDash_.target)) return true;
        return false;
    }


}


}

