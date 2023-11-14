using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject leftBase;
    public GameObject rightBase;
    public GameObject leftBaseSquare;
    public GameObject rightBaseSquare;
    public GameObject satelliteSquare;
    public GameObject nuke;
    public float distance = 10;
    public GameObject[] asteroids;
    [SerializeField] private float interval = 5f;
    public int health = 2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnAsteroid", 0, interval);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider == null)
            {
                fireMissile();
            } else
            {
                if (hit.collider.transform == satelliteSquare.transform)
                {
                    Debug.Log("satellite");
                }

                if (hit.collider.transform == leftBaseSquare.transform)
                {
                    Debug.Log("left base");
                }

                if (hit.collider.transform == rightBaseSquare.transform)
                {
                    Debug.Log("right base");
                }
            }
        }
    }

    private void fireMissile()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(crosshair, mousePos, transform.rotation);
        Instantiate(nuke, rightBase.transform.position, Quaternion.Euler(rightBase.transform.rotation.eulerAngles - new Vector3(0, 0, 90)));
    }

    private void spawnAsteroid()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * distance * 1.75f;
        float y = Mathf.Sin(angle) * distance;
        // give asteroid prefab a random asteroid sprite

        Instantiate(asteroids[Random.Range(0, 13)], new Vector3(x, y, 0), transform.rotation);
    }
}
