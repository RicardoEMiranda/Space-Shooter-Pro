using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Sprite[] lives;

    [SerializeField]
    private Image livesImage;

    [SerializeField]
    private GameObject gameOverObject;

    [SerializeField]
    private float strobeSpeed = 1f;

    private Color colorStart = Color.white, colorEnd = Color.red;
    private bool gameOver;

    // Start is called before the first frame update
    void Start() {
        scoreText.text = ": " + 0;
        livesImage.sprite = lives[3];
    }

    // Update is called once per frame
    void Update() {
        
        if(gameOver) {
            gameOverObject.GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, Mathf.PingPong(Time.time * strobeSpeed, 1));
        }

    }

    public void UpdateUIScore(int points) {
        scoreText.text = ": " + points.ToString();
    }

    public void UpdateHealthSprites(int health) {
        livesImage.sprite = lives[health];
    }

    public void TurnOnGameOverMessage() {
        gameOverObject.SetActive(true);
        gameOver = true;
    }
}
