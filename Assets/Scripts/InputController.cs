using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public AudioClip audioFire; // 발사 효과음
    AudioSource audioSource;
    [SerializeField] GameObject rightGun, leftGun;
    [SerializeField] GameObject statCanvas;

    public float bulletSpeed = 1000;
    bool isRight;

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // OVRInput 을 통해서 Secondary = 오른쪽 컨트롤러의 IndexTrigger 버튼 누르기
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isRight = true;
            Fire();
            Debug.Log("왼쪽");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isRight = false;
            Fire();
            Debug.Log("오른쪽");
        }
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            statCanvas.SetActive(true);
        }
        else
        {
            statCanvas.SetActive(false);
        }

    }
    void PlaySound(string action)
    {
        if (action == "Fire")
        {
            audioSource.clip = audioFire;
        }
        audioSource.Play();
    }

    public void Fire()
    {
        if (isRight)
        {
            PlaySound("Fire");
            GameObject BulletR = ObjectPooler.SpawnFromPool("BulletR", rightGun.transform.position, rightGun.transform.rotation);
            BulletR.GetComponent<Rigidbody>().velocity = Vector3.zero;
            BulletR.GetComponent<Rigidbody>().AddForce(rightGun.transform.forward * bulletSpeed);
        }
        else
        {
            PlaySound("Fire");
            GameObject BulletL = ObjectPooler.SpawnFromPool("BulletL", leftGun.transform.position, leftGun.transform.rotation);
            BulletL.GetComponent<Rigidbody>().velocity = Vector3.zero;
            BulletL.GetComponent<Rigidbody>().AddForce(leftGun.transform.forward * bulletSpeed);
        }
    }
}
