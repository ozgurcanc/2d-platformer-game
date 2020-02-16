using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PContact 
{
    public Particle2D[] particle = new Particle2D[2];

    public float penetration;

    public Vector2D contactNormal;

    public Vector2D[] particleMovement = new Vector2D[2];

    public float restitution;


    private void ResolveVelocity(float duration)
    {
        float separatingVelocity = CalculateSeparatingVelocity();

        if(separatingVelocity > 0) return;

        float newSepVelocity = -separatingVelocity * restitution;

        Vector2D accCausedVelocity = new Vector2D(particle[0].Acceleration);
        accCausedVelocity -= new Vector2D(particle[1].Acceleration);

        float accCausedSepVelocity = accCausedVelocity * contactNormal * duration;

        if(accCausedSepVelocity < 0)
        {
            newSepVelocity += restitution * accCausedSepVelocity;

            if (newSepVelocity < 0) newSepVelocity = 0;
        }

        float deltaVelocity = newSepVelocity - separatingVelocity;

        float totalInverseMass = particle[0].InverseMass;
        totalInverseMass += particle[1].InverseMass;

        if (totalInverseMass <= 0) return;

        float impulse = deltaVelocity / totalInverseMass;

        Vector2D impulsePerIMass = contactNormal * impulse;

        particle[0].Velocity += impulsePerIMass * particle[0].InverseMass;
        particle[1].Velocity -= impulsePerIMass * particle[1].InverseMass;

    }

    private void ResolveInterpenetration(float duration)
    {
        if (penetration <= 0) return;

        float totalInverseMass = particle[0].InverseMass;
        totalInverseMass += particle[1].InverseMass;

        if (totalInverseMass <= 0) return;

        Vector2D movePerIMass = contactNormal * (penetration / totalInverseMass);

        movePerIMass.y = 0;

        particleMovement[0] = movePerIMass * particle[0].InverseMass;
        particleMovement[1] = movePerIMass * -particle[1].InverseMass;

        particle[0].Position = particle[0].Position + particleMovement[0];
        particle[1].Position = particle[1].Position + particleMovement[1];
        
    }

    public void Resolve(float duration)
    {
        ResolveVelocity(duration);
        ResolveInterpenetration(duration);
    }

    private float CalculateSeparatingVelocity()
    {
        Vector2D relativeVelocity = new Vector2D(particle[0].Velocity);
        relativeVelocity -= new Vector2D(particle[1].Velocity);

        return relativeVelocity * contactNormal;
    }
}    
}

