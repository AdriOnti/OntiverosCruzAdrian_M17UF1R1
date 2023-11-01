using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string loadLevel;
    void OnTriggerEnter(Collider other)
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(loadLevel);
    }
}
