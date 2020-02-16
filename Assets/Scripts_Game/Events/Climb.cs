using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class Climb : EventGenerator
{   
    public int id;
    public float yMoveConst = 0.5f; 

    private bool isActive;

    private Particle2D targetParticle;
    private PColliderGenerator targetCollider;
    private PGravity targetGravity;
    private PDrag targetDrag;
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
            targetParticle = target.GetComponent<Particle2D>();
            targetCollider = (PColliderGenerator) target.GetComponent<PBoxCollider2D>();
            targetGravity = target.GetComponent<PGravity>();
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
        isActive = Input.GetKey(GameManager.inputManager.climb);

        bool isIntersect = IntersectionTests.IntersectionTest(targetCollider,(EventGenerator) this);

        if(Input.GetKeyDown(GameManager.inputManager.jump)) isActive = false; 
        
        if(!isIntersect || !isActive)
        {
            if(!targetController.IsFlyEnabled) targetGravity.enabled = true;
            targetDrag.isNotActingY = true; 
            return;
        }

        targetGravity.enabled = false;
        targetDrag.isNotActingY = false;
        
        targetController.WallJump = true; 

        float yMove = Input.GetAxisRaw("Vertical");
        yMove *= yMoveConst;

        targetParticle.Velocity = targetParticle.Velocity + new Vector2D(0,yMove);
    }


    public override bool Equals(object other)
    {
        if(!(other is Climb))
            return false;
        Climb climb_ = (Climb) other;
        if(this.owner.Equals(climb_.owner) && this.id.Equals(climb_.id) && this.target.Equals(climb_.target)) return true;
        return false;
    }
}   
}

