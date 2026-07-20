using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaid : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 1;
    public float damage = 100;
    public int knifeCount = 1;
    public float knifeSpeed = 1;

    public GameObject lowestHealthEnemy;
    //public GameObject target;

    //private SpriteRenderer sprite;

    private GameObject knife;
    private GameObject knifePrefab;

    private Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        knifePrefab = Resources.Load("Sprites/Bullets/Knife") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());

    }

    public void FixedUpdate()
    {
        if (friendScript.FindClosestEnemy() != null || Input.GetMouseButton(1))
        {
            diff = friendScript.closestEnemyDirection;
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => diff != Vector3.zero); //waits until there's an enemy around

        for (var i = 0; i < Mathf.RoundToInt(knifeCount * PlayerMovement.instance.tofuSupp); i++)
        {
            knife = Instantiate(knifePrefab, transform.position, transform.rotation); //laser fireball
            knife.GetComponent<BleedBullet>().bulletDamage = damage;
            knife.GetComponent<BleedBullet>().bleedDamage = 1;
            Rigidbody2D laserRig = knife.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            laserRig.velocity = diff * knifeSpeed;
            knife.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
        }


        yield return new WaitForSeconds(cooldown);

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown());
    }

    public IEnumerator FreeAttack()
    {
        Debug.Log("yep");
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        float lowestHealth = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<EnemyBehaviour>().health < lowestHealth)
            {
                lowestHealthEnemy = enemy;
            }
        }

        Vector3 target;

        target = (lowestHealthEnemy.transform.position - transform.position).normalized;

        knife = Instantiate(knifePrefab, transform.position, transform.rotation); //laser fireball
        knife.GetComponent<BleedBullet>().bulletDamage = damage;
        knife.GetComponent<BleedBullet>().bleedDamage = 1;
        Rigidbody2D laserRig = knife.GetComponent<Rigidbody2D>(); //laser rigidbody

        float distance = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        laserRig.velocity = target * knifeSpeed;
        knife.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

        yield return null;
    }


    #region Level Ups
    public void OnLevelUp()
    {
        currLevel++;

        if (friendScript.level == 1)
        {

        }
        else if (friendScript.level == 2)
        {


        }
        else if (friendScript.level == 3)
        {


        }
        else if (friendScript.level == 4)
        {


        }
        else if (friendScript.level == 5)
        {


        }
        else if (friendScript.level == 6)
        {


        }
        else if (friendScript.level == 7)
        {


        }
        else if (friendScript.level == 8)
        {

        }
    }
    #endregion

}
