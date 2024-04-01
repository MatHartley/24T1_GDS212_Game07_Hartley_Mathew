using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float incomingDamage;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    private bool isTakingDamage;

    [SerializeField] Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTakingDamage)
        {
            currentHealth -= (incomingDamage * Time.deltaTime);
            Debug.Log(currentHealth + "/" + maxHealth);
        }

        healthSlider.value = currentHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "LightDamage")
        {
            isTakingDamage = true;
            Debug.Log("In Light");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LightDamage")
        {
            isTakingDamage = false;
        }
    }
}
