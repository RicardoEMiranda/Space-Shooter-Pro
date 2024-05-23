using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject pauseModal;

    private SpawnManager spawnManager;
    public float waveTimer;
    public int waveNumber;
    public bool restartState;
    private bool asteroidDestroyed;

    // Start is called before the first frame update
    void Start() {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        waveTimer = 0;
        waveNumber = 0;
        restartState = false;

        if(spawnManager == null) {
            Debug.Log("Spawn Manager object not found");
        }
    }

    // Update is called once per frame
    void Update() {

        ManageEnemyWaves();

    }

    private void ManageEnemyWaves() {
        //keep track of game play running time
        
        if (asteroidDestroyed) { 
        waveTimer += Time.deltaTime;

        if (waveTimer < 20f) {
            //For the first 20 seconds
            //Wave 0, tell spawn manager to spawn an enemy once every 2-3 seconds
            waveNumber = 0;
        } else if (waveTimer >= 20f && waveTimer < 45f) {
            //Between 21-45 seconds, Wave 1, tell spawn manager to spawn an enemy once every 1-2 seconds
            waveNumber = 1;
        } else if (waveTimer >= 45 && waveTimer < 60) {
            //Between 45 - 60 seconds, Wave 2, tell spawn manager to spawn an enemy once every .5 - 1 seconds
            waveNumber = 2;
        } else if (waveTimer >= 60) {
            //at 70 seconds, stop spawning to get ready for the boss battle
            waveNumber = 3;
            //spawnManager.StopSpawning();
        } else {
            Debug.Log("Wave Timer Exception at GameManager");
        }
        //Debug.Log("Wave: " + waveNumber + "   Wave Timer: " + waveTimer);
        }
    }

    public void StartWaveTimer() {
        waveTimer = 0;
        waveNumber = 0;
        asteroidDestroyed = true;
    }

    public void RestartButtonClicked() {
        Debug.Log("Restart clicked");
        SceneManager.LoadScene("GameScene");
        StartWaveTimer();
        //waveNumber = 0;
        //restartState = true;
        //restartTime = Time.time;
        //Debug.Log("Wave Number: " + waveNumber);
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