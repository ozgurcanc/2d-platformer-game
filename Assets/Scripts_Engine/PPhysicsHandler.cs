using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PPhysicsHandler 
{
    public static int id = 0;
    protected List<Particle2D> particles = new List<Particle2D>();

    public void Add(Particle2D _p)
    {
        particles.Add(_p);
    }

    public void Remove(Particle2D _p)
    {
        particles.Remove(_p);        
    }

    public void Clear()
    {
        particles.Clear();
    }

    public void UpdatePhysics(float duration)
    {
        foreach(Particle2D aParticle in particles)
        {
            aParticle.Integrate(duration);
        }
    }

}
}

