using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[Header("Enemy Attributes")]
	public float life = 10;
	public float speed = 1f;
	public bool isInvincible = false;

	private bool isHit = false;
	private bool facingRight = true; 
	private Rigidbody2D rb;
	private Animator anim;

	[Header("Patrol Checks")]
	public LayerMask turnLayerMask;

	private bool isPlatform;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;

	[Header("Alert")]
	private float alertTime = 5f;
	private float alertCount = 0f;
	private bool isAlert = false;
	private Transform alertTransform;

	[Header("Player Reference")]
	[SerializeField] GameObject player;

	[Header("Internals")]
	private GameObject lightBeam;

	private void Start()
    {
		anim = GetComponent<Animator>();
		lightBeam = this.transform.GetChild(0).gameObject;
		player = GameObject.Find("PlayerCharacter");
	}

    void Awake () 
	{
		fallCheck = transform.Find("FallCheck");
		wallCheck = transform.Find("WallCheck");
		rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    private void Update()
    {
		if (life <= 0)
		{
			StartCoroutine(DestroyEnemy());
		}

		if (isAlert)
        {
			alertCount += Time.deltaTime;
			//Debug.Log(this.name + ": Alert:" + alertCount);
			if (alertCount >= alertTime)
			{
				isAlert = false;
				alertCount = 0f;
			}
        }
    }

    void FixedUpdate () 
	{
		if (!isAlert)
		{
			if (speed == 0)
			{
				anim.SetBool("IsWaiting", true);
			}
			else
			{
				anim.SetBool("IsWaiting", false);
				if (life <= 0)
				{
					transform.GetComponent<Animator>().SetBool("IsDead", true);
					StartCoroutine(DestroyEnemy());
				}

				isPlatform = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
				isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);

				if (!isHit && life > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
				{
					if (isPlatform && !isObstacle && !isHit)
					{
						if (facingRight)
						{
							rb.velocity = new Vector2(-speed, rb.velocity.y);
						}
						else
						{
							rb.velocity = new Vector2(speed, rb.velocity.y);
						}
					}
					else
					{
						Flip();
					}
				}
			}
		}
		else
		{
			rb.velocity = new Vector2(0, 0);
			anim.SetBool("IsWaiting", true);

			if (alertTransform.position.x < transform.position.x)
			{
				Vector3 theScale = transform.localScale;
				theScale.x = -1;
				transform.localScale = theScale;
				facingRight = true;
			}
			else if (alertTransform.position.x > transform.position.x)
			{
				Vector3 theScale = transform.localScale;
				theScale.x = 1;
				transform.localScale = theScale;
				facingRight = false;
			}
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage) 
	{
		if (!isInvincible) 
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			transform.GetComponent<Animator>().SetBool("Hit", true);
			life -= damage;
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(direction * 500f, 100f));
			StartCoroutine(HitTime());
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Sound")
		{
			isAlert = true;
			alertTransform = collision.transform;
			alertTransform.position = collision.transform.position;
			Debug.Log(collision.transform.position);
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
		}
	}

	IEnumerator HitTime()
	{
		isHit = true;
		isInvincible = true;
		yield return new WaitForSeconds(0.1f);
		isHit = false;
		isInvincible = false;
	}

	IEnumerator DestroyEnemy()
	{
		lightBeam.SetActive(false);
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		transform.GetComponent<Animator>().SetBool("IsDead", true);
		yield return new WaitForSeconds(0.25f);
		rb.velocity = new Vector2(0, rb.velocity.y);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
