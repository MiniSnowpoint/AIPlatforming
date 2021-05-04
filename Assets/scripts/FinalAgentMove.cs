using UnityEngine;
using System.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class FinalAgentMove : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform targetTransform2;
    [SerializeField] private Transform targetTransform3;
    [SerializeField] private Transform targetTransform4;
    [SerializeField] private Transform targetTransform5;
    [SerializeField] private Transform targetTransform6;
    [SerializeField] private Transform targetTransform7;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        targetTransform2.localPosition = new Vector3(50, 0, 0);
        targetTransform3.localPosition = new Vector3(151, 0, 0);
        targetTransform4.localPosition = new Vector3(254, 9, 0);
        targetTransform5.localPosition = new Vector3(384, 0, 0);
        targetTransform6.localPosition = new Vector3(478, 0, 88);
        targetTransform7.localPosition = new Vector3(435, 0, 220);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(targetTransform2.localPosition);
        sensor.AddObservation(targetTransform3.localPosition);
        sensor.AddObservation(targetTransform4.localPosition);
        sensor.AddObservation(targetTransform5.localPosition);
        sensor.AddObservation(targetTransform6.localPosition);
        sensor.AddObservation(targetTransform7.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        AddReward(-0.0005f);
        float moveX = actionBuffers.ContinuousActions[0];
        float moveZ = actionBuffers.ContinuousActions[1];
        float moveJ = actionBuffers.ContinuousActions[2];

        float moveSpeed = 23f;
        transform.localPosition += new Vector3((moveX + (moveJ * 2)), (moveJ * 4), moveZ) * Time.deltaTime * moveSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        continuousActions[2] = Input.GetAxisRaw("Jump");
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
        if (other.TryGetComponent<Check3>(out Check3 check3))
        {
            SetReward(+80.0f);
            targetTransform4.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Check4>(out Check4 check4))
        {
            SetReward(+80.0f);
            targetTransform5.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Check5>(out Check5 check5))
        {
            SetReward(+80.0f);
            targetTransform6.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Check6>(out Check6 check6))
        {
            SetReward(+80.0f);
            targetTransform7.localPosition = new Vector3(0, -25, 0);
        }
        if (other.TryGetComponent<Wave>(out Wave wave))
        {
            SetReward(-20.0f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-40.0f);
            EndEpisode();
        }
    }
}
