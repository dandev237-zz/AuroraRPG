using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Utilities.PlayerTag);
    }

    //Called in LateUpdate because the player is moved inside Update
    private void LateUpdate()
    {
        this.transform.position = player.transform.position;
    }
}