using UnityEngine;

/// <summary>
/// This script holds data related to a planet, including its display name and description.
/// </summary>
public class PlanetInfo : MonoBehaviour
{
    // This will be the display name of the planet
    [TextArea]
    public string planetName;

    // This field holds a short description about the planet
    [TextArea]
    public string planetDescription;
}


