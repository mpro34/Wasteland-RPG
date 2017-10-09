using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
		Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

	//Make it a getter function for geting raycastHit 
    RaycastHit raycastHit;
    public RaycastHit hit
    {
        get { return raycastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }

    //Add a delegate and register observers
    public delegate void OnLayerChange(); //declare new delegate type
    public OnLayerChange layerChangeObservers; //instantiate an observer set

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                raycastHit = hit.Value;
                if (layerHit != layer) //if layer has changed
                {
                    layerHit = layer;
                    layerChangeObservers();  //call the delegates
                }
                layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }

	//? means this can return either a value or null. Without the ?, function can only return 1 value (i.e bool)
    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); //Send ray from position of mouse cursor

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
