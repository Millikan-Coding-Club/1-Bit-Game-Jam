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
    public GameObject gameController;
    private float speed;
    private float rotationSpeed;
    private float targetX;
    private float targetY;

    // Start is called before the first frame update
    void Start()
    {

        rotationSpeed = Random.Range(maxSpin * -1, maxSpin);
        speed = Random.Range(minSpeed, maxSpeed);
        targetX = Random.Range(-1.4f, 1.4f);
        targetY = Random.Range(-1.4f, 1.4f);
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
            
        }
    }
}
