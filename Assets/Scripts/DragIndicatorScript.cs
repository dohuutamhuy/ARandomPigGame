using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DragIndicatorScript : MonoBehaviour {
    // Start is called before the first frame update
    Vector3 startPos;
    Vector3 endPos;
    Camera cam;
    LineRenderer lr;
    Rigidbody2D rb;
    SpriteRenderer sr;    
    Vector3 camOffset = new Vector3(0, 0, 10);
    public AnimationCurve ac;
    public Material lineMat;
    public Text scoreText;
    private int score;

    void Start() {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        score = 0;
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = lineMat;
        lr.positionCount = 2;
        lr.enabled = false;
        lr.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Position:" + rb.position);
        //Debug.Log("Velocity:" + rb.velocity);        
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && sr.bounds.Contains(cam.ScreenToWorldPoint(touch.position) + camOffset)) {                                
                lr.enabled = true;                                
                lr.widthCurve = ac;
                lr.numCapVertices = 15;
            }
            if (touch.phase == TouchPhase.Moved && lr.enabled) {
                startPos = transform.position;
                endPos = cam.ScreenToWorldPoint(touch.position) + camOffset;
                lr.SetPosition(0, startPos);
                lr.SetPosition(1, endPos);
            }
            if (touch.phase == TouchPhase.Ended && lr.enabled) {
                lr.enabled = false;
                Vector2 direction = startPos - endPos;
                rb.AddForce(new Vector2(direction.x, direction.y), ForceMode2D.Impulse);
            }
        }        

        if (Input.GetMouseButtonDown(0) && sr.bounds.Contains(cam.ScreenToWorldPoint(Input.mousePosition) + camOffset)) {            
            lr.enabled = true;                            
            lr.widthCurve = ac;
            lr.numCapVertices = 15;
        }
        if (Input.GetMouseButton(0) && lr.enabled) {
            startPos = transform.position;
            endPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;            
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);
        }
        if (Input.GetMouseButtonUp(0) && lr.enabled) {
            lr.enabled = false;
            Vector2 direction = startPos - endPos;
            rb.AddForce(new Vector2(direction.x, direction.y), ForceMode2D.Impulse);
        }
        scoreText.text = "Score: " + score.ToString();        
    }

    private void OnTriggerEnter2D(Collider2D collision) {        
        switch (collision.gameObject.name) {
            case "Lemonade(Clone)":
                score++;                
                Destroy(collision.gameObject);
                break;
            case "Bacon(Clone)":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }
    }

    void FixedUpdate() {
        rb.velocity = rb.velocity * 0.9951f;    
    }
}
