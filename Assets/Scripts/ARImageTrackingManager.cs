using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARImageTrackingManager : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager imageManager;

    [SerializeField]
    private ImgPrefabPair[] referenceImgPrefabPairs;

    private Dictionary<string, GameObject> trackingObjectDic = new Dictionary<string, GameObject>();

    private void Awake ()
    {
        imageManager.trackedImagesChanged += OnImageTracked;

        foreach (ImgPrefabPair pairs in referenceImgPrefabPairs)
        {
            GameObject trackingObj = GameObject.Instantiate(pairs.prefab);
            trackingObjectDic.Add(pairs.image.name, trackingObj);
            trackingObj.SetActive(false);
        }
    }

    private void OnImageTracked (ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var img in eventArgs.updated)
        {
            TrackedImgUpdate(img);
        }
    }

    private void TrackedImgUpdate (ARTrackedImage trackedImg)
    {
        if (trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            trackingObjectDic[trackedImg.referenceImage.name].SetActive(true);
            trackingObjectDic[trackedImg.referenceImage.name].transform.position = trackedImg.transform.position;
            trackingObjectDic[trackedImg.referenceImage.name].transform.rotation = trackedImg.transform.rotation;
        }
        if (trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {
            trackingObjectDic[trackedImg.referenceImage.name].SetActive(false);
        }
    }

    [Serializable]
    public struct ImgPrefabPair
    {
        public Sprite image;
        public GameObject prefab;
    }
}