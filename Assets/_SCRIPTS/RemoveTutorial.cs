using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tutorialPanel.SetActive(false);
    }
}
