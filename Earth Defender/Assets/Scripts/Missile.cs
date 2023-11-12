using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public GameObject missilePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), speed * Time.deltaTime);
        transform.up = new Vector3(0f, 0f) - transform.position;
    }
}
