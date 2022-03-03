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
    [SerializeField]
    private TrackCheckpoints trackCheckpoints;
    [SerializeField]
    private Transform Target;
    private void Awake()
    {
        car = GetComponent<PrometeoCarController>();
    }
    public void Start()
    {
       //InitialTransform = transform;
        
    }
    public override void OnEpisodeBegin()
    {
        transform.position = InitialTransform.position;
    }

    
    public override void CollectObservations(VectorSensor sensor)
    {
        //float direction = Vector3.Dot(transform.forward, new Vector3(15, 11.83f, 91.57f));
        //sensor.AddObservation(transform.position);
        sensor.AddObservation(Target.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forward = 0.0f;
        float turn = 0.0f;
        switch (actions.DiscreteActions[0])
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
        }
        car.Accel(forward);
        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        int forward = 0;
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
        discrettActions[1] = turn;

    }
    private void OnCollisionEnter(Collision collision) //first collsion first happen
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            Debug.Log("collided");
            AddReward(-1f);
        }
    }
    private void OnCollisionStay(Collision collision) //trigger if collsion keeps happening
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            Debug.Log("scratched");
            AddReward(-0.5f);
        }
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
    
}
