using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour {

    private void Awake()
    {
        Transform checkpointTransform = transform.Find("CheckPoints");
        foreach (Transform checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
        }
    }
    public void GoThroughCheckPoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.transform.name);
    }

}
