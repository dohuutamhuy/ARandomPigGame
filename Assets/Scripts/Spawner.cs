using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lemonadePrefab;
    public GameObject baconPrefab;
    private SpriteRenderer sr;
    private EdgeCollider2D ec;
    public PolygonCollider2D pc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ContainsPoint(Vector2[] polygon, Vector2 testPoint) {
        bool result = false;
        int j = polygon.Length - 2;
        for (int i = 0; i < polygon.Length-1; i++) {
            if (polygon[i].y < testPoint.y && polygon[j].y >= testPoint.y || polygon[j].y < testPoint.y && polygon[i].y >= testPoint.y) {
                if (polygon[i].x + (testPoint.y - polygon[i].y) / (polygon[j].y - polygon[i].y) * (polygon[j].x - polygon[i].x) < testPoint.x) {
                    result = !result;
                }
            }
            j = i;
        }
        return result;
    }

    private void SpawnEnemy() {        
        if (pc) {
            //Debug.Log(ec.points);
            pc.enabled = true;
            Vector3 center = sr.bounds.center;
            Vector3 extends = sr.bounds.extents;
            for (int i = 0; i < 50; i++) {
                float newX, newY;
                do {
                    newX = Random.Range(center.x - extends.x, center.x + extends.x);
                    newY = Random.Range(center.y - extends.y, center.y + extends.y);
                } while (!pc.OverlapPoint(new Vector2(newX, newY)));
                Instantiate(lemonadePrefab, new Vector3(newX, newY, 0), Quaternion.identity);
            }
            for (int i = 0; i < 3; i++) {
                float newX, newY;
                do {
                    newX = Random.Range(center.x - extends.x, center.x + extends.x);
                    newY = Random.Range(center.y - extends.y, center.y + extends.y);
                } while (!pc.OverlapPoint(new Vector2(newX, newY)));
                Instantiate(baconPrefab, new Vector3(newX, newY, 0), Quaternion.identity);
            }
            pc.enabled = false;
        }        
    }
}

        
