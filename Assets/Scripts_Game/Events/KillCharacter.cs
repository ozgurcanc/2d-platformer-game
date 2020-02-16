using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class KillCharacter : EventGenerator
{
    public int id;

    private PColliderGenerator targetCollider;
    private CharController targetController;
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
            targetController = target.GetComponent<CharController>();
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
            targetController.DieChar();
            isActive = false;
        }
    }


    public override bool Equals(object other)
    {
        if(!(other is KillCharacter))
            return false;
        KillCharacter char_ = (KillCharacter) other;
        if(this.owner.Equals(char_.owner) && this.id.Equals(char_.id) && this.target.Equals(char_.target)) return true;
        return false;
    }
}


}

