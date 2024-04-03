using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float incomingDamage;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public bool isTakingDamage;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioSource damageSFX;

    private CharacterController2D characterController2D;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        characterController2D = gameObject.GetComponent<CharacterController2D>();
        damageSFX = GameObject.Find("DamageSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTakingDamage)
        {
            //damageSFX.Play(); //only playing when you STOP taking damage?!?
            currentHealth -= (incomingDamage * Time.deltaTime);
            //Debug.Log(currentHealth + "/" + maxHealth);
        }

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            damageSFX.Stop();
            characterController2D.ApplyDamage(10f, transform.position);
            //Debug.Log("I Should Be Dying");
        }
    }
}
