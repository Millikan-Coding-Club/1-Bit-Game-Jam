using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Circle : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image = null;

    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (GameController.cooldown != 0 && GameController.selectedBaseStr != "")
        {
            image.fillAmount = 1 - GameController.count / GameController.cooldown;
        }
    }
}
