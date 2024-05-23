using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Vector3 originalPosition;
    public Vector3 newPosition;
    private float timer;
    private float shakeDuration;
    private float settleTimer, settleTime;
    public bool shakingFinished;
    
    void Start()  {
        originalPosition = new Vector3(0, 1, -12);
        timer = 0;
        settleTimer = 0;
        settleTime = .5f;
        shakeDuration = 2f;
        shakingFinished = true;
    }

    
    public IEnumerator ShakeCamera() {
        while(timer < shakeDuration) {
            timer += Time.deltaTime;
            float x = Random.Range(-.25f, .25f);
            float y = Random.Range(-.25f, .25f);
            newPosition = new Vector3(x, y, originalPosition.z);
            float magnitude = Random.Range(1f, 3f);
            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, .1f);
            yield return null;
        }
        timer = 0;
        shakingFinished = true;
        //this.transform.position = originalPosition;
    }

    public IEnumerator RecenterCamera() {
        //Vector3 positionDelta = originalPosition - this.transform.position;
        //float positionDeltaMagnitude = positionDelta.magnitude;
        //while (positionDeltaMagnitude > .01) {
            //positionDelta = originalPosition - this.transform.position;
            //positionDeltaMagnitude = positionDelta.magnitude;
            while (settleTimer < settleTime) {
            //Debug.Log("Settling");
            settleTimer += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, originalPosition, .1f);
            yield return null;
        }
        settleTimer = 0;
        this.transform.position = originalPosition;
    }


}
