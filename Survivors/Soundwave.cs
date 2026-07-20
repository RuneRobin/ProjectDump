using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwave : MonoBehaviour
{
    public float despawnTime = 10;
    public float damage;
    public float duration;

    private List<GameObject> alreadyHit = new List<GameObject>();

    SpriteRenderer soundSprite;
    public float alphaValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        soundSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeTo(alphaValue, duration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeTo(float aValue, float fadeTime) //fades out object, then destroys it once invisible
    {
        float alpha = soundSprite.color.a;
        
        for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(soundSprite.color.r, soundSprite.color.g, soundSprite.color.b, Mathf.Lerp(alpha, aValue, t));
            soundSprite.color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !alreadyHit.Contains(collision.gameObject))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().health -= damage;
            alreadyHit.Add(collision.gameObject);
        }
    }

}
