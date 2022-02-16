using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;


public class CarAgent : Agent
{
    private CarController carController;
    [SerializeField] private Transform spawnPosition;
    public GameObject checkPoints;
    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            AddReward(+1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            AddReward(-1f);
        }
    }
    public override void OnEpisodeBegin()
    {
        transform.position = spawnPosition.position + new Vector3(Random.Range(-5f, +5f), 0, Random.Range(-5f, +5f));
        base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }
}
