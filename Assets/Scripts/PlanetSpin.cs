using UnityEngine;

/// <summary>
/// This script causes a planet to spin around his Y-axis to simulate a natural rotation
/// </summary>

public class PlanetSpin : MonoBehaviour
{
    // Speed at which the planet spins (in degrees per second)
    public float spinSpeed = 20f;

    void Update()
    {
        // Continuously rotate the planet around its local Y-axis (up)
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}


