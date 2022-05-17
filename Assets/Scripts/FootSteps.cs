using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
	float Timer = 0.0f;
	CharacterController OVR;

	public AudioSource audioSource;
	public AudioClip audioWalk;

	// Use this for initialization
	void Start()
	{
		this.audioSource = GetComponent<AudioSource>();
		OVR = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (OVR.isGrounded == true && OVR.velocity.magnitude > 2.0f)
		{
			if (Timer > 0.3f)
			{
				audioSource.clip = audioWalk;
				audioSource.Play();
				Timer = 0.0f;
			}

			Timer += Time.deltaTime;
		}
	}
}
