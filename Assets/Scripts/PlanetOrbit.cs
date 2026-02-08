using UnityEngine;

/// <summary>
/// Makes a planet orbit around its parent object by rotating it
/// around its own Y-axis at a specified speed.
/// </summary>
public class PlanetOrbit : MonoBehaviour
{
    // The speed at which the planet orbits around its parent (degrees per second)
    public float orbitSpeed = 10f;

    // This static flag controls whether orbiting is turned on or off for all planets
    public static bool rotationEnabled = false;

    void Update()
    {
        // If rotation is enabled, rotate the planet around the Y axis
        if (rotationEnabled)
        {
            // Rotate the planet around its local up (Y) axis
            transform.Rotate(Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}

