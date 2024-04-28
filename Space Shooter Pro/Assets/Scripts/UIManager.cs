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

    // Start is called before the first frame update
    void Start() {
        scoreText.text = ": " + 0;
        livesImage.sprite = lives[3];
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateUIScore(int points) {
        scoreText.text = ": " + points.ToString();
    }

    public void UpdateHealthSprites(int health) {
        livesImage.sprite = lives[health];
    }

    public void TurnOnGameOverMessage() {
        gameOverObject.SetActive(true);
    }
}
