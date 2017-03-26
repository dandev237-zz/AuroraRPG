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

    private void Start()
    {
        raycaster = GetComponent<CameraRaycaster>();
        raycaster.notifyLayerChangeObservers += ChangeCursorTexture;
    }

    /// <summary>
    /// Observer method, called only when CameraRaycaster notifies a change in the layer hit with its ray
    /// </summary>
    /// <param name="newLayer">The layer that has just been hit by the raycaster</param>
    private void ChangeCursorTexture(int newLayer)
    {
        switch (newLayer)
        {
            case Utilities.WalkableLayer:
                chosenCursorTexture = walkCursor;
                break;

            case Utilities.EnemyLayer:
                chosenCursorTexture = attackCursor;
                break;

            default:
                chosenCursorTexture = unknownCursor;
                break;
        }

        Cursor.SetCursor(chosenCursorTexture, cursorHotspot, CursorMode.Auto);
    }
}