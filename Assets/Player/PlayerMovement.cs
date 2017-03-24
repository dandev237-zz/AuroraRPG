using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkMoveStopRadius = 0.2f;

    private ThirdPersonCharacter character;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster raycaster;
    private Vector3 currentClickTarget;

    private void Start()
    {
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        ProcessMouseMovement();
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(Utilities.LeftMouseButton))
        {
            switch (raycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = raycaster.hit.point;
                    break;

                case Layer.Enemy:
                    print("Not moving to an enemy!!");
                    break;

                default:
                    print("Unexpected layer found");
                    break;
            }
        }

        Vector3 playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            character.Move(currentClickTarget - transform.position, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        currentClickTarget = transform.position;    //So that the character doesn't run without the left click button being held down
    }
}