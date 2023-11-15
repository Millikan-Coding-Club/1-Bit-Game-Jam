using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosion;
    private Rigidbody2D rb;
    private Vector2 target;
    public float speed = 1f;
    public float rotateSpeed = 10f;
    private bool left = false;
    public AudioSource audioSource;
    public AudioClip MissileClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.rotation += 90;
        audioSource.PlayOneShot(MissileClip, 0.4f);

    }

    private void FixedUpdate()
    {
        Vector2 direction = target - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Earth")
        {
            left = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "asteroid")
        {
            Destroy(gameObject);
        } else if (left == true && collision.gameObject.tag != "crosshair")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
