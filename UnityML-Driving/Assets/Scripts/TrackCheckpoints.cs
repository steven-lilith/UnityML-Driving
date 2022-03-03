using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour {
    [SerializeField]
    public List<CheckpointSingle> checkpointSinglesList;
    [SerializeField]
    private List<GameObject> track;
    public int nextIndex;
    private void Awake()
    {
        /*Transform checkpointTransform = transform.Find("CheckPoints");
        checkpointSinglesList = new List<CheckpointSingle>();
        //check the check points within the list in correct order
        foreach (Transform checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSinglesList.Add(checkpointSingle);
        }*/
        nextIndex = 0;
        for(int i =0;i<track.Count;++i)
        {
            CheckpointSingle checkpoint = track[i].GetComponentInChildren<CheckpointSingle>();
            checkpointSinglesList.Add(checkpoint);
        }
        
    }
    public void GoThroughCheckPoint(CheckpointSingle checkpointSingle)
    {
        //check if the player has skipped any check points 
        if(checkpointSinglesList.IndexOf(checkpointSingle)==nextIndex)
        {
            Debug.Log("correct");
            nextIndex = (nextIndex + 1) % checkpointSinglesList.Count; //make sure all the check points are available for the second lap
        }
        //if yes, then addreward(-1f)
        else
        {
            Debug.Log("wrong");
        }
    }

}
