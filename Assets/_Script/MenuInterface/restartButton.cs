using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartButton : MonoBehaviour
{
    public void restartFunction()
    {
        SceneManager.LoadScene(0);
    }
}
