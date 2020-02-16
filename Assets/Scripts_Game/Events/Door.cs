using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class Door : EventGenerator
{   
    public int id;
    
    public int neededKey; 
    private int currentKey = 0;

    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = EventHandler.id;
            EventHandler.id+=1;
            World.eventRegistry.Add((EventGenerator) this);
        }
    }

    void OnDisable()
    {
        World.eventRegistry.Remove((EventGenerator) this);
    }

    public void KeyCollected(){
        currentKey++;
    }

    public override void ExecuteEvent(float duration)
    {
        if(currentKey >= neededKey)
        {
            gameObject.SetActive(false);
        }
    }

    public override bool Equals(object other)
    {
        if(!(other is Door))
            return false;
        Door door_ = (Door) other;
        if(this.owner.Equals(door_.owner) && this.id.Equals(door_.id) && this.target.Equals(door_.target)) return true;
        return false;
    }

}    
}

