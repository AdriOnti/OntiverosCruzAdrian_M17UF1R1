using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Con ayuda de internet conseguí esto, que sirve no solo en "x" si no que en "y" también (incluso yendo en diagonal)
[DisallowMultipleComponent]
public class LoopMove : MonoBehaviour
{
    [SerializeField] Vector2 movementVector = new Vector2(10f, 10f);
    [SerializeField] float period = 2f;
    public bool isStop = false;

    public float movementFactor;

    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {

        if (period <= Mathf.Epsilon || isStop) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

    public void Stop()
    {
        isStop = true;
    }
}