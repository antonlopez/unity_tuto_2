﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlaceObjectOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid;
    private bool isObjectPlaced;

    public GameObject positionIndicator;
    public GameObject prefabToPlace;
    public Camera aRCamera;
    public Canvas canvas;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }


    // Update is called once per frame
    void Update()
    {

        if (!isObjectPlaced) {
            UpdatePlacementPose();

            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }
       

    }

    private void UpdatePlacementPose() {
        var screenCenter = aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.All);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            positionIndicator.SetActive(true);
            positionIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            positionIndicator.SetActive(false);

        }
    }

    private void PlaceObject()
    {
       var spawnedObject = Instantiate(prefabToPlace, placementPose.position, placementPose.rotation);
        RotateManager.GetInstance().SetPyramid(spawnedObject);
        isObjectPlaced = true;
        positionIndicator.SetActive(false);
        canvas.gameObject.SetActive(true);


    }

}
