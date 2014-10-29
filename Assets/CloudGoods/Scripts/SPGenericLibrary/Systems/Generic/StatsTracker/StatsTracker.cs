using UnityEngine;
using System;

public abstract class StatsTracker
{
    static StatsTracker statTracker;

    public static void Init(StatsTracker _statTracker)
    {
        statTracker = _statTracker;
    }

    public static void PostAction(string statType, int value)
    {
        statTracker.PostActionHandler(statType, value);
    }

    public abstract void PostActionHandler(string statType, int value);
}
