using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Circle : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image = null;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (GameController.cooldown != 0 )
        {
            image.fillAmount = 1 - GameController.count / GameController.cooldown;
        }
    }
}
