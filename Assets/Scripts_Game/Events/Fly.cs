using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class Fly : EventGenerator
{
    public int id;

    private PColliderGenerator targetCollider;
    private CharController targetController;


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

        if(isIntersect)
        {
            targetController.SetUpFly();
            Destroy(gameObject);
        }
    }

    public override bool Equals(object other)
    {
        if(!(other is Fly))
            return false;
        Fly fly_ = (Fly) other;
        if(this.owner.Equals(fly_.owner) && this.id.Equals(fly_.id) && this.target.Equals(fly_.target)) return true;
        return false;
    }

}


}

