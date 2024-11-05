﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class GoalManager : Component
{
    public Goal CurrentGoal { get; private set; }

    public List<Goal> PausedGoals { get; private set; } = new List<Goal>();

    public List<Goal> Goals { get; private set; } = new List<Goal>();

    public GoalManager() : base(false)
    {
    }

    public override void Update()
    {
        if (CurrentGoal == null && Goals.Count > 0)
        {
            SetGoal(Goals.FirstOrDefault());
        }

        if (CurrentGoal == null && PausedGoals.Count > 0)
        {
            foreach (Goal goal in PausedGoals)
            {
                ResumeGoal(goal);
            }
        }

        CurrentGoal?.Update();
    }

    public void AddGoal(Goal goal)
    {
        if (goal == null) return;

        Goals.Add(goal);
        goal.Assign(GameObject as Character);

        goal.OnFinish(() =>
        {
            if (goal == CurrentGoal)
            {
                CurrentGoal = null;
            }

            RemoveGoal(goal);
        });

        Goals = Goals.OrderBy(g => g.Priority).ToList();
    }

    public void SetGoal(Goal goal)
    {
        if (goal == null) return;

        CurrentGoal = goal;

        RemoveGoal(goal);

        CurrentGoal.Start();
    }

    public void RemoveGoal(Goal goal)
    {
        Goals.Remove(goal);

        Goals = Goals.OrderBy(g => g.Priority).ToList();
    }

    public void PauseCurrentGoal()
    {
        if (CurrentGoal == null) return;

        PausedGoals.Add(CurrentGoal);
        CurrentGoal = null;
    }

    public void PauseGoal(Goal goal)
    {
        if (goal == null) return;

        PausedGoals.Add(goal);
        RemoveGoal(goal);
    }

    public void ResumeGoal(Goal goal)
    {
        if (goal == null) return;

        AddGoal(goal);
        PausedGoals.Remove(goal);
    }
}
