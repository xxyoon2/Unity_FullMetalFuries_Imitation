using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private readonly string _worldSceneName = "0_World";

    public void ClickStart()
    {
        SceneManager.LoadScene(_worldSceneName);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
