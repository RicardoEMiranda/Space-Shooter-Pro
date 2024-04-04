using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
    // Start is called before the first frame update

    private float speed = 8f;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
 
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y > 8f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.transform.tag == "Enemy") {
            //Destroy(this.gameObject);
        }
    }
}
