using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int keysToWin = 3;
    public GameObject activeCheckpoint;
    public Text keyCount;
    public Text keyText;
    public GameObject canvasWGameOver;

    private int keysCollected = 0;


    public void AddKey()
    {
        keysCollected++;
        Debug.Log(keysCollected);
        Debug.Log(CheckWin());
        // Comprobamos si hemos conseguido la llave y lo mostramos
        if (CheckWin() == false)
        {
            keyCount.text = keysCollected.ToString();
        }
        else
        {
            keyCount.text = "";
            keyText.text = "Llaves conseguidas!! Puerta Abierta";
            canvasWGameOver.gameObject.SetActive(true);
        }
    }

    private bool CheckWin()
    {
        Debug.Log(keysCollected);
        Debug.Log(keysToWin);
        return (keysCollected == keysToWin);
    }

    public void ActivateCheckpoint(GameObject newCheckpoint)
    {
        // Quitar Checkpoint antiguo
        if (activeCheckpoint)
        {
            activeCheckpoint.GetComponent<Checkpoint>().Deactivate();
        }

        // Nuevo checkpoint en la puerta
        activeCheckpoint = newCheckpoint;
        activeCheckpoint.GetComponent<Checkpoint>().Activate();
    }


    public void MoveCamera(Vector2 newPosition)
    {
        Camera.main.transform.position = new Vector3(newPosition.x, newPosition.y, Camera.main.transform.position.z);
    }
}
