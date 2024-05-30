using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
    // Start is called before the first frame update

    public float speed = 8f;
    public float missleSpeed = 4f;

    [SerializeField]
    private bool isMissle;

    [SerializeField]
    private AudioSource audioSource1, audioSource2;

    [SerializeField]
    private AudioClip audioClip_missleLaunch, audioClip_missleThrusters;


    private GameObject enemyTarget;
    private Vector3 navigationVector;
    private float theta;
    private Quaternion missleRotationAngle;
    private float rotSpeed = 10f;
    public bool enemyDetected;
    private Vector3 enemyLocation;

    void Start() {
        theta = 0;
    }

    // Update is called once per frame
    void Update() {
 
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y > 11f || transform.position.y < -11) {
            Destroy(this.gameObject);
        }

        //if(isMissle && enemyTarget != null) {
        if(isMissle && enemyDetected) {
            //Debug.Log("Detector message received");
            SeekTarget();
        }
    }

    public void GetTelemetryData(Vector3 loc) {
        enemyLocation = loc;
        //Debug.Log("Enemy Location X: " + enemyLocation.x + " Location Y: " + enemyLocation.y);
        navigationVector = enemyLocation - gameObject.transform.position;
    }

    private void SeekTarget() {

        theta = Mathf.Atan2(navigationVector.y, navigationVector.x) * Mathf.Rad2Deg - 90;
        missleRotationAngle = Quaternion.AngleAxis(theta, new Vector3(0, 0, 1));
        transform.Translate(navigationVector.normalized * missleSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, missleRotationAngle, Time.deltaTime * rotSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.transform.tag == "Enemy") {
            //Debug.Log("Enemy hit from other");
            Destroy(this.gameObject); 
        }
    }
}
