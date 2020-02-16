using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class Hook : EventGenerator
{
    public int id;

    private bool isActive;
    private Particle2D targetParticle;
    private PColliderGenerator targetCollider;
    private Renderer renderer;


    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 10;
    
    private LineRenderer lineRenderer;

    private Vector2D anchor;
    public float springConstant = 100f;
    public float restLength = 0.0f;

    private PAnchoredBungee hookForce;

    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);

            renderer = gameObject.GetComponent<Renderer>();
        }

        if(target)
        {
            targetParticle = target.GetComponent<Particle2D>();
            targetCollider = (PColliderGenerator) target.GetComponent<PBoxCollider2D>();
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        lineRenderer.enabled = false;

        anchor = new Vector2D(transform.position.x, transform.position.y);

        hookForce = new PAnchoredBungee(anchor,springConstant,restLength);
        hookForce.owner = target;
    }

    void OnDisable()
    {
        World.eventRegistry.Remove((EventGenerator) this);
    }

    public override void ExecuteEvent(float duration)
    {
        isActive = Input.GetKey(GameManager.inputManager.hook);
        bool isIntersect = IntersectionTests.IntersectionTest(targetCollider,(EventGenerator) this);

        if(isIntersect) renderer.material.color = Color.green;
        else renderer.material.color = Color.red;

        if(!isIntersect || !isActive)
        {
          lineRenderer.enabled = false;
          return;  
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,target.transform.position);
        
        hookForce.UpdateForce(duration);
    }


    public override bool Equals(object other)
    {
        if(!(other is Hook))
            return false;
        Hook hook_ = (Hook) other;
        if(this.owner.Equals(hook_.owner) && this.id.Equals(hook_.id) && this.target.Equals(hook_.target)) return true;
        return false;
    }
    
}




}
