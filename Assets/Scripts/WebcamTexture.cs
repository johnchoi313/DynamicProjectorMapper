using UnityEngine;

public class SimpleWebcamTexture : MonoBehaviour {
    public string webcamName; // Name of the webcam
    public int webcamID = 0;  // ID of the webcam (optional)
    private WebCamTexture webcamTexture;

    void Start() {
        // Print all available webcams to the log
        PrintAvailableWebcams();
        // If a webcam name is provided, use it; otherwise, fall back to the webcam ID
        if (!string.IsNullOrEmpty(webcamName)) { SetWebcamByName(webcamName); }
        else { SetWebcamByID(webcamID); }
    }

    private void PrintAvailableWebcams() {
        WebCamDevice[] devices = WebCamTexture.devices;
        // Print each device's ID and name
        for (int i = 0; i < devices.Length; i++) {
            Debug.Log("Webcam ID: " + i + ", Name: " + devices[i].name);
        }
    }

    private void SetWebcamByName(string name) {
        // Find the webcam device by name
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (var device in devices) {
            if (device.name == name) {
                webcamTexture = new WebCamTexture(device.name);
                ApplyTexture();
                return;
            }
        }
        Debug.LogWarning("Webcam with name '" + name + "' not found.");
    }

    private void SetWebcamByID(int id) {
        WebCamDevice[] devices = WebCamTexture.devices;
        // Ensure the ID is within the range of available devices
        if (id >= 0 && id < devices.Length) {
            webcamTexture = new WebCamTexture(devices[id].name);
            ApplyTexture();
        }
        else { Debug.LogWarning("Webcam ID " + id + " is out of range."); }
    }

    private void ApplyTexture() {
        // Apply the webcam texture to the object's material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) {
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else { Debug.LogWarning("Renderer not found on the object."); }
    }

    void OnDisable() {
        // Stop the webcam feed when the object is disabled
        if (webcamTexture != null && webcamTexture.isPlaying) { webcamTexture.Stop(); }
    }
}
