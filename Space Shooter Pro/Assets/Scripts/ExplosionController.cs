﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

    // Start is called before the first frame update
    void Start()  {
        
    }

    // Update is called once per frame
    void Update()  {
        StartCoroutine(SelfDestructDelay());
    }

    IEnumerator SelfDestructDelay() {
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 1f);
    }
}
