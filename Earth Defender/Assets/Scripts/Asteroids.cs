using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private float minSpeed = 0.01f * GameController.difficulty;
    private float maxSpeed = 1 + 0.5f * GameController.difficulty;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = .8f;
    [SerializeField] private float maxSpin = 80f;

    [SerializeField] private GameObject explosion;
    private float speed;
    private float rotationSpeed;
    private Vector2 target;
    private float scale;

    private GameObject leftBase;
    private GameObject rightBase;
    private GameObject leftBaseSquare;
    private GameObject rightBaseSquare;
    private float startTime;
    private bool visible = false;

    // Start is called before the first frame update
    void Start()
    {

        rotationSpeed = Random.Range(maxSpin * -1, maxSpin);
        speed = Random.Range(minSpeed, maxSpeed);
        target = Camera.main.ViewportToWorldPoint(new Vector2(Random.value * 2, Random.value * 1));

        scale = Random.Range(minScale, maxScale);
        leftBase = GameObject.Find("LeftBase");
        rightBase = GameObject.Find("RightBase");
        leftBaseSquare = GameObject.Find("leftBaseSquare");
        rightBaseSquare = GameObject.Find("rightBaseSquare");
        transform.localScale = new Vector2(scale, scale);
        startTime = GameController.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.time > startTime + 10f && !visible)
        {
            // destroy if 10 seconds have elapsed
            Destroy(gameObject);
        }
        if (Mathf.Abs(target.x) < 11)
        {
            target.x *= 2;
        }
        if (Mathf.Abs(target.y) < 6)
        {
            target.y *= 2;
        }
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "Earth")
        {
            if (leftBase != null && rightBase != null)
            {
                if (Vector3.Distance(rightBase.transform.position, transform.position) > Vector3.Distance(leftBase.transform.position, transform.position))
                {
                    destroyLeftBase();
                }
                else
                {
                    destroyRightBase();
                }
            } else if (leftBase == null && rightBase != null) {
            
                destroyRightBase();
            } else if (leftBase != null && rightBase == null) {
                destroyLeftBase();
            }
        }

        if (collision.gameObject.name == "LeftBase" && leftBase != null) {
            destroyLeftBase();
        }

        if (collision.gameObject.name == "RightBase" && rightBase != null)
        {
            destroyRightBase();
        }

        if (collision.gameObject.tag == "missile")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag != "crosshair" && collision.gameObject.tag != "square" && visible)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        // destroy if asteroid leaves camera
        Destroy(gameObject);
        visible = false;
    }
    private void destroyLeftBase()
    {
        Destroy(gameObject);
        Destroy(leftBase);
        Destroy(leftBaseSquare);
        Instantiate(explosion, leftBase.transform.position, transform.rotation);
        GameController.health -= 1;
    }

    private void destroyRightBase()
    {
        Destroy(gameObject);
        Destroy(rightBase);
        Destroy(rightBaseSquare);
        Instantiate(explosion, rightBase.transform.position, transform.rotation);
        GameController.health -= 1;
    }
}