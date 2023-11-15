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
    [SerializeField] private float maxSpin = 80f;
    [SerializeField] private GameObject explosion;
    private float speed;
    private float rotationSpeed;
    private float targetX;
    private float targetY;
    private GameObject leftBase;
    private GameObject rightBase;
    private GameObject leftBaseSquare;
    private GameObject rightBaseSquare;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(maxSpin * -1, maxSpin);
        speed = Random.Range(minSpeed, maxSpeed);
        targetX = Random.Range(-1.4f, 1.4f);
        targetY = Random.Range(-1.4f, 1.4f);
        leftBase = GameObject.Find("LeftBase");
        rightBase = GameObject.Find("RightBase");
        leftBaseSquare = GameObject.Find("leftBaseSquare");
        rightBaseSquare = GameObject.Find("rightBaseSquare");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, targetY), speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "Earth")
        {
            Destroy(this.gameObject);
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
            } else if (leftBase == null)
            {
                destroyRightBase();
            } else
            {
                destroyLeftBase();
            }
        }

        if (collision.gameObject.name == "LeftBase")
        {
            destroyLeftBase();
        }

        if (collision.gameObject.name == "RightBase")
        {
            destroyRightBase();
        }

        if (collision.gameObject.tag == "missile")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
    private void destroyLeftBase()
    {
        Destroy(this.gameObject);
        Destroy(leftBase);
        Destroy(leftBaseSquare);
        Instantiate(explosion, leftBase.transform.position, transform.rotation);
        GameController.health -= 1;
    }

    private void destroyRightBase()
    {
        Destroy(this.gameObject);
        Destroy(rightBase);
        Destroy(rightBaseSquare);
        Instantiate(explosion, rightBase.transform.position, transform.rotation);
        GameController.health -= 1;
    }
}