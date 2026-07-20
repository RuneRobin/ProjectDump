using UnityEngine;
using System.Collections;

public class WebsiteOpener : MonoBehaviour
{
    public void WebsiteOpen(string website)
    {
        Application.OpenURL(website);
    }
}
