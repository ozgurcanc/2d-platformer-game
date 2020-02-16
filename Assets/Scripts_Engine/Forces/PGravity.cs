using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class PGravity : PForceGenerator
{

    public GameObject owner;
    public int id;

    private Vector2D gravity;

    public float gravityX;
    public float gravityY;

    public override void UpdateForce(float duration)
    {
        Particle2D particle = owner.GetComponent<Particle2D>();

        if(!particle.HasFiniteMass()) return;
        particle.AddForce(gravity * particle.Mass);
    } 


    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = PForceHandler.id ;
            PForceHandler.id += 1;
            gravity = new Vector2D(gravityX,gravityY);

            World.forceRegistry.Add( (PForceGenerator) this);
        }
    }

    void OnDisable()
    {
        World.forceRegistry.Remove((PForceGenerator) this);
    }

    public override bool Equals(object other)
    {
        if (!(other is PGravity))
            return false;
        PGravity gravity1 = (PGravity) other;
        if (this.owner.Equals(gravity1.owner) && this.id.Equals(gravity1.id))
            return true;
        return false;
    }
}  
}


