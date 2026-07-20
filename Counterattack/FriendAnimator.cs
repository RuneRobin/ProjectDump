using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAnimator : MonoBehaviour
{
    public float timeOffset;
    public float speed = 6;
    public void Start()
    {
        timeOffset = Random.Range(0f, 100f);
    }
    public void Update()
    {
        transform.localScale = new Vector3(1 + Mathf.Sin((Time.time + timeOffset) * speed) * 0.2f, 1 + Mathf.Sin((Time.time + timeOffset - 0.2f) * speed) * 0.2f, 1);
    }
}