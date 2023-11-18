using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private RawImage background;
    public float maxBGSpeed = 10f;
    static public float x;
    static public float y;

    // Update is called once per frame
    void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(x, y) * Time.fixedDeltaTime * maxBGSpeed, background.uvRect.size);
    }

}
