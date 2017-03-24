using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    //Keep variable private, but it will be visible in the inspector
    [SerializeField] private float distanceToBackground = 100f;

    private Camera viewCamera;

    private RaycastHit mHit;

    public RaycastHit hit
    {
        get { return mHit; }
    }

    private Layer mLayerHit;

    public Layer layerHit
    {
        get { return mLayerHit; }
    }

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
                mLayerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        mHit.distance = distanceToBackground;
        mLayerHit = Layer.RaycastEndStop;
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