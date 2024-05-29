using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour {

    [SerializeField]
    public bool fire;

    [SerializeField]
    private GameObject laserContainer;

    [SerializeField]
    private BoxCollider2D laserColliderLeft, laserColliderRight;

    [SerializeField]
    private AudioSource audioSource_laserBeamRay, audioSource_laserBeam;

    private GameObject player;


    // Start is called before the first frame update
    void Start()  {
        player = GameObject.Find("Player");
        fire = true;
    }

    // Update is called once per frame
    void Update() {
        FireLaser();

    }



    /*IEnumerator PauseAttackCenter() {
        goCenter = false;
        yield return new WaitForSeconds(pauseDelay);
        goLeft = true;
        attackSequence += 1;
        //Debug.Log(attackSequence);
    }*/

    private void FireLaser() {
        if(fire) {
            laserContainer.SetActive(true);
            laserColliderLeft.enabled = true;
            laserColliderRight.enabled = true;
            audioSource_laserBeamRay.enabled = true;
            audioSource_laserBeam.enabled = true;
            audioSource_laserBeamRay.volume = .25f;
            audioSource_laserBeam.volume = .25f;
        } else if(!fire) {
            laserContainer.SetActive(false);
            laserColliderLeft.enabled = false;
            laserColliderRight.enabled = false;
            audioSource_laserBeamRay.enabled = false;
            audioSource_laserBeam.enabled = false;
        } else {
            Debug.Log("Fire laser (activate laser) exception");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.transform.tag == "Player") {
            Debug.Log("Player Hit!");
           
            //Destroy(this.gameObject);
        }
    }
}
