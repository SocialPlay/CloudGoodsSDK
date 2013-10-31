using UnityEngine;
using System;

public class KongregateStatsTracker : StatsTracker
{
    public override void PostActionHandler(string statType, int value)
    {
        Application.ExternalCall("UnityStatsTracker", statType, value);
    }
}
