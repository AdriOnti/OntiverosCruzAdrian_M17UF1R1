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
    public Transform spawn;
    private AudioSource audioManager;
    public AudioClip winsound;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();
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
        Debug.Log(IsWin());

        // Comprobamos si hemos conseguido 1 o todas las llaves
        if (keysCollected != keysToWin)
        {
            keyCount.text = keysCollected.ToString();
        }
        else
        {
            keyCount.text = "";
            keyText.text = "Llaves conseguidas!! Puerta Abierta";
            player.gameObject.SetActive(false);
            spikes.gameObject.SetActive(false);
            audioManager.PlayOneShot(winsound);
            StartCoroutine(IsWin());
        }
    }

    IEnumerator IsWin()
    {
        yield return new WaitForSeconds(4);
        player.position = winSpawn.position;
        player.gameObject.SetActive(true);
    }

    public void ActivateCheckpoint(GameObject newCheckpoint)
    {
        // Quitar Checkpoint antiguo, spawn and backspawn
        if (activeCheckpoint)
        {
            activeCheckpoint.GetComponent<Checkpoint>().Deactivate();
            backSpawn.gameObject.SetActive(false);
            spawn.gameObject.SetActive(false);
        }

        // Nuevo checkpoint
        activeCheckpoint = newCheckpoint;
        activeCheckpoint.GetComponent<Checkpoint>().Activate();
    }

    // Comprobamos si viene de una escena posterior (lo se es una manera bruta de hacerlo)
    void CheckBackSpawn()
    {
        if(Time.timeScale == 1.1f)
        {
            backSpawn.gameObject.SetActive(true);
            player.position = backSpawn.position;
        }
        Time.timeScale = 1;
    }
}
