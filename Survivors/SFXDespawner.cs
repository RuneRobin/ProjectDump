using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDespawner : MonoBehaviour
{

    public float duration;

    SpriteRenderer sprite;
    public float alphaValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeTo(alphaValue, duration));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeTo(float aValue, float fadeTime) //fades out object, then destroys it once invisible
    {
        float alpha = sprite.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(alpha, aValue, t));
            sprite.color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}