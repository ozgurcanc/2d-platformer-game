using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
    public class World : MonoBehaviour
{
    public static Map gameWorld = new Map(new Vector2D(0,0),100,100,100,100,50,50);
    public static PPhysicsHandler physicsRegistry = new PPhysicsHandler();
    public static PForceHandler forceRegistry = new PForceHandler();
    public static PCollisionHandler colliderRegistry = new PCollisionHandler();
    public static EventHandler eventRegistry = new EventHandler();

    public static bool isPaused = false;

    void Start()
    {
        isPaused = false;
    }

    void FixedUpdate()
    {   
        if(!isPaused)
        {
            eventRegistry.UpdateEvent(Time.deltaTime);

            forceRegistry.UpdateForces(Time.deltaTime);
            physicsRegistry.UpdatePhysics(Time.deltaTime);
            colliderRegistry.UpdateCollisions(Time.deltaTime);   
        }   
    }
}

}

