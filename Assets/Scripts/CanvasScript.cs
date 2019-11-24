using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CanvasScript : MonoBehaviour
{
    private List<Button> buttons;
    private List<TMP_FontAsset> fonts;
    private List<Color> colors;

    public Button btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10;
    public TMP_FontAsset font1, font2, font3, font4, font5, font6;
    // Start is called before the first frame update
    void Start()
    {
        // set buttons
        buttons = new List<Button>();
        buttons.Add(btn1);
        buttons.Add(btn2);
        buttons.Add(btn3);
        buttons.Add(btn4);
        buttons.Add(btn5);
        buttons.Add(btn6);
        buttons.Add(btn7);
        buttons.Add(btn8);
        buttons.Add(btn9);
        buttons.Add(btn10);

        // set fonts
        fonts = new List<TMP_FontAsset>();
        fonts.Add(font1);
        fonts.Add(font2);
        fonts.Add(font3);
        fonts.Add(font4);
        fonts.Add(font5);
        fonts.Add(font6);

        // set colors
        colors = new List<Color>();
        colors.Add(new Color32(65, 47, 47, 255));
        colors.Add(new Color32(100, 116, 58, 255));
        colors.Add(new Color32(76, 58, 115, 255));
        colors.Add(new Color32(58, 100, 115, 255));
        colors.Add(new Color32(115, 113, 58, 255));
        colors.Add(new Color32(115, 58, 101, 255));
    }

    public void ChangeButtonColors()
    {
        Color newColor = colors[Random.Range(0, colors.Count)];
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = newColor;
        }
    }


    public void ChangeButtonFonts()
    {
        TMP_FontAsset newFont = fonts[Random.Range(0, fonts.Count)];
        foreach (var button in buttons)
        {
            button.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().font = newFont;
        }
    }

    public void ChangeBackgroundColor()
    {
        Color newColor = colors[Random.Range(0, colors.Count)];
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = newColor;
    }
}
