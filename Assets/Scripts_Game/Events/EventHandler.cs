using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class EventHandler
{   
    public static int id = 0;
    protected List<EventGenerator> events = new List<EventGenerator>();

    public void Add(EventGenerator _event)
    {
        events.Add(_event);
    }

    public void Remove(EventGenerator _event)
    {
        events.Remove(_event);        
    }

    public void Clear()
    {
        events.Clear();
    }

    public void UpdateEvent(float duration)
    {
        foreach(EventGenerator aEvent in events)
        {
            aEvent.ExecuteEvent(duration);
        }   
    }

}


}

