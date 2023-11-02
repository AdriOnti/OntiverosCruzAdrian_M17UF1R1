using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string loadLevel;
    private AudioSource audioManager;
    public AudioClip tpSceneAudio;

    void Start()
    {
        audioManager = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Player player = other.collider.GetComponent<Player>();
        
        if (other.gameObject.CompareTag("Player"))
        {
            //if (player != null)
            //{
            //    Debug.Log("p");
            //    DontDestroyOnLoad(other.gameObject);
            //}
            other.gameObject.SetActive(false);
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        audioManager.PlayOneShot(tpSceneAudio);
        StartCoroutine(WaitBeforeLoadLevel());
    }

    IEnumerator WaitBeforeLoadLevel()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(loadLevel);
    }
}
