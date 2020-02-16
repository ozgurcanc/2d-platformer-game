using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class GenerateContacts 
{
    public static PContact GenerateContact(PColliderGenerator a, PColliderGenerator b)
    {
        if(a is PBoxCollider2D && b is PBoxCollider2D )
        {
            return BoxAndBox(a,b);
        }
        
        // after all engine collision possiblities
        return null;
    } 

    private static PContact BoxAndBox(PColliderGenerator a, PColliderGenerator b)
    {       
        PBoxCollider2D box1 = (PBoxCollider2D) a;
        PBoxCollider2D box2 = (PBoxCollider2D) b;
            
        Vector2D box1Origin = (Vector2D) box1.owner.transform.position;  
        Vector2D size = new Vector2D(box1.xSize,box1.ySize);
        Vector2D box1TopRight = box1Origin + size;
        Vector2D box1BottomLeft = box1Origin - size;

        Vector2D box2Origin = (Vector2D) box2.owner.transform.position;  
        Vector2D size2 = new Vector2D(box2.xSize,box2.ySize);
        Vector2D box2TopRight = box2Origin + size2;
        Vector2D box2BottomLeft = box2Origin - size2;

        float penetrationX;
        float penetrationY;

        if(box2BottomLeft.x > box1BottomLeft.x && box2BottomLeft.x < box1TopRight.x && box2TopRight.x > box1TopRight.x )
        {
            penetrationX = Mathf.Abs(box1TopRight.x - box2BottomLeft.x);
        }
        else if(box2TopRight.x > box1BottomLeft.x && box2TopRight.x < box1TopRight.x && box2BottomLeft.x < box1BottomLeft.x )
        {
            penetrationX = Mathf.Abs(box2TopRight.x - box1BottomLeft.x);
        }
        else
        {
            penetrationX = 0;
        }

        if(box2BottomLeft.y > box1BottomLeft.y && box2BottomLeft.y < box1TopRight.y && box2TopRight.y > box1TopRight.y )
        {
            penetrationY = Mathf.Abs(box1TopRight.y - box2BottomLeft.y);
        }
        else if(box2TopRight.y > box1BottomLeft.y && box2TopRight.y < box1TopRight.y && box2BottomLeft.y < box1BottomLeft.y )
        {
            penetrationY = Mathf.Abs(box2TopRight.y - box1BottomLeft.y);
        }
        else
        {
            penetrationY = 0;
        }

        
        if(box1Origin.x < box2Origin.x) penetrationX = -penetrationX;
        if(box1Origin.y < box2Origin.y) penetrationY = -penetrationY;

        Vector2D contactNormal;

        if(box1.isWallX || box2.isWallX)
            contactNormal = new Vector2D(0,penetrationY);
        else if(box1.isWallY || box2.isWallY)
            contactNormal = new Vector2D(penetrationX,0);
        else
            contactNormal = new Vector2D(penetrationX,penetrationY);
        

        float penetration = contactNormal.Magnitude;

        contactNormal.Normalize();

        Particle2D p1 = box1.owner.GetComponent<Particle2D>();
        Particle2D p2 = box2.owner.GetComponent<Particle2D>();

        PContact newContact = new PContact();
        newContact.particle[0] = p1;
        newContact.particle[1] = p2;
        newContact.contactNormal = contactNormal;
        newContact.penetration = penetration + 0.0000000001f; // to be able to move 
            
        if(box1.isWallX || box2.isWallX || box1.isWallY || box2.isWallY)
            newContact.restitution = 0;
        else
            newContact.restitution = 1;

        return newContact;
    }
    
}

}

