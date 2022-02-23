using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DrivingAgent : Agent
{
    private Transform InitialTransform;
    public void Start()
    {
        InitialTransform = transform;
    }
    public override void OnEpisodeBegin()
    {
        transform.position = InitialTransform.position;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float holdWTime = actions.ContinuousActions[0];
        float holdDTime = actions.ContinuousActions[1];
        float holdATime = actions.ContinuousActions[2];
        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Vertical");
        continuousActions[1] = Input.GetAxisRaw("Horizontal");
        continuousActions[2] = Input.GetAxisRaw("Horizontal");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KillZone"))
        {
            SetReward(0f);
            EndEpisode();
        }
    }
}
