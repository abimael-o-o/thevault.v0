using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public delegate void GameEvent();
    private static Dictionary<string, GameEvent> eventDictionary = new Dictionary<string, GameEvent>();

    public static void StartListening(string eventName, GameEvent listener)
    {
        if (eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
            thisEvent += listener;
        else
            eventDictionary[eventName] = listener;
    }

    public static void StopListening(string eventName, GameEvent listener)
    {
        if (eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
            thisEvent -= listener;
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out GameEvent thisEvent))
            thisEvent.Invoke();
    }
}
