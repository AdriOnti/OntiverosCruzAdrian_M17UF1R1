using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    // Simple script, va siguiendo por cada frame al player
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 1f);
    }
}
