using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class Trampoline : EventGenerator
{
    
    public int id;
    public float yMoveConst = 20f; 
    public float xMoveConst = 15f;
    public bool actX = false;
    public bool actY = false;


    private bool isActive;
    private Animator anim;
    private Particle2D targetParticle;
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

            anim = gameObject.GetComponent<Animator>();
        }

        if(target)
        {
            targetParticle = target.GetComponent<Particle2D>();
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
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle") isActive =true;
        else isActive = false;

        bool isIntersect = IntersectionTests.IntersectionTest(targetCollider,(EventGenerator) this);

        if(!isIntersect || !isActive) return;

        if(actY) targetParticle.Velocity.y = yMoveConst;
        if(actX) targetParticle.Velocity.x = xMoveConst;
        StartCoroutine(DisableDrag());
        targetController.RefreshDash();
        anim.Play("tramboline");
    }

    public override bool Equals(object other)
    {
        if(!(other is Trampoline))
            return false;
        Trampoline trampoline_ = (Trampoline) other;
        if(this.owner.Equals(trampoline_.owner) && this.id.Equals(trampoline_.id) && this.target.Equals(trampoline_.target)) return true;
        return false;
    }

    IEnumerator DisableDrag()
    {
        targetController.StopDash();
        targetController.StopFly();
        targetController.IsGrounded = true;
        if(actX) targetParticle.GetComponent<PDrag>().enabled = false;
      //  targetParticle.GetComponent<PGravity>().enabled = true;
        yield return new WaitForSeconds(1f);
        if(actX) targetParticle.GetComponent<PDrag>().enabled = true;
      //  targetParticle.GetComponent<PGravity>().enabled = true;
        yield return null;
    }
}






}
