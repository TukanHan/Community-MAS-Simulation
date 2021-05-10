using Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentQueueController : SingletonBehaviour<AgentQueueController>
{
    public event EventHandler<int> NextRoundStarted;

    public int seed;
    public uint agentsCount;
    public GameObject agentPrefab;
    public List<Agent> Agents { get; set; } = new List<Agent>();

    private float step = 1f;
    private float elapsed = 0f;
    private bool isTimeElapsing = true;

    public int Round { get; private set; }

    private void Start()
    {
        System.Random random = new System.Random(seed);
        for (int i = 0; i < agentsCount; i++)
        {
            InstantineAgent(random, i + 1);
        }
    }

    void Update()
    {
        if(isTimeElapsing)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= step)
                NextRound();
        }      
    }

    public void NextRound(int stepsCount = 1)
    {
        for(int i=0; i<stepsCount; ++i)
        {
            Round++;
            UpdateAgents();
        }

        elapsed = 0;
        OnNextRoundStarted();
    }

    public void Play()
    {
        isTimeElapsing = true;
    }

    public void Pause()
    {
        isTimeElapsing = false;
    }

    void UpdateAgents()
    {
        foreach (Agent agent in GetAliveAgents())
        {
            agent.UpdateBehaviour();
        }
    }

    void InstantineAgent(System.Random random, int number)
    {
        GameObject go = Instantiate(agentPrefab, transform);
        Agent agent = go.GetComponent<Agent>();
        agent.Initialize(random, $"Agent #{number}");
        Agents.Add(agent);
    }

    protected void OnNextRoundStarted()
    {
        NextRoundStarted?.Invoke(this, Round);
    }

    public IEnumerable<Agent> GetAliveAgents()
    {
        return Agents.Where(agent => agent.IsAlive());
    }

    public int GetAliveAgentsCount()
    {
        return Agents.Where(agent => agent.IsAlive()).Count();
    }
}
