using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BleedBullet : MonoBehaviour
    {
        public float despawnTime = 5;
        public float bulletDamage;
        public float bleedDamage;
        public bool pierces = false;
        private List<GameObject> alreadyHit = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Despawner());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator Despawner()
        {
            yield return new WaitForSeconds(despawnTime);
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && !alreadyHit.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
                collision.gameObject.GetComponent<EnemyBehaviour>().bleedTimer = 0;
                collision.gameObject.GetComponent<EnemyBehaviour>().bleeding = true;
                alreadyHit.Add(collision.gameObject);

                if (pierces == false)
                {
                    Destroy(gameObject);
                }

            }
        }

    }
