using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateSpace : MonoBehaviour
{
    public Transform nw;
    public Transform ne;
    public Transform se;
    public Transform sw;
    public Transform player;
    
    private float angleLeft;
    private float angleRight;

    private Vector3 minScale = new Vector3(0.16f, 0.16f, 0.16f);
    private Vector3 maxScale = new Vector3(0.45f, 0.45f, 0.45f);

    // Start is called before the first frame update
    void Start()
    {
        angleLeft = Mathf.Atan( (nw.position.x - sw.position.x)/(nw.position.y - sw.position.y)) * Mathf.Rad2Deg;
        angleRight = Mathf.Atan((ne.position.x - se.position.x)/(ne.position.y - se.position.y)) * Mathf.Rad2Deg;

        print(angleLeft);
        print(angleRight);
    }

    // Update is called once per frame
    void Update()
    {
        var playerAngleLeft = Mathf.Atan((nw.position.x - player.position.x)/(nw.position.y - player.position.y)) * Mathf.Rad2Deg;
        var playerAngleRight = Mathf.Atan((ne.position.x - player.position.x)/(ne.position.y - player.position.y)) * Mathf.Rad2Deg;

        if (playerAngleLeft >= angleLeft || playerAngleRight <= angleRight || player.position.y > nw.position.y)
        {
            PlayerController.instance.newPos = player.transform.position;
            //print("You've crossed the line mister!");
        }
        
        //print("left: " + playerAngleLeft);
        //print("right: " + playerAngleRight);

        var lerpRate = ((player.transform.position.y - sw.position.y) / (nw.position.y - sw.position.y));
        var playerScale = Vector3.Lerp(maxScale, minScale, lerpRate);
        player.localScale = playerScale;
    }
}
