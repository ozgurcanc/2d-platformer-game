using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class PForceHandler 
{
    public static int id = 0;
    protected List<PForceGenerator> forces = new List<PForceGenerator>();

    public void Add(PForceGenerator _fg)
    {
        forces.Add(_fg);
    }

    public void Remove(PForceGenerator _fg)
    {
        forces.Remove(_fg);        
    }

    public void Clear()
    {
        forces.Clear();
    }

    public void UpdateForces(float duration)
    {
        foreach(PForceGenerator aForce in forces)
        {
            aForce.UpdateForce(duration);

        }
    }

}





}

