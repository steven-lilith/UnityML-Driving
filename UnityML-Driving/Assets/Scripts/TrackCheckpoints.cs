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

    public System.Action onCarCorrecCheckPoint;
    public System.Action onCarWrongCheckPointl;
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
        nextIndex = -1;
        for(int i =0;i<track.Count;++i)
        {
            CheckpointSingle[] checkpoint = track[i].GetComponentsInChildren<CheckpointSingle>();
            for (int u = 0; u < checkpoint.Length; ++u)
            {
                checkpoint[u].SetTrackCheckpoints(this);
                checkpointSinglesList.Add(checkpoint[u]);
            }
        }
        
    }
    public void GoThroughCheckPoint(CheckpointSingle checkpointSingle)
    {
        //check if the player has skipped any check points 
        if(nextIndex == -1)
        {
            onCarCorrecCheckPoint?.Invoke();
            Debug.Log("correct");
            nextIndex = checkpointSinglesList.IndexOf(checkpointSingle) + 1;
        }
        else if(checkpointSinglesList.IndexOf(checkpointSingle)==nextIndex)
        {
            onCarCorrecCheckPoint?.Invoke();
            Debug.Log("correct");
            nextIndex = (nextIndex + 1) % checkpointSinglesList.Count; //make sure all the check points are available for the second lap
        }
        //if yes, then addreward(-1f)
        else
        {
            onCarWrongCheckPointl?.Invoke();
            Debug.Log("wrong");
        }
    }

}
