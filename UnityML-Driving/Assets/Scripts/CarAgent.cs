using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //float forwardAmount = 0f;
        //float turnAmount = 0f;
        //switch(actions.DiscreteActions[0])
        //{
        //    case 0:forwardAmount = 0f;
        //        break;
        //    case 1:
        //        forwardAmount = +1f;
        //        break;
        //    case 2:
        //        forwardAmount = -1f;
        //        break;
        //}
        //switch (actions.DiscreteActions[1])
        //{
        //    case 0:
        //        turnAmount = 0f;
        //        break;
        //    case 1:
        //        turnAmount = +1f;
        //        break;
        //    case 2:
        //        turnAmount = -1f;
        //        break;
        //}

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        SetReward(1f);
        EndEpisode();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
        continuousAction[1] = Input.GetAxisRaw("Vertical");

    }
    private void OnTriggerEnter(Collider2D collision)
    {
        if (collision.TryGetComponent<CheckpointSingle>(out CheckpointSingle checkpoint))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (collision.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
            
        }
    }
    //private CarController carController;
    //[SerializeField] private Transform spawnPosition;
    //public GameObject checkPoints;
    //private void Awake()
    //{
    //    carController = GetComponent<CarController>();
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject.CompareTag("Player"))
    //    {
    //        AddReward(+1f);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag =="Player")
    //    {
    //        AddReward(-1f);
    //    }
    //}
    //public override void OnEpisodeBegin()
    //{
    //    transform.position = spawnPosition.position + new Vector3(Random.Range(-5f, +5f), 0, Random.Range(-5f, +5f));
    //    base.OnEpisodeBegin();
    //}
    //public override void CollectObservations(VectorSensor sensor)
    //{
    //    base.CollectObservations(sensor);
    //}
}
