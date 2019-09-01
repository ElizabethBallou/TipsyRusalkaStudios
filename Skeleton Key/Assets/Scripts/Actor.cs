using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{ //needs a sprite for each emotion
    public Sprite[] emotionSprites;
    private SpriteRenderer _spriteRenderer;
    public int ID; //left = 0; right = 1
    
    public enum characterEmotions
    {
        neutral,
        happy,
        sad,
        angry,
        laughing
    }

    private characterEmotions currentState;

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = characterEmotions.neutral;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(string emotionName)
    {
        StartCoroutine(emotionName + "State");
    }

    IEnumerator HappyState()
    {
        _spriteRenderer.sprite = emotionSprites[0];
        yield return null;
    }
    
    IEnumerator SadState()
    {
        _spriteRenderer.sprite = emotionSprites[1];
        yield return null;
    }
    
    IEnumerator WorriedState()
    {
        _spriteRenderer.sprite = emotionSprites[2];
        yield return null;
    }
    
    IEnumerator LaughingState()
    {
        _spriteRenderer.sprite = emotionSprites[3];
        yield return null;
    }
    
    IEnumerator NeutralState()
    {
        _spriteRenderer.sprite = emotionSprites[4];
        yield return null;
    }
}
