using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
    // Start is called before the first frame update

    public float speed = 8f;
    public float missleSpeed = 4f;

    [SerializeField]
    private bool isMissle;

    private GameObject enemyTarget;
    private Vector3 navigationVector;
    private float theta;
    private Quaternion missleRotationAngle;
    private float rotSpeed = 10f;
    private float missleTimer;

    void Start() {
        enemyTarget = GameObject.FindWithTag("Enemy");
        theta = 0;
        missleTimer = 0;
    }

    // Update is called once per frame
    void Update() {
 
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y > 8f) {
            Destroy(this.gameObject);
        }

        if(isMissle && enemyTarget != null) {
            FindTarget();
        }
    }

    private void FindTarget() {
        missleTimer += Time.deltaTime;
        if(missleTimer<=1.5f) {
            navigationVector = enemyTarget.transform.position - transform.position;
            theta = Mathf.Atan2(navigationVector.y, navigationVector.x) * Mathf.Rad2Deg - 90;
            missleRotationAngle = Quaternion.AngleAxis(theta, new Vector3(0, 0, 1));

            if (navigationVector.magnitude > .1) {
                transform.Translate(navigationVector.normalized * missleSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, missleRotationAngle, Time.deltaTime * rotSpeed);
            } else {
                transform.Translate(navigationVector.normalized * missleSpeed * Time.deltaTime);
            }

        } 

        if (transform.position.y >10 || transform.position.y<-10) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.transform.tag == "Enemy") {
            Debug.Log("Enemy hit from other");
            Destroy(this.gameObject); 
        }
    }
}
