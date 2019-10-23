using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private Material normalSpriteMat;

    private Material outlineSpriteMat;

    private SpriteRenderer _spriteRenderer;

    public string descriptionKey;
    // Start is called before the first frame update
    void Start()
    {
        normalSpriteMat = Resources.Load<Material>("Materials/RegularSpriteMat");
        outlineSpriteMat = Resources.Load<Material>("Materials/OutlineMat");
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        _spriteRenderer.material = outlineSpriteMat;
    }

    public void HideOutline()
    {
        _spriteRenderer.material = normalSpriteMat;
    }

    
}
