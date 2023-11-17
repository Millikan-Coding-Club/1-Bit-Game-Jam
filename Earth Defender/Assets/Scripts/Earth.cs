using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public GameObject CircleCanvas;
    private float RotationSpeed;
    public float minSpeed = -5f;
    public float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        RotationSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float rotAmount = RotationSpeed * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
        CircleCanvas.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }
}
