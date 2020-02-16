using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
public class IntersectionTests 
{
    public static bool IntersectionTest(PColliderGenerator a, PColliderGenerator b)
    {
        if(a is PBoxCollider2D && b is PBoxCollider2D)
        {
            return BoxAndBox(a,b);
        }
        // after all engine collision possibilities
        return false;
    } 

    public static bool IntersectionTest(PColliderGenerator a, EventGenerator b)
    {
        if(a is PBoxCollider2D)
        {
            return BoxAndEvent(a,b);
        }
        return false;
    }
    
    public static bool IntersectionTest(EventGenerator b, PColliderGenerator a)
    {
        if(a is PBoxCollider2D)
        {
            return BoxAndEvent(a,b);
        }
        return false;
    }


    private static bool BoxAndBox(PColliderGenerator a, PColliderGenerator b)
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

        if(box1TopRight.x < box2BottomLeft.x || box2TopRight.x < box1BottomLeft.x) return false;
            
        if(box1TopRight.y < box2BottomLeft.y || box2TopRight.y < box1BottomLeft.y) return false;

        return true;
    } 

    private static bool BoxAndEvent(PColliderGenerator a, EventGenerator b)
    {
        PBoxCollider2D box1 = (PBoxCollider2D) a;
            
        Vector2D box1Origin = (Vector2D) box1.owner.transform.position;  
        Vector2D size = new Vector2D(box1.xSize,box1.ySize);
        Vector2D box1TopRight = box1Origin + size;
        Vector2D box1BottomLeft = box1Origin - size;

        Vector2D box2Origin = (Vector2D) b.owner.transform.position;  
        Vector2D size2 = new Vector2D(b.xSize,b.ySize);
        Vector2D box2TopRight = box2Origin + size2;
        Vector2D box2BottomLeft = box2Origin - size2;

        if(box1TopRight.x < box2BottomLeft.x || box2TopRight.x < box1BottomLeft.x) return false;
            
        if(box1TopRight.y < box2BottomLeft.y || box2TopRight.y < box1BottomLeft.y) return false;

        return true;
    }
}   
}

