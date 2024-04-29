using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private GameObject explosionBig;

    [SerializeField]
    private SpawnManager spawnManager;

    private float speed = 12f;

    // Start is called before the first frame update
    void Start() {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.transform.tag == "Laser") {
            //Debug.Log("Hit by Laser");
            Instantiate(explosionBig, transform.position, Quaternion.identity);
            spawnManager.StartSpawning();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
