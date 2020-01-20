using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    public Text TimerText;
    public Slider TimerSlider;

    public float CooldownTimeInSeconds;

    private CooldownTimer _cooldownTimer;

    private void Awake()
    {
        _cooldownTimer = new CooldownTimer(CooldownTimeInSeconds);
        
        // Register handler that will trigger when timer is complete
        _cooldownTimer.TimerCompleteEvent += OnTimerComplete;
    }
    
    private void Update () {
        // Update cooldown timer with Time.deltaTime 
        _cooldownTimer.Update(Time.deltaTime);
        if (_cooldownTimer.IsActive)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        TimerText.text = "Cooldown: " + _cooldownTimer.TimeRemaining;
        TimerSlider.value = _cooldownTimer.PercentRemaining;
    }

    private void OnTimerComplete()
    {
        TimerText.text = "Cooldown Completed!";
    }

    public void StartTimer()
    {
        _cooldownTimer.Start();
    }
}
