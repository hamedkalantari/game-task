using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ProfileMenu : MonoBehaviour
{
    private string path;
    private string nameInput;
    private string emailInput;
    private string usernameInput;


    private void Start()
    {
        if (PlayerPrefs.HasKey("image"))
        {
            LoadImage(PlayerPrefs.GetString("image"));
        }

        if (PlayerPrefs.HasKey("name") && PlayerPrefs.GetString("name").Length > 1)
        {
            transform.GetChild(3).transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            transform.GetChild(3).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("name");
        }

        if (PlayerPrefs.HasKey("email") && PlayerPrefs.GetString("email").Length > 1)
        {
            transform.GetChild(5).transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            transform.GetChild(5).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("email");
        }

        if (PlayerPrefs.HasKey("username") && PlayerPrefs.GetString("username").Length > 1)
        {
            transform.GetChild(7).transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            transform.GetChild(7).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("username");
        }
    }

    private void OnDisable()
    {
        nameInput = transform.GetChild(3).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;
        emailInput = transform.GetChild(5).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;
        usernameInput = transform.GetChild(7).transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("name", nameInput);
        PlayerPrefs.SetString("email", emailInput);
        PlayerPrefs.SetString("username", usernameInput);
    }

    private void LoadImage(string path)
    {
        if (!System.String.IsNullOrEmpty(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            Sprite loadedSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100.0f);
            Debug.Log(GetComponent<Image>());
            GameObject.Find("Profile Image").GetComponent<Image>().sprite = loadedSprite;
        }
    }

    public void SetImage()
    {
        path = UnityEditor.EditorUtility.OpenFilePanel("Open image", "", "jpg,png");
        PlayerPrefs.SetString("image", path);
        if (!System.String.IsNullOrEmpty(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            Sprite loadedSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100.0f);
            GameObject.Find("Profile Image").GetComponent<Image>().sprite = loadedSprite;
        }
    }
}
