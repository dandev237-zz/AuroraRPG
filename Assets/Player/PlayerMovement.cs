using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkMoveStopRadius = 0.2f;
    [SerializeField] private float attackRange = 5.0f;

    private ThirdPersonCharacter character;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster raycaster;
    private Vector3 currentDestination, clickPoint;

    private void Start()
    {
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
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
            clickPoint = raycaster.hit.point;
            switch (raycaster.layerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;

                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackRange);
                    break;

                default:
                    print("Unexpected layer found");
                    break;
            }
        }

        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            character.Move(playerToClickPoint, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    private Vector3 ShortDestination(Vector3 clickPoint, float reductionFactor)
    {
        Vector3 shorteningVector = (clickPoint - transform.position).normalized * reductionFactor;
        return clickPoint - shorteningVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.2f);

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}