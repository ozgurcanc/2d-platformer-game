using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{

public class PDrag : PForceGenerator
{
    public GameObject owner;
    public int id;

    public float k1;
    public float k2;
    public bool isNotActingX;
    public bool isNotActingY;

    public PDrag(float _k1, float _k2)
    {
        k1 = _k1;
        k2 = _k2;
    }

    public override void UpdateForce( float duration)
    {  
        Particle2D particle = owner.GetComponent<Particle2D>();

        Vector2D force = new Vector2D(particle.Velocity);

        float dragCoeff = force.Magnitude;

        dragCoeff = k1 * dragCoeff + k2 * dragCoeff * dragCoeff;

        force.Normalize();
        force *= -dragCoeff;

        if(isNotActingX) force.x = 0;
        if(isNotActingY) force.y = 0;
        
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
        if (!(other is PDrag))
        return false;
        PDrag drag1 = (PDrag) other;
        if (this.owner.Equals(drag1.owner) && this.id.Equals(drag1.id))
            return true;
        return false;
    }

}   

}


