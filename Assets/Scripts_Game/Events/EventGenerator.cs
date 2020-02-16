using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public abstract class EventGenerator : MonoBehaviour
{
    public float xSize;
    public float ySize; 
    public GameObject target;
    public GameObject owner;

    public abstract void ExecuteEvent(float duration);

    void OnDrawGizmos()
    {
        Vector3 origin = owner.transform.position;
        Vector3 topRight = origin + new Vector3(xSize,ySize,0);
        Vector3 topLeft = origin + new Vector3(-xSize,ySize,0);
        Vector3 bottomRight = origin + new Vector3(xSize,-ySize,0);
        Vector3 bottomLeft = origin + new Vector3(-xSize,-ySize,0);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(topRight,bottomRight);
        Gizmos.DrawLine(topRight,topLeft);
        Gizmos.DrawLine(topLeft,bottomLeft);
        Gizmos.DrawLine(bottomLeft,bottomRight);
    }

}


}


