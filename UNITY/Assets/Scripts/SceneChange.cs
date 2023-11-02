using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string loadLevel;
    private AudioSource audioManager;
    public AudioClip tpSceneAudio;
    public bool isBakcSpwan;

    void Start()
    {
        audioManager = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Player player = other.collider.GetComponent<Player>();
        
        if (other.gameObject.CompareTag("Player"))
        {
            // Imposible que me funcionara el maldito dont destroy on load ;)

            //if (player != null)
            //{
            //    Debug.Log("p");
            //    DontDestroyOnLoad(other.gameObject);
            //}
            other.gameObject.SetActive(false);
            LoadLevel();
        }
    }

    // Carga nivel con espera de un segundo pa que se escuche el sonido de mario tuberias
    public void LoadLevel()
    {
        audioManager.PlayOneShot(tpSceneAudio);
        StartCoroutine(WaitBeforeLoadLevel());
    }

    IEnumerator WaitBeforeLoadLevel()
    {
        // Manera bruta de comprobar si viene de una escena posterior (si quiere ir hacia atrás)
        if (isBakcSpwan)
        {
            Time.timeScale = 1.1f;
        }
        else Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(loadLevel);
    }
}
