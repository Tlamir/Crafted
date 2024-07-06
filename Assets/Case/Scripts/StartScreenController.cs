using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CaseScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
