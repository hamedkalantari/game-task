using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScene : MonoBehaviour
{
    public static MainMenuScene singleton;
    public static bool playFromBeginning;
    public GameObject mainMenu;

    void Start()
    {
		DontDestroyOnLoad(this.gameObject);
		if (singleton == null)
		{
            playFromBeginning = true;
            singleton = this;
		}
        else
        {
            Destroy(this.gameObject);
        }
	}

    private void Update()
    {
        if (!playFromBeginning)
        {
            GameObject.FindGameObjectWithTag("PlayButtonText").GetComponent<TextMeshProUGUI>().text = "RESUME";
        }
        else
        {
            GameObject.FindGameObjectWithTag("PlayButtonText").GetComponent<TextMeshProUGUI>().text = "PLAY";
        }
    }

    public void PlayGame()
    {
        if (playFromBeginning)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            RollingBall.resume = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
