using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class TrackCheckpoints : Agent {

    private List<CheckpointSingle> checkpointSinglesList;
    private int nextIndex;
    public event EventHandler onEnterCorrectCheckPoint;
    public event EventHandler onEnterWrongCheckPoint;

    private void Awake()
    {
        Transform checkpointTransform = transform.Find("CheckPoints");
        checkpointSinglesList = new List<CheckpointSingle>();
        //check the check points within the list in correct order
        foreach (Transform checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSinglesList.Add(checkpointSingle);
        }
    }
    public void GoThroughCheckPoint(CheckpointSingle checkpointSingle)
    {
        //check if the player has skipped any check points 
        if(checkpointSinglesList.IndexOf(checkpointSingle)==nextIndex)
        {
            Debug.Log("correct");
            AddReward(+1f);
            nextIndex = (nextIndex + 1) % checkpointSinglesList.Count; //make sure all the check points are available for the second lap
            onEnterCorrectCheckPoint?.Invoke(this, EventArgs.Empty);
        }
        //if yes, then addreward(-1f)
        else
        {
            AddReward(-1f);
            Debug.Log("wrong");
            onEnterWrongCheckPoint?.Invoke(this, EventArgs.Empty);
        }
    }

}
