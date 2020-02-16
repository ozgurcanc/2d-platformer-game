using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class PAnchoredBungee : PForceGenerator
{
    
    public GameObject owner;
    public int id;

    private Vector2D anchor;

    public float springConstant;
    public float restLength;
    public float AnchorX;
    public float AnchorY;

    public PAnchoredBungee(){}

    public PAnchoredBungee(Vector2D _anchor, float _springConstant, float _restLength)
    {
        anchor = _anchor;
        springConstant = _springConstant;
        restLength = _restLength;
    }

    public void init(Vector2D _anchor, float _springConstant, float _restLength)
    {
        anchor = _anchor;
        springConstant = _springConstant;
        restLength = _restLength;
    }
        
    public override void UpdateForce(float duration)
    {
        Particle2D particle = owner.GetComponent<Particle2D>();   
        Vector2D force = new Vector2D(particle.Position);
        force -= anchor;

        float magnitude = force.Magnitude;
        if(magnitude < restLength) return;
        magnitude = (magnitude - restLength) * springConstant;

        force.Normalize();
        force *= -magnitude;
        particle.AddForce(force);  
    }

    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = PForceHandler.id ;
            PForceHandler.id += 1;

            anchor = new Vector2D(AnchorX,AnchorY);
            
            World.forceRegistry.Add( (PForceGenerator) this);
        }
    }

    void OnDisable()
    {
        World.forceRegistry.Remove((PForceGenerator) this);
    }
    
    public override bool Equals(object other)
    {
        if (!(other is PAnchoredBungee))
        return false;
        PAnchoredBungee spring1 = (PAnchoredBungee) other;
        if (this.owner.Equals(spring1.owner) && this.id.Equals(spring1.id))
            return true;
        return false;
    }
}  

}


