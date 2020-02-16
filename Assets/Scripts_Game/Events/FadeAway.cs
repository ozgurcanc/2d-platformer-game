using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class FadeAway : EventGenerator
{   
    public int id;
    private PColliderGenerator targetCollider;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite1;
    public Sprite sprite2;
    
    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        if(isIntersect) StartCoroutine(IFadeAway());
    }

    public override bool Equals(object other)
    {
        if(!(other is FadeAway))
            return false;
        FadeAway key_ = (FadeAway) other;
        if(this.owner.Equals(key_.owner) && this.id.Equals(key_.id) && this.target.Equals(key_.target)) return true;
        return false;
    }

    IEnumerator IFadeAway()
    {
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = sprite1;
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = sprite2;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield return null;
    }
}

}

