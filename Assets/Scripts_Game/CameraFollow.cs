using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cyclone;

public class CameraFollow : MonoBehaviour
{   

    public GameObject target = null;

    private Vector3 offset;
    public float smoothConst = 5f;
    public float deadZoneX = 2f;
    public float deadZoneY = 2f;
    
    void Start()
    {
        offset = transform.position - target.transform.position;  
        Debug.Log(offset); 
    }

    void Update()
    {
        Vector3 lastCharPosition = transform.position - offset;
        Vector3 diff = target.transform.position - lastCharPosition;
        

        if(diff.x > deadZoneX)
        {
            lastCharPosition.x += diff.x - deadZoneX;
        }
        else if(diff.x < -deadZoneX)
        {
            lastCharPosition.x += diff.x + deadZoneX;
        }

        if(diff.y > deadZoneY)
        {
            lastCharPosition.y += diff.y - deadZoneX;
        }
        else if(diff.y < -deadZoneY)
        {
            lastCharPosition.y += diff.y + deadZoneX;
        }

        Vector3 nextCamPosition = lastCharPosition + offset;

        transform.position = Vector3.Lerp(transform.position,nextCamPosition,smoothConst * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Vector3 origin = target.transform.position;
        Vector3 topRight = origin + new Vector3(deadZoneX,deadZoneY,0);
        Vector3 topLeft = origin + new Vector3(-deadZoneX,deadZoneY,0);
        Vector3 bottomRight = origin + new Vector3(deadZoneX,-deadZoneY,0);
        Vector3 bottomLeft = origin + new Vector3(-deadZoneX,-deadZoneY,0);

        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(topRight,bottomRight);
        Gizmos.DrawLine(topRight,topLeft);
        Gizmos.DrawLine(topLeft,bottomLeft);
        Gizmos.DrawLine(bottomLeft,bottomRight);
    }
}
