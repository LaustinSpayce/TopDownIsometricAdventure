using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

	public CinemachineVirtualCamera[] Cameras;
    public int initialCameraIndex = 0;

    public int inactivePriority = 0;
    public int activePriority = 10;

	int activeCamera;

	// Use this for initialization
	void Start ()
	{
        SetCamera(initialCameraIndex);
	}
	
    // Ray gave me these cool inline if statements to use. Neato.
    public void IncreaseCameraIndex()
    {
        Cameras[activeCamera].Priority = inactivePriority;
        activeCamera = (activeCamera == Cameras.Length - 1 ? 0 : activeCamera + 1);
        Cameras[activeCamera].Priority = activePriority;
    }

    public void DecreaseCameraIndex()
    {
        Cameras[activeCamera].Priority = inactivePriority;
        activeCamera = (activeCamera == 0 ? Cameras.Length - 1 : activeCamera - 1);
        Cameras[activeCamera].Priority = activePriority;
    }

    public void SetCamera(int cameraIndex)
    {
        if (cameraIndex > Cameras.Length)
        {
            Debug.Log("Invalid Camera Index! Reset to 0");
            cameraIndex = 0;
        }
        activeCamera = cameraIndex;
        foreach (var camera in Cameras)
        {
            camera.Priority = inactivePriority;
        }
        Cameras[cameraIndex].Priority = activePriority;
    }
}

