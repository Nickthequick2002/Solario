using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// This script manages the placement of an AR solar system prefab when the user taps on a detected plane.
/// It also controls background music playback and the visibility of plane detection and a remove button.
/// </summary>


public class SolarSystemPlacer : MonoBehaviour
{
    [Header("AR Components")]
    public GameObject solarSystemPrefab;      // The prefab of the entire solar system
    public ARRaycastManager raycastManager;   // AR raycast manager for plane detection
    public ARPlaneManager planeManager;       // Used to disable planes after placement

    [Header("UI")]
    public GameObject removeButton;           // A button that lets the user remove the solar system

    [Header("Audio")]
    public AudioClip backgroundMusic;         // Background music that plays when the solar system appears

    private AudioSource backgroundSource;     // Audio source component to play the background music
    private GameObject musicObject;           // GameObject to hold the audio source
    private GameObject placedObject;          // The spawned instance of the solar system
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();  // Reusable list for raycast results

    void Start()
    {
        // Hide the remove button at the beginning
        removeButton.SetActive(false);
    }

    void Update()
    {
#if UNITY_EDITOR
        // Simulated placement for testing in Unity Editor with mouse click
        if (placedObject == null && Input.GetMouseButtonDown(0))
        {
            // Place object in front of camera
            Vector3 fakePosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
            PlaceSolarSystem(fakePosition, Quaternion.identity);
        }
#else
        // Real device: touch input for placing the solar system
        if (placedObject != null) return; // Prevent placing more than one

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            // Raycast against detected planes
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                PlaceSolarSystem(hitPose.position, hitPose.rotation);
            }
        }
#endif

        // Show or hide the remove button depending on whether the solar system exists
        if (placedObject != null && !removeButton.activeSelf)
            removeButton.SetActive(true);
        else if (placedObject == null && removeButton.activeSelf)
            removeButton.SetActive(false);
    }

    // Instantiate the solar system prefab, scale it down, and disable planes
    private void PlaceSolarSystem(Vector3 position, Quaternion rotation)
    {
        placedObject = Instantiate(solarSystemPrefab, position, rotation);
        placedObject.transform.localScale = Vector3.one * 0.1f;

        // Disable plane detection and hide existing planes
        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        PlayBackgroundMusic(); // Start background music
    }

    // Called when the user presses the remove button
    public void RemoveSolarSystem()
    {
        if (placedObject != null)
        {
            Destroy(placedObject);
            placedObject = null;

            // Re-enable plane detection and make planes visible again
            planeManager.enabled = true;
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }

            StopBackgroundMusic(); // Stop the music
        }
    }

    // Create an audio source and play looping background music
    private void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogWarning("Background music not assigned.");
            return;
        }

        // Only create music object once
        if (musicObject == null)
        {
            musicObject = new GameObject("BackgroundMusic");
            backgroundSource = musicObject.AddComponent<AudioSource>();
            backgroundSource.loop = true;
            backgroundSource.playOnAwake = false;
            backgroundSource.spatialBlend = 0f; // 2D sound
            backgroundSource.volume = 0.5f;
        }

        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    // Stop the background music when the solar system is removed
    private void StopBackgroundMusic()
    {
        if (backgroundSource != null && backgroundSource.isPlaying)
        {
            backgroundSource.Stop();
        }
    }
}
