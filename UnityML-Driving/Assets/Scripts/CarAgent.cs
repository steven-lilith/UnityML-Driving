using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using static CarController;

public class CarAgent : Agent
{
    private Vector3 originalPosition;

    private BehaviorParameters behaviorParameters;

    private CarController carController;

    private Rigidbody carControllerRigidBody;

    private  ParkingSpot carSpots;

    public void Initialize()
    {
        originalPosition = transform.localPosition;
        behaviorParameters = GetComponent<BehaviorParameters>();
        carController = GetComponent<CarController>();
        carControllerRigidBody = carController.GetComponent<Rigidbody>();
        carSpots = transform.parent.GetComponentInChildren<ParkingSpot>();

        ResetParkingLotArea();
    }

    public void OnEpisodeBegin()
    {
        ResetParkingLotArea();
    }

    private void ResetParkingLotArea()
    {
        // important to set car to automonous during default behavior
        //carController.IsAutonomous = behaviorParameters.BehaviorType == BehaviorType.Default;
        //transform.localPosition = originalPosition;
        //transform.localRotation = Quaternion.identity;
        //carControllerRigidBody.velocity = Vector3.zero;
        //carControllerRigidBody.angularVelocity = Vector3.zero;

    }

    void Update()
    {
        if (transform.localPosition.y <= 0)
        {
            TakeAwayPoints();
        }
    }

    public void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.rotation);

        sensor.AddObservation(carSpots.CarGoal.transform.position);
        sensor.AddObservation(carSpots.CarGoal.transform.rotation);

        sensor.AddObservation(carControllerRigidBody.velocity);
    }

    public void OnActionReceived(float[] vectorAction)
    {
        var direction = Mathf.FloorToInt(vectorAction[0]);

        switch (direction)
        {
            case 0: // idle
                carController.CurrentDirection = Direction.Idle;
                break;
            case 1: // forward
                carController.CurrentDirection = Direction.MoveForward;
                break;
            case 2: // backward
                carController.CurrentDirection = Direction.MoveBackward;
                break;
            case 3: // turn left
                carController.CurrentDirection = Direction.TurnLeft;
                break;
            case 4: // turn right
                carController.CurrentDirection = Direction.TurnRight;
                break;
        }

        AddReward(-1f / MaxStep);
    }

    public void GivePoints(float amount = 1.0f, bool isFinal = false)
    {
        AddReward(amount);

        if (isFinal)
        {
            //StartCoroutine(SwapGroundMaterial(successMaterial, 0.5f));

            EndEpisode();
        }
    }

    public void TakeAwayPoints()
    {
        //StartCoroutine(SwapGroundMaterial(failureMaterial, 0.5f));

        AddReward(-0.01f);

        EndEpisode();
    }
}
