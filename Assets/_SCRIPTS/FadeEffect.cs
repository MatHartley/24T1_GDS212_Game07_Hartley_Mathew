using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private GameObject blackout;
    [SerializeField] private float fadeSpeed;
    public bool fadeOut = false;
    public bool fadeIn = false;

    private float alpha;

    void Start()
    {
        var image = blackout.gameObject.GetComponent<Image>();
        if (image != null)
        {
            alpha = image.color.a; // Get initial alpha of the Image
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    fadeOut = true;
        //    fadeIn = false;
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    fadeOut = false;
        //    fadeIn = true;
        //}
        if (fadeOut)
        {
            alpha = Mathf.Lerp(alpha, 1f, fadeSpeed * Time.deltaTime); // Lerp towards black (alpha 1)

            // Update the alpha of the Image
            var image = blackout.gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.color = new Color(0, 0, 0, alpha);
            }
        }
        else if (fadeIn)
        {
            alpha = Mathf.Lerp(alpha, 0f, fadeSpeed * Time.deltaTime); // Lerp towards clear (alpha 0)

            // Update the alpha of the Image
            var image = blackout.gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
