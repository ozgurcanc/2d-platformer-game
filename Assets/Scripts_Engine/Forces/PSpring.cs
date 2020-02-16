using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PSpring : PForceGenerator
{
    public GameObject owner;
    public int id;

    public Particle2D anchor;
    
    public float springConstant;
    public float restLength;

    public PSpring(){}

    public PSpring(Particle2D _anchor, float _springConstant, float _restLength)
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
        if (!(other is PSpring))
        return false;
        PSpring spring1 = (PSpring) other;
        if (this.owner.Equals(spring1.owner) && this.id.Equals(spring1.id))
            return true;
        return false;
    }
   
   
} 


}

