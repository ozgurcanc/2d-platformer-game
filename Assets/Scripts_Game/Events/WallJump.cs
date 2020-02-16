using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class WallJump : EventGenerator
{
    public int id;

    private Particle2D targetParticle;
    private PColliderGenerator targetCollider;
    private PDrag targetDrag;
    private CharController targetController;

    int left=0;

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
            targetParticle = target.GetComponent<Particle2D>();
            targetCollider = (PColliderGenerator) target.GetComponent<PBoxCollider2D>();
            targetDrag = target.GetComponent<PDrag>();
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

        if(isIntersect && Input.GetKeyDown(KeyCode.W))
        {
            if(target.transform.position.x > gameObject.transform.position.x) left = 1;
            else left = -1;
            StartCoroutine(IWallJump());
        }
    } 


    public override bool Equals(object other)
    {
        if(!(other is WallJump))
            return false;
        WallJump climb_ = (WallJump) other;
        if(this.owner.Equals(climb_.owner) && this.id.Equals(climb_.id) && this.target.Equals(climb_.target)) return true;
        return false;
    }

    IEnumerator IWallJump()
    {
        targetParticle.Velocity = new Vector2D(6f*left,12f);
        targetDrag.enabled = false;
        targetController.WallJump = true;
        yield return new WaitForSeconds(0.3f);
        targetDrag.enabled = true;
        targetController.WallJump = false;
        yield return null;
    }


}



}

