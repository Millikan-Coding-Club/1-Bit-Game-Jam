using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public GameObject explosion;
    public float maxSpeed = 13f;
    private float rotateSpeed;

    private void Start()
    {
        rotateSpeed = Random.Range(-maxSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), Time.fixedDeltaTime * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "asteroid")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
