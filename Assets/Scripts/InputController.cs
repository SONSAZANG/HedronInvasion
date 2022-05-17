using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public AudioClip audioFire; // �߻� ȿ����
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
        
        // OVRInput �� ���ؼ� Secondary = ������ ��Ʈ�ѷ��� IndexTrigger ��ư ������
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isRight = true;
            Fire();
            Debug.Log("����");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isRight = false;
            Fire();
            Debug.Log("������");
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
