using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] ExportTransitionAnimation transition;
    [SerializeField] int sceneInt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartChangeScene());
    }

    IEnumerator StartChangeScene()
    {
        transition.CanUseAnimation();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneInt);
    }
}
