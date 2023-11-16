using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private GameObject audioSource;
    public AudioClip ExplosionClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioObject (Explosion)");
        audioSource.gameObject.GetComponent<AudioSource>().PlayOneShot(ExplosionClip);
        Destroy(gameObject, 0.25f);
    }
}
