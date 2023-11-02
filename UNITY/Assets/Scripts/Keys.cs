using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public GameController gameController;

    // Script no usado (lo guardo por si acaso), al final lo implementé en player
    // Cuando player la coja llama a la función de añadir key y se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.AddKey();
            Destroy(gameObject);
        }
    }
}
