using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public string Base;
    Renderer visual;

    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (Base == GameController.BaseSelection) {
            visual.enabled = false;
        }
        else {
            visual.enabled = true;
        }
        
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            GameController.BaseSelection = Base;
        }
    }

}
