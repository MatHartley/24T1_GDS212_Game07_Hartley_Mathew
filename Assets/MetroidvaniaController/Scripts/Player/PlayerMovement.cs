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
	private Transform alertTransform;

	private int alertX;
	private int alertY;
	private int alertZ;

	[Header("Audio Emission")]
	[SerializeField] private float audioTimerReset = 1f;
	/// <summary>
	/// Should countdown
	/// </summary>
	private float audioTimer;
	[SerializeField] private GameObject sneakCircle;
	[SerializeField] private GameObject runCircle;
	[SerializeField] private GameObject loudCircle;

	[Header("Script References")]
	public CharacterController2D controller;
	public AudioEmission audioEmission;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
	{
		audioTimer -= Time.deltaTime;

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.Z))
		{
			jump = true;
			AlertCircle(loudCircle);
		}

        if (Input.GetKeyUp(KeyCode.Z))
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
			AlertCircle(loudCircle);
		}

		if (Input.GetKeyUp(KeyCode.C))
		{
			dash = false;		
		}
	}
	
	private void AlertCircle(GameObject alert)
	{
		if (alert.gameObject.name == "loudCircle")
        {
			audioTimer = 0;
        }
		if (audioTimer <= 0)
		{
			alertTransform = (this.transform);
			GameObject thisAlert = Instantiate(alert, transform.position, Quaternion.identity);
			Destroy(thisAlert, 1);
			audioTimer = audioTimerReset;
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
			AlertCircle(runCircle);
		}
		else
		{
			run = false;
		}
	}
}
