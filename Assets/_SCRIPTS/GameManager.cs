using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject assassinTarget;
    [SerializeField] GameObject[] potentialTargets;
    [SerializeField] GameObject assassinPortrait;
    [SerializeField] GameObject[] potentialPortraits;
    private int targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        targetIndex = Random.Range(0, potentialTargets.Length);
        assassinTarget = potentialTargets[targetIndex];
        assassinPortrait = potentialPortraits[targetIndex];
        assassinPortrait.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (assassinTarget == null)
        {
            Debug.Log("YOU WIN");
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
