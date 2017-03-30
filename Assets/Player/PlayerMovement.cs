using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character = null;
    private CameraRaycaster raycaster = null;
    private AICharacterControl aiCharacterControl = null;

    private GameObject walkTarget = null;

    private void Start()
    {
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();

        walkTarget = new GameObject("WalkTarget");

        //Register observer to mouse left click events
        raycaster.notifyMouseClickObservers += OnMouseClick;
    }

	/// <summary>
	/// Observer method, called only when the player clicks the left mouse button
	/// </summary>
	/// <param name="raycastHit">Struct containing data regarding the raycast hit</param>
	/// <param name="layer">ID of the layer hit</param>
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
}