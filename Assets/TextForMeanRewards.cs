using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextForMeanRewards : MonoBehaviour
{
    [SerializeField] private Text text;
    MoveToGoalAgents moveToGoalAgents;
    private static float totalPositiveReward = 0f;
    private static int agentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveToGoalAgents = GetComponent<MoveToGoalAgents>();
        text.text = "Mean Rewards: 0.0";
    }

    // Update is called once per frame
    public void UpdateMeanRewards(float positiveReward)
    {
        agentCount++;
        totalPositiveReward += positiveReward;

        float meanPositiveReward = totalPositiveReward / agentCount;

        text.text = $"Mean Rewards:"+ meanPositiveReward;
    }
}
