using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAction : MonoBehaviour
{
    [SerializeField] private Texture2D walkCursor = null;
    [SerializeField] private Texture2D attackCursor = null;
    [SerializeField] private Texture2D unknownCursor = null;
    [SerializeField] private Vector2 cursorHotspot = new Vector2(96.0f, 96.0f);

    private CameraRaycaster raycaster;
    private Texture2D chosenCursorTexture = null;
    private Layer previouslyHitLayer, layerHit;

    private void Start()
    {
        raycaster = GetComponent<CameraRaycaster>();
    }

    private void Update()
    {
        layerHit = raycaster.layerHit;

        if (!layerHit.Equals(previouslyHitLayer))
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

        previouslyHitLayer = layerHit;
    }
}