using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private GameObject player;

    private void Start()
    {
        //Locate player object by tag
        player = GameObject.FindGameObjectWithTag(PlayerTag);
        print(PlayerTag + " found!!");
    }

    //Called in LateUpdate because the player is moved inside Update
    private void LateUpdate()
    {
        this.transform.position = player.transform.position;
    }
}