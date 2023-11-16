using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
    private GameObject selectedBase;
    public static string selectedBaseStr;

    public float distance = 10;
    public GameObject[] asteroids;
    public GameObject warning;
    static public int health = 2;
    public AudioSource audioSource;
    public AudioClip SelectClip;
    public AudioClip ReloadClip;
    public TextMeshProUGUI TimerText;
    static public float time = 0;
    private int spawns = 0;
    public float interval = 3f;
    public float cooldown = 1f;
    private float count = 0f;
 
    // Start is called before the first frame update
    void Start()
    {
        selectedBaseStr = "";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        count += Time.deltaTime;
        TimerText.text = "Time Survived: " + Mathf.RoundToInt(time);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectBase(leftBase, "left");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectBase(rightBase, "right");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider == null || hit.collider.gameObject.tag == "asteroid")
            {
                if (count > cooldown)
                {
                    fireMissile();
                }
            } else
            {
                if (hit.collider.transform == satelliteSquare.transform)
                {
                    Debug.Log("satellite");
                }
                if (leftBase != null)
                {
                    if (hit.collider.transform == leftBaseSquare.transform
                        && !selectedBaseStr.Equals("left"))
                    {
                        selectBase(leftBase, "left");
                    }
                }

                if (rightBase != null)
                {
                    if (hit.collider.transform == rightBaseSquare.transform
                        && !selectedBaseStr.Equals("right"))
                    {
                        selectBase(rightBase, "right");

                    }
                }
            }
        }

        if (health == 0)
        {
            gameOver();
        }

        // function to spawn asteroid as a function of time.

        if (spawns + 1 < time * interval) {
            spawnAsteroid();
            spawns++;
        }
    }

    private void selectBase(GameObject baseToSelect, string baseStr)
    {
        if (baseToSelect != null)
        {
            selectedBase = baseToSelect;
            selectedBaseStr = baseStr;
            audioSource.PlayOneShot(SelectClip, 0.4f);
        }
    }

    private void fireMissile()
    {
        count = 0f;
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (selectedBase != null)
        {
            Instantiate(nuke, selectedBase.transform.position, Quaternion.Euler(selectedBase.transform.rotation.eulerAngles - new Vector3(0, 0, 90)));
            Instantiate(crosshair, target, transform.rotation);
        } else
        {
            warning.SetActive(true);
            Invoke("deactivateWarning", 3);
        }
        
    }

    private void deactivateWarning() 
    {
        warning.SetActive(false); 
    }

    private void spawnAsteroid()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * distance * 1.75f;
        float y = Mathf.Sin(angle) * distance;
        GameObject obj = Instantiate(asteroids[Random.Range(0, 13)], new Vector3(x, y, 0), transform.rotation);
    }

    private void gameOver()
    {
        // TODO
        Debug.Log("Game Over");
    }
}
