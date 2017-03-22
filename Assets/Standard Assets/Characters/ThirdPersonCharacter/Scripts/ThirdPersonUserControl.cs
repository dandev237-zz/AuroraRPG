using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter mCharacter;        // A reference to the ThirdPersonCharacter on the object
        private Transform mCam;                         // A reference to the main camera in the scenes transform
        private Vector3 mCamForwardVector;              // The current forward direction of the camera
        private Vector3 mMove;
        private bool mJump;                             // the world-relative desired move direction, calculated from the camForward and user input.

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                mCam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            mCharacter = GetComponent<ThirdPersonCharacter>();
        }

        private void Update()
        {
            if (!mJump)
            {
                mJump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (mCam != null)
            {
                // calculate camera relative direction to move:
                mCamForwardVector = Vector3.Scale(mCam.forward, new Vector3(1, 0, 1)).normalized;
                mMove = v * mCamForwardVector + h * mCam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                mMove = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) mMove *= 0.5f;
#endif

            // pass all parameters to the character control script
            mCharacter.Move(mMove, crouch, mJump);
            mJump = false;
        }
    }
}