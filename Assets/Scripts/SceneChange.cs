using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string loadLevel;
    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(loadLevel);
    }
}
