
/*
 * Utility timer. Has the option to be recurring.
 * Update needs to be called with the deltaTime.
 * TimerCompleteEvent is emitted on completion.
 */
public class CooldownTimer
{
    public float TimeRemaining { get; private set; }
    public float TotalTime { get; private set; }
    public bool IsRecurring { get; }
    public bool IsActive { get; private set; }
    public int TimesCounted { get; private set; }

    public float TimeElapsed => TotalTime - TimeRemaining;
    public float PercentElapsed => TimeElapsed / TotalTime;
    public float PercentRemaining => TimeRemaining / TotalTime;
    public bool IsCompleted => TimeRemaining <= 0;

    public delegate void TimerCompleteHandler();

    /// <summary>
    /// Emits event when timer is completed
    /// </summary>
    public event TimerCompleteHandler TimerCompleteEvent;

    /// <summary>
    /// Create a new CooldownTimer
    /// Must call Start() to begin timer
    /// </summary>
    /// <param name="time">Timer length (seconds)</param>
    /// <param name="recurring">Is this timer recurring</param>
    public CooldownTimer(float time, bool recurring = false)
    {
        TotalTime = time;
        IsRecurring = recurring;
        TimeRemaining = TotalTime;
    }

    /// <summary>
    /// Start timer with existing time
    /// </summary>
    public void Start()
    {
        if (IsActive) { TimesCounted++; }
        TimeRemaining = TotalTime;
        IsActive = true;
        if (TimeRemaining <= 0)
        {
            TimerCompleteEvent?.Invoke();
        }
    }

    /// <summary>
    /// Start timer with new time
    /// </summary>
    public void Start(float time)
    {
        TotalTime = time;
        Start();
    }

    public virtual void Update(float timeDelta)
    {
        if (TimeRemaining > 0 && IsActive)
        {
            TimeRemaining -= timeDelta;
            if (TimeRemaining <= 0)
            {
                if (IsRecurring)
                {
                    TimeRemaining = TotalTime;
                }
                else
                {
                    IsActive = false;
                    TimeRemaining = 0;
                }

                TimerCompleteEvent?.Invoke();
                TimesCounted++;
            }
        }
    }

    public void Invoke()
    {
        TimerCompleteEvent?.Invoke();
    }

    public void Pause()
    {
        IsActive = false;
    }

    /// <summary>
    /// Add additional time to timer
    /// </summary>
    public void AddTime(float time)
    {
        TimeRemaining += time;
        TotalTime += time;
    }
}