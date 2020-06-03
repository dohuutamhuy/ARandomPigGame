using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour
{
    EdgeCollider2D ec;
    // Start is called before the first frame update
    void Start() {
        PolygonCollider2D pc = GetComponent<PolygonCollider2D>();
        ec = gameObject.AddComponent<EdgeCollider2D>();
        List<Vector2> points = new List<Vector2>();        
        for (int i = 0; i < pc.points.Length; i++) {
            points.Add(pc.points[i]);
        }
        points.Add(pc.points[0]);        
        ec.points = points.ToArray();
        pc.enabled = false;
        //Destroy(pc);
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
