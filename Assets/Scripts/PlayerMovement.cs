using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkMoveStopRadius = 0.2f;

    private ThirdPersonCharacter mCharacter;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster mRaycaster;
    private Vector3 currentClickTarget;

    private void Start()
    {
        mRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        mCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(Utilities.LeftMouseButton))
        {
            //Collider hitCollider = mRaycaster.hit.collider;
            //print("Cursor raycast hit layer " + hitCollider.gameObject.name.ToString());

            switch (mRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = mRaycaster.hit.point;
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
            mCharacter.Move(currentClickTarget - transform.position, false, false);
        }
        else
        {
            mCharacter.Move(Vector3.zero, false, false);
        }
    }
}