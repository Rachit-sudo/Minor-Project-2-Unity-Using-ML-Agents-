using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MoveToGoalAgents : Agent
{
    // Start is called before the first frame update
    //machine learning learns through Reinforcement Learning
    //Based Upon Rewards
    //Way we collect observation by overriding
    //we need a penalty so we add some colliders on the edges
    [SerializeField] private Transform targetTransform;
    //some part for visualization...
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    public float speed = 1f;
    public float Reward = 1f;
    [SerializeField] private float positiveMeanReward = 0f;
    [SerializeField] private float negativeMeanReward = 0f;
    [SerializeField] private int positiveCollsionCount = 0;
    [SerializeField] private int negativeCollsionCount = 0;



    public override void OnEpisodeBegin()
    {
        //reset back on the initial state
        //agent -> X(-5.561,-0.376)
        //agent -> Z(-2.73,2.625)
        //agent.default(-0.376,1.079,-0.68)
        transform.localPosition = new Vector3(Random.Range(-2.7805f,-0.188f),1, Random.Range(-1.365f, 1.3125f)); //local position because that would be the same place where it got a reward++ or reward--.
        //goal -> Z(-2.289,3.188)
        //goal -> X(-7.39,4.39)
        targetTransform.localPosition = new Vector3(Random.Range(-3.3f, 1.794f),1.4f,Random.Range(-1.1445f, 1.4595f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); //the sensor is collecting information on position of the player
        sensor.AddObservation(targetTransform.localPosition); //sensor is collecting the information on the position/coordinates on which the player has moved 
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //we shall display the Discrete Actions performed by the Agent... as we have attached the script
        //avoid padding observations made by the agent
        //we are going to add a request decision
        //Defining two movements in X and Z direction
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //since ML just works on the numbers
        transform.position += new Vector3(moveX, 0, moveZ) * speed *Time.deltaTime ; //Used for basic Locomotion.

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+Reward);
            floorMeshRenderer.material = winMaterial;
            positiveCollsionCount++;
            positiveMeanReward = Reward/positiveCollsionCount;
            // Pass the mean reward value as an argument
            Debug.Log(positiveMeanReward);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-Reward);
            floorMeshRenderer.material = loseMaterial;
            negativeCollsionCount++;
            negativeMeanReward = -Reward/negativeCollsionCount;
            // Pass the negative mean reward value as an argument
            Debug.Log(negativeMeanReward);
            EndEpisode();
        }
    }
  

}
