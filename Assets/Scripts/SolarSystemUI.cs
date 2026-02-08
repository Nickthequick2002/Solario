using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages the UI button that makes the planets rotate around the sun
/// </summary>

public class SolarSystemUI : MonoBehaviour
{
    // Reference to the UI Button in the scene used to toggle rotation
    public Button rotationButton;

    void Start()
    {
        // When the button is clicked, call the ToggleRotation method
        rotationButton.onClick.AddListener(ToggleRotation);
    }

    // This method flips the static rotationEnabled flag in the PlanetOrbit script
    // It enables or disables orbiting for all planets
    void ToggleRotation()
    {
        PlanetOrbit.rotationEnabled = !PlanetOrbit.rotationEnabled;
    }
}
