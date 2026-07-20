using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kestryl : MonoBehaviour
{

    public float cooldown = 60;
    public float aimSpeed = 1;
    public float damage = 500;
    private Vector3 mousePos;
    private bool seekingTarget = false;

    private GameObject crosshair;
    private GameObject crosshairPrefab;
    private new Camera camera;
    


    // Start is called before the first frame update
    void Start()
    {
        crosshairPrefab = Resources.Load("Sprites/Bullets/Crosshair") as GameObject;
        camera = Camera.main;
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (seekingTarget == true)
        {
            crosshair.transform.position = Vector2.Lerp(transform.position, mousePos, aimSpeed);
        }

        if (Input.GetMouseButton(1) && seekingTarget == true)
        {    
                if (Input.GetMouseButton(0))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.gameObject.GetComponent<EnemyBehaviour>().health -= damage;
                    }
                    
                    seekingTarget = false;
                    Destroy(crosshair);
                    StartCoroutine(AttackCooldown());
                }
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        seekingTarget = true;
        crosshair = Instantiate(crosshairPrefab, new Vector2(mousePos.x, mousePos.y), transform.rotation);
    }

}
