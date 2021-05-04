using UnityEngine;
using System.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class AgentMove : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform targetTransform2;
    [SerializeField] private Transform targetTransform3;
    //[SerializeField] private GameObject target3;
    //[SerializeField] private GameObject target2;

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(Random.Range(0f, +50f), 0, Random.Range(-30f, 30f));
        transform.localPosition = new Vector3(0, 0, 0);
        //targetTransform.localPosition = new Vector3(Random.Range(400f, +480f), -8, Random.Range(-30f, 30f));
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        //target2.SetActive(true);
        //target3.SetActive(true);
        targetTransform2.localPosition = new Vector3(150, 0, -30);
        targetTransform3.localPosition = new Vector3(300, 0, 23);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(targetTransform2.localPosition);
        sensor.AddObservation(targetTransform3.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        
        float moveX = actionBuffers.ContinuousActions[0];
        float moveZ = actionBuffers.ContinuousActions[1];

        float moveSpeed = 20f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+100.0f);
            EndEpisode();
        }
        if (other.TryGetComponent<Check1>(out Check1 check1))
        {
            SetReward(+80.0f);
            targetTransform2.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Check2>(out Check2 check2))
        {
            SetReward(+80.0f);
            targetTransform3.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-40.0f);
            EndEpisode();
        }
    }
}
