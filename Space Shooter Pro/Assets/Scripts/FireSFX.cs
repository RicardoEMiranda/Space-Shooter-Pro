using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSFX : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip_Fire;

    private bool audioSourceActive;

    // Start is called before the first frame update
    void Start() {
        audioSource.enabled = true;
        audioSource.clip = audioClip_Fire;
        audioSource.Play();
        audioSource.volume = .5f;
    }

}
