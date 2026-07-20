using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitbox : MonoBehaviour
{

    public List<Collider2D> hitboxColliders;
    public float hitboxTimerToHurt = 0.4f;
    //public float timeWindow = 0f;
    //public bool parryWindow = false;

    public bool invulWindow = false;
    public float invulTimer = 0.5f;

    public float timeToBlock = 0;
    public bool isNowBlocking = false;

    public PlayerScript player;

    private AudioSource audioSource;
    public AudioClip[] differentParrySounds;

    private bool parrySpriteActive = false;
    private float parrySpriteTimer = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        audioSource = GetComponent<AudioSource>();
        hitboxColliders = new List<Collider2D>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(hitboxColliders.Count != 0 && CharacterLoader.instance.currentlyFighting == false)
        {
            hitboxColliders.Clear();
            hitboxTimerToHurt = 0.4f;
        }

        if(CharacterLoader.instance.currentlyFighting == true)
        {
            if (hitboxColliders.Count != 0)
            {
                hitboxTimerToHurt -= Time.unscaledDeltaTime;
            }
            if (hitboxColliders.Count == 0 && hitboxTimerToHurt != 0.4f)
            {
                hitboxTimerToHurt = 0.4f;
            }
        }

        if (hitboxTimerToHurt <= 0)
        {
            if (invulWindow == false)
            {
                Debug.Log("taking damage from timer to hurt running out");
                if (hitboxColliders[0].isActiveAndEnabled)
                {
                    TakeDamage(hitboxColliders[0].GetComponent<EnemyBehaviour>().damage);
                }
                else
                {
                    hitboxColliders.RemoveAt(0);
                }
            }
            foreach (Collider2D bullet in hitboxColliders)
            {
                if (bullet.isActiveAndEnabled)
                {
                    Destroy(bullet.gameObject);
                }
            }
            hitboxColliders.Clear();
            hitboxTimerToHurt = 0.4f;
        }

        if (invulWindow == true)
        {
            invulTimer -= Time.unscaledDeltaTime;
            if (invulTimer <= 0)
            {
                invulWindow = false;
                invulTimer = 0.5f;
            }
        }

        if(Input.GetMouseButton(1) && CharacterLoader.instance.currentlyFighting == true)
        {
            timeToBlock = Mathf.Clamp(timeToBlock + Time.unscaledDeltaTime, 0, 1f);
            if(timeToBlock >= 1)
            {
                isNowBlocking = true;
            }
        }
        else
        {
            timeToBlock = 0;
            isNowBlocking = false;
        }
        #region Sprite Change on Parry
        if (parrySpriteActive == true)
        {
            parrySpriteTimer -= Time.unscaledDeltaTime;
            if(parrySpriteTimer <= 0)
            {
                parrySpriteActive = false;
                parrySpriteTimer = 0.3f;
                //player.GetComponent<SpriteRenderer>().sprite = player.gameObject.GetComponent<CharacterAnimator>().neutralStance; //bring this back later with another parry sprite animation
            }
        }
        #endregion
    }

    public void TakeDamage(int damage)
    {
        player.health -= damage;
        player.SetHealth(player.health);
        player.blockRegenTimer = 0;

        invulWindow = true;
    }

    public void MinusBlock(int damage)
    {
        player.block -= damage;
        player.SetBlock(player.block);
        player.blockRegenTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyProjectile") //arms and bullets never interact so this is fine as it is
        {
            hitboxColliders.Add(collision);
        }

        /*if(collision.tag == "Enemy")
        {
            if (Input.GetMouseButton(1) && isNowBlocking == false)
            {
                collision.GetComponent<EnemyStats>().physicallyParried = true;

                invulTimer = 0.1f;
                invulWindow = true;//brief invulnerability after a successful parry
            }

            if (!Input.GetMouseButton(1) || isNowBlocking == false)
            {
                TakeDamage(collision.GetComponent<EnemyStats>().damage);
                Debug.Log("enemy hit you");
            }

            if(isNowBlocking == true)
            {
                MinusBlock(1);
            }

        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "EnemyProjectile")
        {
            if (Input.GetMouseButton(1))
            {
                if(isNowBlocking == true && player.block > 0)
                {
                    hitboxColliders.Remove(collision);

                    if (collision.GetComponent<EnemyBehaviour>().isDestructable == true)
                    {
                        Destroy(collision.gameObject);
                    }

                    MinusBlock(1); // might also change dynamically as with health damage
                }
                else if(timeToBlock < 1 && hitboxColliders.Contains(collision))
                {
                    audioSource.PlayOneShot(differentParrySounds[Random.Range(0, differentParrySounds.Length)], audioSource.volume);
                    collision.GetComponent<EnemyBehaviour>().parried = true; //to trigger parried effects when destroyed
                    hitboxColliders.Remove(collision);

                    if (collision.GetComponent<EnemyBehaviour>().isDestructable == true)
                    {
                        Destroy(collision.gameObject);
                    }

                    invulTimer = 0.1f;
                    invulWindow = true;//brief invulnerability after a successful parry

                    //player.GetComponent<SpriteRenderer>().sprite = player.gameObject.GetComponent<CharacterAnimator>().parryStance; //bring this back later with another parry sprite animation
                    parrySpriteTimer = 0.3f;
                    parrySpriteActive = true;
                }                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyProjectile" && collision.isActiveAndEnabled && collision.GetComponent<EnemyBehaviour>().isDestructable == true)
        {
            hitboxColliders.Remove(collision);
            if (invulWindow == false)
            {
                Debug.Log("Taking damage from exiting parry zone");
                TakeDamage(collision.GetComponent<EnemyBehaviour>().damage);
                Destroy(collision.gameObject);
            }
            
        }
    }
}
