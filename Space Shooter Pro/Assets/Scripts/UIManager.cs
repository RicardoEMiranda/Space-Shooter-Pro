using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text scoreText;

    // Start is called before the first frame update
    void Start() {
        scoreText.text = ": " + 0;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateUIScore(int points) {
        scoreText.text = ": " + points.ToString();
    }
}
