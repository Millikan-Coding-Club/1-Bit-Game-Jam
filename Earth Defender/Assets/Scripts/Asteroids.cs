using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.01f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = .8f;
    [SerializeField] private float maxSpin = 80f;

    [SerializeField] private GameObject explosion;
    private float speed;
    private float rotationSpeed;
    private float targetX;
    private float targetY;
    private Vector2 target;
    private float scale;

    private GameObject leftBase;
    private GameObject rightBase;
    private GameObject leftBaseSquare;
    private GameObject rightBaseSquare;
    public AudioSource audioSource;
    public AudioClip CollisionClip;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(maxSpin * -1, maxSpin);
        speed = Random.Range(minSpeed, maxSpeed);
        //targetX = Random.Range(0, 1.4f);
        //targetY = Random.Range(-1.4f, 1.4f);
        target = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

        scale = Random.Range(minScale, maxScale);
        leftBase = GameObject.Find("LeftBase");
        rightBase = GameObject.Find("RightBase");
        leftBaseSquare = GameObject.Find("leftBaseSquare");
        rightBaseSquare = GameObject.Find("rightBaseSquare");
        transform.localScale = new Vector2(scale, scale);

    }

    // Update is called once per frame
    void Update()
    {

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
            Destroy(gameObject);
            Debug.Log("Asteroid hit");
        }

        if (collision.gameObject.tag != "crosshair")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Debug.Log("Asteroid hit");
            audioSource.Play();
        }
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