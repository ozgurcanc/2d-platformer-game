using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


namespace cyclone
{
public class Particle2D : MonoBehaviour
{
    public GameObject owner;
    public int id;

    protected Vector2D position;
    protected Vector2D velocity;
    protected Vector2D acceleration;

    public float damping=1;
    public float inverseMass=1;

    protected Vector2D forceAccum = new Vector2D();

    public Particle2D()
    {
        position =  new Vector2D();
        velocity = new Vector2D();
        acceleration = new Vector2D();
    }

    public Particle2D(Vector2D p,Vector2D v, Vector2D a)
    {
        position = p;
        velocity = v;
        acceleration = a;
    }

    public Particle2D(Particle2D a)
    {
        position = new Vector2D(a.Position);
        velocity = new Vector2D(a.Velocity);
        acceleration = new Vector2D(a.Acceleration);
        damping = a.Damping;
        inverseMass = a.InverseMass;
    }

    public Vector2D Position
    {
        set
        {
            position = value;
        }
        get
        {
            return position;
        }
    }

    public Vector2D Velocity
    {
        set
        {
            velocity = value;
        }
        get
        {
            return velocity;
        }
    }

    public Vector2D Acceleration
    {
        set
        {
            acceleration = value;
        }
        get
        {
            return acceleration;
        }
    }

    public float Damping
    {
        set
        {
            damping = value;
        }
        get
        {
            return damping;
        }
    }

    public float Mass
    {
        set
        {
            Assert.IsTrue(value != 0);
            inverseMass = 1/value;
        }
        get
        {
            Assert.IsTrue(inverseMass!= 0);
            return 1/inverseMass;
        }
    }

    public float InverseMass
    {
        set
        {
            inverseMass = value;
        }
        get
        {
            return inverseMass;
        }
    }

    

    public void SetPosition(float x, float y)
    {
        position.x = x;
        position.y = y;
    }

    public void SetVelocity(float x, float y)
    {
        velocity.x = x;
        velocity.y = y;
    }

    public void SetAcceleration(float x, float y)
    {
        acceleration.x = x;
        acceleration.y = y;
    }

    public bool HasFiniteMass()
    {
        return inverseMass>= 0.0f;
    }

    public void ClearAccumulator()
    {
        forceAccum = new Vector2D();
    }

    public void AddForce(Vector2D force)
    {
        forceAccum += force;
    }

    public void Integrate(float duration)
    {
        if(inverseMass <= 0.0f) return;

        Assert.IsTrue(duration > 0.0f);

        position.AddScaledVector(velocity,duration);

        Vector2D resultingAcc = new Vector2D(acceleration);

        resultingAcc.AddScaledVector(forceAccum,inverseMass);

        velocity.AddScaledVector(resultingAcc,duration);
        
        velocity *= Mathf.Pow(damping,duration);

        ClearAccumulator();
        
        owner.transform.position = (Vector3) position;
    }

   

    public override string ToString()
    {
        return "position:  " + position.ToString() + "\n" + 
                "velocity:  " + velocity.ToString() + "\n" +
                "acceleration:  " + acceleration.ToString() + "\n" ;
    } 


    public override bool Equals(object other)
    {
        if (!(other is Particle2D))
            return false;
        Particle2D particle1 = (Particle2D) other;
        if (this.owner.Equals(particle1.owner) && this.id.Equals(particle1.id))
            return true;
        return false;
    }

    void OnEnable()
    {
        position =  new Vector2D();
        velocity = new Vector2D();
        acceleration = new Vector2D();
        
        if(gameObject)
        {
            position = (Vector2D) transform.position;
            owner = gameObject;
            id = PPhysicsHandler.id;
            PPhysicsHandler.id += 1;
            World.physicsRegistry.Add(this);
        }
    }

    void OnDisable()
    {
        World.physicsRegistry.Remove(this);
    }

}































}
