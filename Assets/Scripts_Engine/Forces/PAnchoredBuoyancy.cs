using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PAnchoredBuoyancy : PForceGenerator
{
    public GameObject owner;
    public int id; 

    public float waterLevel;
    public float halfHeight;
    public float volume;
    public float liquidDensity;

    public PAnchoredBuoyancy(float _halfHeight, float _volume, float _waterLevel, float _liquidDensity)
    {
        halfHeight = _halfHeight;
        volume = _volume;
        waterLevel = _waterLevel;
        liquidDensity = _liquidDensity;
    }

    public override void UpdateForce(float duration)
    {
        Particle2D particle = owner.GetComponent<Particle2D>();    
        float depth = particle.Position.y;

        if(depth >= waterLevel+ halfHeight) return;
        Vector2D force = new Vector2D();

        if(depth <= waterLevel - halfHeight)
        {
            force.y = liquidDensity * volume;
            particle.AddForce(force);
            return;
        }

        force.y = liquidDensity * volume * ((depth - halfHeight - waterLevel)/(2*halfHeight));
        particle.AddForce(-force);
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
        if (!(other is PAnchoredBuoyancy))
        return false;
        PAnchoredBuoyancy spring1 = (PAnchoredBuoyancy) other;
        if (this.owner.Equals(spring1.owner) && this.id.Equals(spring1.id))
            return true;
        return false;
    }
           

}   





}

