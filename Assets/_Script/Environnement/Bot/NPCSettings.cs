using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSettings : MonoBehaviour
{
    public GameObject dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogue.SetActive(true);
    }
}
