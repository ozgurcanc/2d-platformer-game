using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class Key : EventGenerator
{
    public int id;

    private bool isActive = true;
    private Animator anim;
    private PColliderGenerator targetCollider;
    
    public Door door;

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

        if(!isActive || !isIntersect) return;

        anim.Play("keyPicked");
        anim.SetBool("isPicked",true);
        isActive = false;

        door.KeyCollected();
    }

    public override bool Equals(object other)
    {
        if(!(other is Key))
            return false;
        Key key_ = (Key) other;
        if(this.owner.Equals(key_.owner) && this.id.Equals(key_.id) && this.target.Equals(key_.target)) return true;
        return false;
    }

    
}

}


