using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    //Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("PlayerCharacter").gameObject.GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth.isTakingDamage = true;
            //Debug.Log("In Light");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth.isTakingDamage = false;
        }
    }
}
