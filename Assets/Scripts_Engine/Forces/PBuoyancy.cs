using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PBuoyancy : PForceGenerator
{
    public GameObject owner;
    public int id; 

    public Particle2D waterCenter;
    public float halfWaterHeight;
    public float halfHeight;
    public float volume;
    public float liquidDensity;

    public PBuoyancy(Particle2D _waterCenter, float _halfWaterHeight, float _halfHeight, float _volume, float _liquidDensity)
    {
        waterCenter = _waterCenter;
        halfWaterHeight = _halfWaterHeight;
        halfHeight = _halfHeight;
        volume = _volume;
        liquidDensity = _liquidDensity;
    }

    public override void UpdateForce( float duration)
    {
        Particle2D particle = owner.GetComponent<Particle2D>();  
        float waterLevel = waterCenter.Position.y + halfWaterHeight;
        float depth = particle.Position.y;

        if(depth >= waterLevel + halfHeight) return;
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
        if (!(other is PBuoyancy))
        return false;
        PBuoyancy spring1 = (PBuoyancy) other;
        if (this.owner.Equals(spring1.owner) && this.id.Equals(spring1.id))
            return true;
        return false;
    }

}





}

