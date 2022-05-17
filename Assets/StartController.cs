using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public AudioClip audioNext;
    AudioSource audioSource;
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.Any))
        {
            StartBtnClick();
        }
    }

    public void StartBtnClick()
    {
        audioSource.clip = audioNext;
        SceneManager.LoadScene("GameScene");
        audioSource.Play();
    }
}
