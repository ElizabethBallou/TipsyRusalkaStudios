using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public Transform _newPos;
    private Vector3 defaultPos;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, defaultPos.x, _newPos.position.x), Mathf.Clamp(transform.position.y, defaultPos.y, _newPos.position.y), 0f);
    }
}
