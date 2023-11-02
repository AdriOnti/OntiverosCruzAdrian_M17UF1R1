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
    public GameObject spikes;
    public Transform player;
    public Transform winSpawn;
    private int keysCollected = 0;
    public GameObject pauseMenu;
    private bool isPauseMenu = false;
    public Transform backSpawn;

    private void Start()
    {
        CheckBackSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        isPauseMenu = !isPauseMenu;
        if (Time.timeScale == 1) Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(isPauseMenu);
    }

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
            player.position = winSpawn.position;
            spikes.gameObject.SetActive(false);
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

    void CheckBackSpawn()
    {
        if(Time.timeScale == 1.1f)
        {
            player.position = backSpawn.position;
        }
        Time.timeScale = 1;
    }


    //public void MoveCamera(Vector2 newPosition)
    //{
    //    Camera.main.transform.position = new Vector3(newPosition.x, newPosition.y, Camera.main.transform.position.z);
    //}
}
