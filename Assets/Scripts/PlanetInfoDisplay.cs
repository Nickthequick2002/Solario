using UnityEngine;
using TMPro;

/// <summary>
/// Handles displaying information about a selected planet using a UI panel.
/// When a planet is tapped, this script updates the planet's name and description on screen and makes the panel visible.
/// </summary>

public class PlanetInfoDisplay : MonoBehaviour
{
    // The entire UI panel that shows the info (can be shown/hidden)
    public GameObject infoPanel;

    // Text element for the planet's name (uses TextMeshPro)
    public TMP_Text planetNameText;

    // Text element for the planet's description or facts
    public TMP_Text planetFactsText;

    // This method is called when a planet is tapped.
    // It updates the UI text fields and shows the panel.
    public void ShowInfo(string name, string description)
    {
        planetNameText.text = name;
        planetFactsText.text = description;
        infoPanel.SetActive(true);  // Make the panel visible
    }

    // This method hides the info panel (can be called by a close button)
    public void HideInfo()
    {
        infoPanel.SetActive(false);  // Make the panel invisible
    }
}

