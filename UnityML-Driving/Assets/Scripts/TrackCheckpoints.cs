using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour {

    private List<CheckpointSingle> checkpointSinglesList;
    private int nextIndex;
    private void Awake()
    {
        Transform checkpointTransform = transform.Find("CheckPoints");
        checkpointSinglesList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSinglesList.Add(checkpointSingle);
        }
    }
    public void GoThroughCheckPoint(CheckpointSingle checkpointSingle)
    {
        if(checkpointSinglesList.IndexOf(checkpointSingle)==nextIndex)
        {
            Debug.Log("correct");
            nextIndex = (nextIndex + 1) % checkpointSinglesList.Count;
        }
        else
        {
            Debug.Log("wrong");
        }
    }

}
