using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class Ground : EventGenerator
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
        targetController.IsGrounded = isIntersect || targetController.IsGrounded;
    }


    public override bool Equals(object other)
    {
        if(!(other is Ground))
            return false;
        Ground ground_ = (Ground) other;
        if(this.owner.Equals(ground_.owner) && this.id.Equals(ground_.id) && this.target.Equals(ground_.target)) return true;
        return false;
    }

}


}

