using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character = null;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster raycaster = null;
    private AICharacterControl aiCharacterControl = null;

    private Vector3 currentDestination, clickPoint;
    private GameObject walkTarget = null;

    private void Start()
    {
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();

        currentDestination = transform.position;
        walkTarget = new GameObject("WalkTarget");

        //Register observer to mouse left click events
        raycaster.notifyMouseClickObservers += OnMouseClick;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        
    }

    private void OnMouseClick(RaycastHit raycastHit, int layer)
    {
        switch(layer)
        {
            case Utilities.WalkableLayer:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;

            case Utilities.EnemyLayer:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;

            default:
                Debug.LogWarning("Unknown layer clicked. Layer int: " + layer);
                return;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position, clickPoint);
        //Gizmos.DrawSphere(currentDestination, 0.1f);
        //Gizmos.DrawSphere(clickPoint, 0.2f);

        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}