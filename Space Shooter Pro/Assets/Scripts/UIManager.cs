using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text scoreText, ammoCountText;
    
    [SerializeField] 
    private Text messageText;

    [SerializeField]
    private Sprite[] lives;

    [SerializeField]
    private Image livesImage, ammoCountFill;

    [SerializeField]
    private GameObject gameOverObject;

    [SerializeField]
    private float strobeSpeed = 1f;

    [SerializeField]
    private GameObject button_Restart;

    private Color colorStart = Color.white, colorEnd = Color.red;
    private bool gameOver;

    private bool[] waveMessageDisplayed;
    private string[] waveMessageText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        scoreText.text = ": " + 0;
        ammoCountText.text = "15/15";
        livesImage.sprite = lives[3];
        waveMessageDisplayed = new bool[] { false, false, false, false };
        waveMessageText = new string[] { "FIRST WAVE!", "SECOND WAVE!", "THIRD WAVE!", "BOSS FIGHT!" };
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        
        if(gameOver) {
            gameOverObject.GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, Mathf.PingPong(Time.time * strobeSpeed, 1));
        }

       if(gameManager.restartState) {
            waveMessageDisplayed = new bool[] { false, false, false, false };
        }
    }

    public void AlertIncomingWave(int wave) {
        if(wave ==0) {
            //StartCoroutine(DisplayWaveMessage(wave));
        } else if (wave == 1) {
            //StartCoroutine(DisplayWaveMessage(wave));
        } else if (wave == 2) {
            //StartCoroutine(DisplayWaveMessage(wave));
        } else if (wave == 3) {
            //StartCoroutine(DisplayWaveMessage(wave));
        }
        StartCoroutine(DisplayWaveMessage(wave));

    }

    IEnumerator DisplayWaveMessage(int waveNo) {
        if(!waveMessageDisplayed[waveNo]) {
            messageText.gameObject.SetActive(true);
            messageText.text = waveMessageText[waveNo];
            waveMessageDisplayed[waveNo] = true;
            yield return new WaitForSeconds(3);
            messageText.gameObject.SetActive(false);
        }
        
    }

    public void UpdateUIScore(int points) {
        scoreText.text = ": " + points.ToString();
    }

    public void UpdateAmmoCount(int ammoCount) {
        ammoCountText.text = ammoCount.ToString() + "/15";
    }

    public void UpdateAmmoCountIndicator(float fill) {
        ammoCountFill.fillAmount = fill;
    }

    public void UpdateHealthSprites(int health) {
        livesImage.sprite = lives[health];

        if(health <= 0) {
            StartCoroutine(GameOverFlicker());
        }

    }

    public void TurnOnGameOverMessage() {
        gameOverObject.SetActive(true);
        button_Restart.SetActive(true);
        gameOver = true;
    }

    IEnumerator GameOverFlicker() {

        while(true) {
            gameOverObject.SetActive(true);
            yield return new WaitForSeconds(.7f);
            gameOverObject.SetActive(false);
            yield return new WaitForSeconds(.33f);
        }
      
    }

}
