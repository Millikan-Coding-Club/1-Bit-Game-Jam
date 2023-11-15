using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    Renderer visual;
    public string Base;

    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (GameController.selectedBaseStr.Equals(Base)) {

            visual.enabled = false;
        }
        else {
            visual.enabled = true;
        }
    }
    
}
