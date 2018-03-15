using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

	public CinemachineVirtualCamera[] Cameras;

	int cameraArraySize;
	int activeCamera;

	// Use this for initialization
	void Start ()
	{
	cameraArraySize = Cameras.Length;
	activeCamera = 0;
	Cameras[0].Priority = 10;
	for (int i = 1; i < Cameras.Length; i++)
		{
			Cameras[i].Priority = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            DecreaseCameraIndex();
        }
    }

    public void IncreaseCameraIndex()
    {
        if (activeCamera < Cameras.Length - 1)
        {
            Cameras[activeCamera + 1].Priority = 10;
            Cameras[activeCamera].Priority = 0;
            activeCamera++;
        }
        else
        {
            Cameras[0].Priority	= 10;
			Cameras[Cameras.Length - 1].Priority = 0;
            activeCamera = 0;
        }
    }

	    public void DecreaseCameraIndex()
    {
        if (activeCamera > 0)
        {
            Cameras[activeCamera - 1].Priority = 10;
            Cameras[activeCamera].Priority = 0;
            activeCamera--;
        }
        else
        {
             Cameras[Cameras.Length - 1].Priority = 10;
			Cameras[0].Priority = 0;
            activeCamera = Cameras.Length - 1;
        }
    }
}
