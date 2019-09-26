using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorManager : MonoBehaviour
{
    public Transform slidingWall;
    public Transform pos1;
    public Transform pos2;

    private Vector3 _newPos;

    public string currentState;
    
    public float speed;
    public float resetTime;
    
    void Start()
    {
        ChangeTargetPos();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Physics.IgnoreLayerCollision(8, 9);
        slidingWall.position = Vector3.Lerp(slidingWall.position, _newPos, speed * Time.deltaTime);
    }

    private void ChangeTargetPos()
    {
        switch (currentState)
        {
            case "Moving to position 2":
                currentState = "Moving to position 1";
                _newPos = pos1.position;
                break;
            case "Moving to position 1":
                currentState = "Moving to position 2";
                _newPos = pos2.position;
                break;
            case "":
                currentState = "Moving to position 2";
                _newPos = pos2.position;
                break;
            default:
                break;
        }
        
        Invoke(nameof(ChangeTargetPos), resetTime);
    }
}
