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
    [SerializeField]
    private int maxCollisionCount;
    private int currentCollisionCount;
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
        transform.forward = InitialTransform.forward;
        checkpoints.nextIndex = 0;
        currentCollisionCount = 0;
    }

    
    public override void CollectObservations(VectorSensor sensor)
    {
        //float direction = Vector3.Dot(transform.forward, new Vector3(15, 11.83f, 91.57f));
        //sensor.AddObservation(transform.position);
        //for(int i =0;i<checkpoints.checkpointSinglesList.Count;++i)
        //{
        //    sensor.AddObservation(checkpoints.checkpointSinglesList[i].transform);
        //}


        Vector3 checkPointForward = checkpoints.nextCheckPoint.transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkPointForward);
        sensor.AddObservation(directionDot);

        //AddReward(-0.01f);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;

        car.Accel(input[1]);
        car.turn(input[0]);
        /*AddReward(0.0001f * gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if(currentCollisionCount==maxCollisionCount)
        {
            EndEpisode();
        }*/
        Debug.Log(input[1]);
        //Debug.Log(turn);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        /*int forward = 0;
        int turn = 0;
        if(Input.GetKey(KeyCode.W))
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
        ActionSegment<int> discrettActions = actionsOut.DiscreteActions;

        discrettActions[0] = forward;
        discrettActions[1] = turn;*/
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxisRaw("Horizontal");
        action[1] = Input.GetAxisRaw("Vertical");

    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KillZone"))
        {
            SetReward(0f);
            //Destroy(gameObject);
            EndEpisode();
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("wall"))
        {
            AddReward(-10.0f);
            currentCollisionCount++;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-10.0f);
            currentCollisionCount++;
        }
    }
    private void CheckPoint_Correct()
    {
        AddReward(100.0f);
    }
    private void CheckPoint_Wrong()
    {
        AddReward(-200.0f);
        EndEpisode();
    }
}
