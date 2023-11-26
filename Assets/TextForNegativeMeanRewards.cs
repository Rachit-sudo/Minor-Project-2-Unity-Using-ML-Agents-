using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextForNegativeMeanRewards : MonoBehaviour
{
    [SerializeField] private Text text;
    MoveToGoalAgents moveToGoalAgents;
    private static float totalNegativeReward = 0f;
    private static int agentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveToGoalAgents = GetComponent<MoveToGoalAgents>();
        text.text = "Negative Mean Rewards: 0.0";
    }

    // Update is called once per frame
    public void UpdateNegativeMeanRewards(float negativeReward)
    {
        agentCount++;
        totalNegativeReward += negativeReward;

        float meanNegativeReward = totalNegativeReward / agentCount;

        text.text = "Negative Mean Rewards:"+meanNegativeReward;
    }
}
