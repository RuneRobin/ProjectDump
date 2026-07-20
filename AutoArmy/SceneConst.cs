using UnityEngine;

public class SceneConst : MonoBehaviour
{
    public static SceneConst instance;

    public bool autoStart = false;

    public AudioClip[] SFXCollection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
