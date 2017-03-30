using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private int[] layerPriorities;
    [SerializeField] private float maxRaycastDepth = 100.0f;

    private int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

    // Setup delegates for broadcasting layer changes to other classes
    public delegate void OnCursorLayerChange(int newLayer);
    public event OnCursorLayerChange notifyLayerChangeObservers;

    public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);
    public event OnClickPriorityLayer notifyMouseClickObservers;

    private void Update()
    {
        //EventSystem object = interactable UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            NotifyObserversIfLayerChanged(Utilities.UILayer);
            return; // Stop looking for other objects
        }

        // Raycast to max depth, every frame as things can move under mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

		//Check for priority layer hit
        RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
        if (!priorityHit.HasValue)
        {
            NotifyObserversIfLayerChanged(Utilities.DefaultLayer); // broadcast default layer
            return;
        }

        int layerHit = priorityHit.Value.collider.gameObject.layer;     //TODO consider changing this
        NotifyObserversIfLayerChanged(layerHit);

        if (Input.GetMouseButton(Utilities.LeftMouseButton))
        {
            notifyMouseClickObservers(priorityHit.Value, layerHit);
        }
    }

    private void NotifyObserversIfLayerChanged(int newLayer)
    {
        if (newLayer != topPriorityLayerLastFrame)
        {
            topPriorityLayerLastFrame = newLayer;
            notifyLayerChangeObservers(newLayer);
        }
    }

    private RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits)
    {
        // Form list of layer numbers hit
        List<int> layersOfHitColliders = new List<int>();
        foreach (RaycastHit hit in raycastHits)
        {
            layersOfHitColliders.Add(hit.collider.gameObject.layer);
        }

        // Step through layers in order of priority looking for a gameobject with that layer
        foreach (int layer in layerPriorities)
        {
            foreach (RaycastHit hit in raycastHits)
            {
                if (hit.collider.gameObject.layer == layer)
                {
                    return hit;
                }
            }
        }
        return null; // because we cannot use GameObject? nullable
    }
}