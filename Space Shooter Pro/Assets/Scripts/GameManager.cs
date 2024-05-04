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
        //Debug.Log("Pause Button clicked");
        pauseModal.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeButtonClicked() {
        //Debug.Log("Resume Button clicked");
        pauseModal.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitButtonClicked() {
        //Debug.Log("Exit Button clicked");

        //Preprocessor Directives. Conitionally compiles code depending on the condition.
        //If game is playing in the Unity Editor, then will use UnityEditor.EditorApplication.isPlaying to quit the application,
        //since Application.Quit() doesn't work when running on the editor. Otherwise, if playing from the Windows build,
        //uses Application.Quit(). 

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        # endif 
        Application.Quit();
    }
}