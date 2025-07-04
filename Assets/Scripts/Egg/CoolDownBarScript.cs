using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBarScript : MonoBehaviour
{
    public float SecToCoolDown = 0.2f; // Cool down time in seconds
    private float LastTriggered = 0f; // Last time the bar was triggered
    private bool Active = false; // Is the bar currently active
    private float InitBarWidth = 0f; // Initial width of the bar
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        InitBarWidth = rt.sizeDelta.x; // Store the initial width of the bar
        LastTriggered = Time.time;
        Vector2 s = GetComponent<RectTransform>().sizeDelta;
        s.x = 0f; // Update the width of the bar based on
        GetComponent<RectTransform>().sizeDelta = s; // Set the new size
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            UpdateBar();
        }
    }

    private void UpdateBar()
    {
        float sec = SecondsTillNext();
        float percent = sec / SecToCoolDown;
        if (sec < 0)
        {
            percent = 0f; // Ensure we don't go below 0
            Active = false; // Reset the active state if cooldown is complete
        }
        Vector2 s = GetComponent<RectTransform>().sizeDelta;
        s.x = InitBarWidth * percent; // Update the width of the bar based on
        GetComponent<RectTransform>().sizeDelta = s; // Set the new size
    }
    private float SecondsTillNext()
    {
        float secLeft = -1;
        if (Active)
        {
            float sinceLast = Time.time - LastTriggered;
            return SecToCoolDown - sinceLast;
        }
        return secLeft;
    }
    public bool TriggerCoolDown()
    {
        // Debug.Log("TriggerCoolDown called. Active: " + Active + ", LastTriggered: " + LastTriggered + ", Time.time: " + Time.time);
        bool canTrigger = !Active;
        if (canTrigger)
        {
            Active = true;
            LastTriggered = Time.time;
            UpdateBar();
        }
        return canTrigger; // Cool down not triggered, still cooling down
    }
    public bool ReadyForNext()
    {
        return (!Active);
    }
}
