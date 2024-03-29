using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	[Header("Character Attributes")]
	public float runSpeed = 40f;
	private float horizontalMove = 0f;
	public Animator animator;

	[Header("Movement States")]
	public bool jump = false;
	public bool dash = false;
	public bool run = false;
	public bool sneak = false;

	[Header("Audio Emission")]
	private float audioCooldown = 1f;
	private float audioCount;

	[Header("Script References")]
	public CharacterController2D controller;

    // Update is called once per frame
    void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.Z))
		{
			jump = true;
		}

        if (Input.GetKeyUp(KeyCode.Z))
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
		}

		if (Input.GetKeyUp(KeyCode.C))
		{
			dash = false;		
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
