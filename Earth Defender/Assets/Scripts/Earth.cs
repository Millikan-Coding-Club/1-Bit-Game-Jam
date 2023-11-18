using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public GameObject CircleCanvas;
    private float RotationSpeed;
    public float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        RotationSpeed = Random.Range(-maxSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float rotAmount = RotationSpeed * Time.fixedDeltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
        CircleCanvas.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }
}
