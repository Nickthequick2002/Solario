using UnityEngine;

/// <summary>
/// This script manages the UI button that toggles the orbital rotation
/// of all planets in the solar system by enabling or disabling.
/// </summary>


public class PlanetTapHandler : MonoBehaviour
{
    // Reference to the AR camera (used to cast rays from screen touches/clicks)
    public Camera arCamera;

    // Reference to the UI display script that shows planet name and description
    public PlanetInfoDisplay infoDisplay;

    void Update()
    {
#if UNITY_EDITOR
        // In the Unity Editor: allow testing with mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position into the 3D scene
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            HandleRaycast(ray);
        }
#else
        // On an actual mobile device: use finger touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the touch position into the 3D scene
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            HandleRaycast(ray);
        }
#endif
    }


    // This method checks if the ray hit a planet, and displays its info
    void HandleRaycast(Ray ray)
    {
        // Cast a ray into the scene and check if it hits a collider
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Try to get the PlanetInfo component from the hit object
            PlanetInfo info = hit.transform.GetComponent<PlanetInfo>();

            // If the object has planet info, show it in the UI
            if (info != null)
            {
                infoDisplay.ShowInfo(info.planetName, info.planetDescription);
            }
        }
    }
}
