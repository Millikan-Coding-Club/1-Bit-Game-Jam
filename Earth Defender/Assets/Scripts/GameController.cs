using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject baseObject;
    public GameObject satellite;
    public int baseAmount = 5;
    public float distance = 10;
    private float earthRadius = 5;
    [SerializeField] private float interval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnMissile", 0, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnMissile()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * distance * 2;
        float y = Mathf.Sin(angle) * distance;
        Instantiate(missilePrefab, new Vector3(x, y, 0), transform.rotation);
    }
}
