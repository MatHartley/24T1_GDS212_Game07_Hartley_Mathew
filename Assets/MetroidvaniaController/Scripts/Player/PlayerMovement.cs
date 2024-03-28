using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	private float horizontalMove = 0f;
	public bool jump = false;
	public bool dash = false;
	public bool run = false;
	public bool sneak = false;

	private float audioCooldown = 1f;
	private float audioCount;

	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.Z))
		{
			jump = true;
		}
		else
		{
			audioCount += Time.deltaTime;
			if (audioCount >= audioCooldown)
			{
				jump = false;
				audioCount = 0;
			}
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
		}
		else
		{
			audioCount += Time.deltaTime;
			if (audioCount >= audioCooldown)
			{
				dash = false;
				audioCount = 0;
			}
		}
	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);

		if (horizontalMove != 0)
		{
			run = true;
		}
		else
		{
			run = false;
		}
	}
}
