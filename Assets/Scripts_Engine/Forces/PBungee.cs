using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
    public class PBungee : PForceGenerator
{
    public GameObject owner;
    public int id;

    public Particle2D anchor;
    
    public float springConstant;
    public float restLength;

    public PBungee (){}

    public PBungee(Particle2D _anchor, float _springConstant, float _restLength)
    {
        anchor = _anchor;
        springConstant = _springConstant;
        restLength = _restLength;
    }

    public void Init(Particle2D _anchor, float _springConstant, float _restLength)
    {
        anchor = _anchor;
        springConstant = _springConstant;
        restLength = _restLength;
    }

    public override void UpdateForce(float duration)
    {
        Particle2D particle = owner.GetComponent<Particle2D>();
        Vector2D force = new Vector2D(particle.Position);
        force -= anchor.Position;

        float magnitude = force.Magnitude;
        if(magnitude <= restLength) return;
        magnitude = Mathf.Abs(restLength - magnitude) * springConstant;

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
            
            World.forceRegistry.Add( (PForceGenerator) this);
        }
    }

 
    void OnDisable()
    {
        World.forceRegistry.Remove((PForceGenerator) this);
    }

    public override bool Equals(object other)
    {
        if (!(other is PBungee))
        return false;
        PBungee spring1 = (PBungee) other;
        if (this.owner.Equals(spring1.owner) && this.id.Equals(spring1.id))
            return true;
        return false;
    }
}

}

