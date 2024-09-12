using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject mainImage;
    public Sprite gameOverImg;
    public Sprite gameClearImg;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImg", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.gameState == "gameClear")
        {
            ShowMainImage(gameClearImg, restartButton);
        }
        else if (Player.gameState == "gameOver")
        {
            ShowMainImage(gameOverImg, nextButton);
        }
        else if (Player.gameState == "playing") return;
    }
    private void InactiveImg()
    {
        mainImage.SetActive(false);
    }

    private void ShowMainImage(Sprite image, GameObject button)
    {
        mainImage.GetComponent<Image>().sprite = image;
        mainImage.SetActive(true);
        Button btn = button.GetComponent<Button>();
        btn.interactable = false;
        panel.SetActive(true);
    }
}
