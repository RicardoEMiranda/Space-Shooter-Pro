using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject pauseModal;



    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void RestartButtonClicked() {
        //Debug.Log("Restart clicked");
        SceneManager.LoadScene("GameScene");
    }

    public void PauseButtonClicked() {
        Debug.Log("Pause Button clicked");
        pauseModal.SetActive(true);
    }

    public void ResumeButtonClicked() {
        Debug.Log("Resume Button clicked");
    }

    public void ExitButtonClicked() {
        Debug.Log("Exit Button clicked");
    }
}