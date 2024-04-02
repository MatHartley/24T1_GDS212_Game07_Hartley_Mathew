using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmission : MonoBehaviour
{
    [Header("Audio Cues")]
    [SerializeField] GameObject sneakCircle;
    [SerializeField] GameObject runCircle;
    [SerializeField] GameObject loudCircle;

    [Header("Script References")]
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (sneakCircle == null) return;

        if (playerMovement.sneak)
        {
            sneakCircle.SetActive(true);
        }
        else
        {
            sneakCircle.SetActive(false);
        }

        if (playerMovement.run)
        {
            runCircle.SetActive(true);
        }
        else
        {
            runCircle.SetActive(false);
        }

        if (playerMovement.jump)
        {
            loudCircle.SetActive(true);
        }
        else
        {
            loudCircle.SetActive(false);
        }
    }
}
