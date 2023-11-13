using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private float rotationSpeed;
    public float maxSpin = 80f;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(maxSpin * -1, maxSpin);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
