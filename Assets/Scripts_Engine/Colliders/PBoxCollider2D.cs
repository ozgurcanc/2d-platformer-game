using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
public class PBoxCollider2D : PColliderGenerator
{
    public GameObject owner;
    public int id;
    public bool IsTrigger;
    public float xSize;
    public float ySize; 
    public bool isWallY=false;
    public bool isWallX=false;


    void OnEnable()
    {
        if(gameObject)
        {
            owner = gameObject;
            id = PCollisionHandler.id;
            PCollisionHandler.id+=1;
            World.colliderRegistry.Add((PColliderGenerator) this);
            
        }
    }


    void OnDisable()
    {
        World.colliderRegistry.Remove((PColliderGenerator) this);
    }


    public override bool Equals(object other)
    {
        if(!(other is PBoxCollider2D))
            return false;
        PBoxCollider2D pBoxCollider2D = (PBoxCollider2D) other;
        if(this.owner.Equals(pBoxCollider2D.owner) && this.id.Equals(pBoxCollider2D.id)) return true;
        return false;
    }



    void OnDrawGizmos()
    {
        Vector3 origin = owner.transform.position;
        Vector3 topRight = origin + new Vector3(xSize,ySize,0);
        Vector3 topLeft = origin + new Vector3(-xSize,ySize,0);
        Vector3 bottomRight = origin + new Vector3(xSize,-ySize,0);
        Vector3 bottomLeft = origin + new Vector3(-xSize,-ySize,0);

        Color color = Color.red;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topRight,bottomRight);
        Gizmos.DrawLine(topRight,topLeft);
        Gizmos.DrawLine(topLeft,bottomLeft);
        Gizmos.DrawLine(bottomLeft,bottomRight);
    }
}

}


