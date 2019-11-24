using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RollingBall : MonoBehaviour
{
    public static bool resume = false;
    public float ballSpeed = 1000f;
    public float moveSpeed = 5f;
    public GameObject[] pins;

    private bool ballStarted;
    private int score;
    private bool paused;
    private GameObject[] gameSceneParentObj;
    private Vector3[] pinOrgPosition;
    private Quaternion[] pinOrgRotation;
    private Vector3 reset;
    private float start_time;

    private void Start()
    {
        pinOrgPosition = new Vector3[pins.Length];
        pinOrgRotation = new Quaternion[pins.Length];
        reset = transform.position;
        paused = false;
        ballStarted = false;
        start_time = 0.0f;
        if (PlayerPrefs.HasKey("score") && PlayerPrefs.GetString("score").Length > 1)
        {
            score = System.Convert.ToInt32(PlayerPrefs.GetString("score"));
            GameObject.Find("score text").GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        }
        else
        {
            score = 0;
        }

        for (int i = 0; i < pins.Length; i++)
        {
            pinOrgPosition[i] = pins[i].transform.position;
            pinOrgRotation[i] = pins[i].transform.rotation;
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        MainMenuScene.singleton.mainMenu.SetActive(paused);
        gameSceneParentObj = GameObject.FindGameObjectsWithTag("GameSceneParentObj");
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        move.x = -Input.GetAxis("Horizontal");

        transform.position += move * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            // change speed according to pressure time
            if (Mathf.Abs(start_time) < 0.01f)
            {
                start_time = Time.time;
            }
        }
        if (Input.GetKeyUp("space"))
        {
            float amount = Mathf.Abs(1 - Mathf.Exp(-(Time.time - start_time)));
            GetComponent<Rigidbody>().AddForce(-ballSpeed * amount * transform.forward);
            ballStarted = true;
            start_time = 0.0f;
        }
        if (Input.GetKeyDown("q") || resume)
        {
            resume = false;
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0.00f;
            }
            else
            {
                Time.timeScale = 1.00f;
            }
            MainMenuScene.singleton.mainMenu.SetActive(paused);
            MainMenuScene.playFromBeginning = !paused;
            gameSceneParentObj[0].SetActive(!paused);
            gameSceneParentObj[1].GetComponent<MeshRenderer>().enabled = !paused;
        }
        if (ballStarted && gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            Invoke("ResetAndSaveScore", 3);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("score", score.ToString());
    }

    void ResetAndSaveScore()
    {
        if (ballStarted)
        {
            for (int i = 0; i < pins.Length; i++)
            {

                if (Mathf.Abs(pins[i].transform.rotation.x) >= 0.3f ||
                    Mathf.Abs(pins[i].transform.rotation.y) >= 0.3f ||
                    Mathf.Abs(pins[i].transform.rotation.z) >= 0.3f)
                {
                    score += 1;
                }
                pins[i].transform.position = pinOrgPosition[i];
                pins[i].transform.rotation = pinOrgRotation[i];
                pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            GameObject.Find("score text").GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            ballStarted = false;
            transform.position = reset;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
