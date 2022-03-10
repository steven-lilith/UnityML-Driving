using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DrivingAgent : Agent
{
    
    private PrometeoCarController car;
    [SerializeField]
    private Transform InitialTransform;
    private Transform Target;
    [SerializeField]
    private TrackCheckpoints checkpoints;
    private void Awake()
    {
        car = GetComponent<PrometeoCarController>();
    }
    public void Start()
    {
        //InitialTransform = transform;
        //Target = checkpoints.checkpointSinglesList[checkpoints.nextIndex].transform;
        checkpoints.onCarCorrecCheckPoint += CheckPoint_Correct;
        checkpoints.onCarWrongCheckPointl += CheckPoint_Wrong;

    }
    public override void OnEpisodeBegin()
    {
        transform.position = InitialTransform.position;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //float direction = Vector3.Dot(transform.forward, new Vector3(15, 11.83f, 91.57f));
        //sensor.AddObservation(transform.position);
        for(int i =0;i<checkpoints.checkpointSinglesList.Count;++i)
        {
            sensor.AddObservation(checkpoints.checkpointSinglesList[i].transform);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //float forward = 0.0f;
        //float turn = 0.0f;
        /*switch (actions.DiscreteActions[0])
        {
            case 0:
                forward = 0.0f;
                break;
            case 1:
                forward = 1.0f;
                break;
            case 2:
                forward = -1.0f;
                break;
            default:
                break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0:
                turn = 0.0f;
                break;
            case 1:
                turn = 1.0f;
                break;
            case 2:
                turn = -1.0f;
                break;
            default:
                break;
        }*/
        float forward = actions.ContinuousActions[0];
        float turn = actions.ContinuousActions[1];
        car.Accel(forward);
        car.turn(turn);
        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        int forward = 0;
        int turn = 0;
        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turn = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            turn = 1;
        }
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = forward;
        continuousActions[1] = turn;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KillZone"))
        {
            SetReward(0f);
            //Destroy(gameObject);
            EndEpisode();
        }
    }
    private void CheckPoint_Correct()
    {
        AddReward(10.0f);
    }
    private void CheckPoint_Wrong()
    {
        AddReward(-20.0f);
    }
}
