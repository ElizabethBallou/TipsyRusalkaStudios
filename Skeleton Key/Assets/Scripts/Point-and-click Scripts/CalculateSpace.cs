using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateSpace : MonoBehaviour
{
    private Camera cam;

    #region RoomTransforms
    public Transform nw;
    public Transform ne;
    public Transform trough;
    #endregion
    
    public Vector2 newPos;
    public Vector2 midpoint;
    public Vector2 origin;
    public float radius;
    public float distance;

    public static CalculateSpace instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        cam = Camera.main;
        
        CalculateHorizon();
    }

    public void SetTargetPosition()
    {
        newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(newPos, origin);
    }
    
    public void DistanceCheck()
    {
        distance = Vector2.Distance(newPos, origin);
    }

    public bool TargetIsFloor()
    {
        #region Old code for angles of trapezoid-shaped room.
        //float angleLeft = Mathf.Atan( (nw.position.x - sw.position.x)/(nw.position.y - sw.position.y)) * Mathf.Rad2Deg;
        //float angleRight = Mathf.Atan((ne.position.x - se.position.x)/(ne.position.y - se.position.y)) * Mathf.Rad2Deg;
        
        /*var targetPos = newPos;
        var targetAngleLeft = Mathf.Atan((nw.position.x - targetPos.x)/(nw.position.y - targetPos.y)) * Mathf.Rad2Deg;
        var targetAngleRight = Mathf.Atan((ne.position.x - targetPos.x)/(ne.position.y - targetPos.y)) * Mathf.Rad2Deg;

        if (targetAngleLeft >= angleLeft || targetAngleRight <= angleRight || targetPos.y > nw.position.y)
        {
            return true;
        }*/
        #endregion

        return (distance > radius && newPos.y < nw.position.y);
    }

    public void CalculateHorizon()
    {
        midpoint = new Vector2((nw.position.x + ne.position.x)/2, (nw.position.y + ne.position.y)/2);
        
        trough.transform.position = new Vector2(midpoint.x, trough.transform.position.y);
        
        float yCoordinate = (Mathf.Pow(nw.position.x, 2) - 2*nw.position.x*midpoint.x + Mathf.Pow(nw.position.y, 2) - Mathf.Pow(trough.position.x, 2) + 2*trough.position.x*midpoint.x - Mathf.Pow(trough.position.y, 2))
                            /(2*nw.position.y - 2*trough.position.y);
        //Debug.Log("y-coordinate: " + yCoordinate);

        origin = new Vector2(midpoint.x, yCoordinate);
        
        radius = Mathf.Sqrt((Mathf.Pow((nw.position.x - midpoint.x), 2) + (Mathf.Pow(nw.position.y - yCoordinate, 2))));
        //Debug.Log("radius: " + radius);
    }
}
