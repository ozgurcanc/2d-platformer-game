using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class PCollisionHandler
{
    public static int id = 0;
    protected List<PColliderGenerator> colliders = new List<PColliderGenerator>();
    protected List<PContact> contacts = new List<PContact>();

    public void Add(PColliderGenerator _cg)
    {
        colliders.Add(_cg);
    }

    public void Remove(PColliderGenerator _cg)
    {
        colliders.Remove(_cg);  
    }

    public void Clear()
    {
        colliders.Clear();
    }

    public void UpdateCollisions(float duration)
    {
        World.gameWorld.SetupMap(colliders);
        contacts = World.gameWorld.GenerateContacts();

        foreach(PContact aContact in contacts )
        {
            aContact.Resolve(duration);
        }
        
        contacts.Clear();
        World.gameWorld.Clear();
    }
}


}

