using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Header("Flicker Counters")]
    [SerializeField] float offTime;
    private float offCount;
    [SerializeField] float onMax;
    private float onCount;

    [Header("Internals")]
    private GameObject lightBeam;
    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        lightBeam = this.transform.GetChild(0).gameObject;
        //Debug.Log("Hello " + lightBeam.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            lightBeam.SetActive(false);
            offCount += Time.deltaTime;
            if (offCount >= Random.Range (0, offTime))
            {
                isOn = true;
                onCount = 0;
            }
        }
        else if (isOn)
        {
            lightBeam.SetActive(true);
            onCount += Time.deltaTime;
            if (onCount >= Random.Range(0, onMax))
            {
                isOn = false;
                offCount = 0;
            }
        }
    }
}
