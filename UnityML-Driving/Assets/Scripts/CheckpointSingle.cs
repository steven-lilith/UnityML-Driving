using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour 
{

    private TrackCheckpoints trackCheckpoints;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PrometeoCarController>(out PrometeoCarController carController))
        {
            trackCheckpoints.GoThroughCheckPoint(this);
        }
    }
    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
