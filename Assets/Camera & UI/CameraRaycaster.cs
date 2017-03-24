using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] private float distanceToBackground = 100f;

    private Camera viewCamera;
    private RaycastHit mHit;
    private Layer mLayerHit;

    public RaycastHit hit
    {
        get { return mHit; }
    }

    public Layer layerHit
    {
        get { return mLayerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer);       //Delegate type

    public event OnLayerChange layerChangeObservers;  //Observers set instance

    private void Start()
    {
        viewCamera = Camera.main;
    }

    private void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                mHit = hit.Value;
                if (layerHit != layer)       //There has been a change in the layer hit by the ray
                {
                    mLayerHit = layer;
                    layerChangeObservers(layer);
                }

                return;
            }
        }

        // Otherwise return background hit
        mHit.distance = distanceToBackground;
        if (layerHit != Layer.RaycastEndStop)
        {
            mLayerHit = Layer.RaycastEndStop;
            layerChangeObservers(layerHit);
        }
    }

    //Nullable value type (This function can return null)
    private RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}