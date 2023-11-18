using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject earth;
    public GameObject explosion;
    public GameObject bg;

    private GameObject selectedBase;
    public static string selectedBaseStr;

    public float distance = 10;
    public GameObject[] asteroids;
    public GameObject warning;
    static public int health = 2;
    public AudioSource audioSource;
    public AudioClip SelectClip;
    public TextMeshProUGUI TimerText;
    static public float time = 0;
    private int spawns = 0;
    static public float cooldown;
    static public float count = 1f;
    public float InitialDifficulty = 1f;
    public float DifficultyIncreasePerMin = 1f;
    static public float difficulty = 1f;

    public GameObject gameOverCanvas;
    public TextMeshProUGUI surviveText;
    public GameObject startCanvas;

    bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        cooldown = 95f;
        Background.x = Random.Range(-1f, 1f);
        Background.y = Random.Range(-1f, 1f);
        selectedBaseStr = "";
        difficulty = InitialDifficulty;
        gameOverCanvas.SetActive(false);
        isGameOver = false;
    }

    // Update is called once per frame
    void Update() {
        if (!isGameOver) {
            difficulty += Time.deltaTime / (60 / DifficultyIncreasePerMin);
            cooldown = 1 - difficulty / 100;
            time += Time.deltaTime;
            if (count < cooldown)
            {
                count += Time.deltaTime;
            } else
            {
                count = cooldown;
            }
            TimerText.text = "Time Survived: " + Mathf.RoundToInt(time);

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                selectBase(leftBase, "left");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                selectBase(rightBase, "right");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider == null || hit.collider.gameObject.tag == "asteroid") {
                    if (count >= cooldown && !startCanvas.activeInHierarchy) {
                        fireMissile();
                    }
                }
                else {
                    if (leftBase != null) {
                        if (hit.collider.transform == leftBaseSquare.transform
                            && !selectedBaseStr.Equals("left")) {
                            selectBase(leftBase, "left");
                        }
                    }

                    if (rightBase != null) {
                        if (hit.collider.transform == rightBaseSquare.transform
                            && !selectedBaseStr.Equals("right")) {
                            selectBase(rightBase, "right");

                        }
                    }
                }
            }

            if (health == 0) {
                isGameOver = true;
                earth.SetActive(false);
                GameObject obj = Instantiate(explosion, new Vector2(0, 0), transform.rotation);
                obj.transform.localScale = new Vector2(6, 6);

                Invoke("gameOver", 0.5f);

            }

            // function to spawn asteroid as a function of time.

            if (spawns + 1 < time * 1.25 * difficulty) {
                spawnAsteroid();
                spawns++;
            }
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
        surviveText.text = "You survived for: " + Mathf.RoundToInt(time) + " seconds";
        TimerText.gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void startGame()
    {
        startCanvas.SetActive(false);
        TimerText.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void quickGame()
    {
        Application.Quit();
    }
    public void restart()
    {
        time = 0;
        health = 2;
        difficulty = InitialDifficulty;
        isGameOver = false;
        TimerText.gameObject.SetActive(true);
        SceneManager.LoadScene("Game");
        Time.timeScale = 0;
    }
}
