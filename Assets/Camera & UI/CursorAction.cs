using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAction : MonoBehaviour
{
    [SerializeField] private Texture2D walkCursor = null;
    [SerializeField] private Texture2D attackCursor = null;
    [SerializeField] private Texture2D unknownCursor = null;
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

    private CameraRaycaster raycaster;
    private Texture2D chosenCursorTexture = null;
    private Layer lastLayerHit, currentLayerHit;

    private void Start()
    {
        raycaster = GetComponent<CameraRaycaster>();
    }

    private void LateUpdate()
    {
        currentLayerHit = raycaster.layerHit;

        if (!currentLayerHit.Equals(lastLayerHit))
        {
            switch (raycaster.layerHit)
            {
                case Layer.Walkable:
                    chosenCursorTexture = walkCursor;
                    break;

                case Layer.Enemy:
                    chosenCursorTexture = attackCursor;
                    break;

                case Layer.RaycastEndStop:
                    chosenCursorTexture = unknownCursor;
                    break;

                default:
                    Debug.LogError("I don't know what cursor to show.");
                    return;
            }
        }

        Cursor.SetCursor(chosenCursorTexture, cursorHotspot, CursorMode.Auto);

        lastLayerHit = currentLayerHit;
    }
}