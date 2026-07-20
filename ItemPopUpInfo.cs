using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPopUpInfo : MonoBehaviour
{
    public static ItemPopUpInfo instance;
    public TextMeshProUGUI textComponent;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Cursor.visible = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Input.mousePosition;
        
    }

    public void SetAndShowTooltip(string message)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
        Cursor.visible = false;

        if (textComponent.text.Contains("RY4N"))
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        }
    }

    public void HideTooltip()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
