using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeObserver : MonoBehaviour {

    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.layerChangeObservers += SomeHandlingFunction;
	}
	
	// Update is called once per frame
	void SomeHandlingFunction () {
        print("Yahoo, called from elsewhere");
	}
}
