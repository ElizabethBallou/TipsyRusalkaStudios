using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    private Quaternion _camRotation;
    public GameObject lockPick;
    private Rigidbody2D _rb2d;

    public float forceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = lockPick.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _camRotation = transform.rotation;
        Vector3 constRotation = new Vector3(0, 0, (Input.GetAxis("RotateLock") * forceMultiplier));
        transform.Rotate(constRotation);
        lockPick.transform.rotation = _camRotation;

        //translate velocity from global to local
        var localVelocity = lockPick.transform.InverseTransformDirection(_rb2d.velocity);
        localVelocity.x = 0;
        _rb2d.velocity = transform.TransformDirection(localVelocity);
    }
}
