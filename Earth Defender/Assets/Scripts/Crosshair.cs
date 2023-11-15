using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private GameObject missile;
    // Start is called before the first frame update
    void Start()
    {
        missile = GameObject.FindGameObjectsWithTag("missile").Last();
    }

    private void Update()
    {
        if (missile == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == missile)
        {
            Destroy(gameObject);
        }
    }
}
