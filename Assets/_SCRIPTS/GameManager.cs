using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] GameObject deadTarget;
    [SerializeField] GameObject[] potentialDead;
    [SerializeField] GameObject assassinTarget;
    [SerializeField] GameObject[] potentialTargets;
    [SerializeField] GameObject assassinPortrait;
    [SerializeField] GameObject[] potentialPortraits;
    private int targetIndex;
    
    [Header("UI References")]
    [SerializeField] private GameObject winPanel;

    [Header("Script References")]
    [SerializeField] FadeEffect fadeEffect;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        targetIndex = Random.Range(0, potentialTargets.Length);
        assassinTarget = potentialTargets[targetIndex];
        assassinPortrait = potentialPortraits[targetIndex];
        assassinPortrait.SetActive(true);
        deadTarget = potentialDead[targetIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (assassinTarget == null)
        {
            fadeEffect.fadeOut = true;
            StartCoroutine(CountOut());
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

    private IEnumerator CountOut()
    {
        yield return new WaitForSeconds(3f);
        winPanel.SetActive(true);
        deadTarget.SetActive(true);
        Cursor.visible = true;
    }
}