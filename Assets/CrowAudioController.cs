using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAudioController : MonoBehaviour
{
    public AudioSource crowAudioSource;
    public AudioClip[] crowAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        //crowAudioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioLoop());

        IEnumerator PlayAudioLoop()
        {
            crowAudioSource.clip = crowAudioClip[Random.Range(0, crowAudioClip.Length)];
            crowAudioSource.Play();
            yield return new WaitForSeconds(Random.Range(5.0f, 13.0f));
            StartCoroutine(PlayAudioLoop());
        }   
    }
}
