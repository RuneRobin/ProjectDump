using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBeamGuy : MonoBehaviour
{
    private GameObject player;
    private GameObject squareBeamPrefab;
    private GameObject squareBeam;
    private EnemyStats enemyStats;

    public int numberOfSquaresPerSegment = 3;

    public float rotateSpeed = 1;
    private float invert = 1;
    public int parriedCount = 0;
    public int maxParriedCount = 8;
    public bool rampUp = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        squareBeamPrefab = Resources.Load("Enemy Assets/SquareBeamSquare") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();
        StartCoroutine(BeamAttack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, rotateSpeed * invert);
        
        if(rampUp == true)
        {
            rampUp = false;
            rotateSpeed += 0.5f;
            invert *= -1;
        }

        if(parriedCount >= maxParriedCount)
        {
            StopCoroutine(BeamAttack());
            foreach(Transform child in transform)
            {
                if(player.GetComponent<ParryHitbox>().hitboxColliders.Contains(child.GetComponent<Collider2D>()))
                {
                    player.GetComponent<ParryHitbox>().hitboxColliders.Remove(child.GetComponent<Collider2D>());
                }
                Destroy(child.gameObject);

            }

            parriedCount = 0;
            rotateSpeed = 0;
            enemyStats.woOTimer += 6;
        }

        if(enemyStats.woOTimer == 0 && rotateSpeed < 1)
        {
            rotateSpeed = 1;
            gameObject.transform.Rotate(0, 0, 0);
            StartCoroutine(BeamAttack());
        }
    }

    public IEnumerator BeamAttack()
    {
        float height = numberOfSquaresPerSegment * squareBeamPrefab.transform.localScale.y;
        float separation = numberOfSquaresPerSegment / height;

        List<GameObject> eenymeenymineymo = new List<GameObject>();
        int rng = Random.Range(0, numberOfSquaresPerSegment);

        for (int i = 0; i < numberOfSquaresPerSegment; i++)
        {
            squareBeam = Instantiate(squareBeamPrefab, transform.TransformPoint(0, -((height-1) / 2) + i * separation, 0), transform.rotation);
            squareBeam.transform.SetParent(gameObject.transform);
            eenymeenymineymo.Add(squareBeam);
        }

        Destroy(eenymeenymineymo[rng]);
        eenymeenymineymo.RemoveAt(rng);
        

        yield return new WaitForSeconds(0.2f);
        if (rotateSpeed != 0)
        {
            StartCoroutine(BeamAttack());
        }
    }
}
