using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class SimpleQTE : MonoBehaviour
{
    [Header("Timeline (optional)")]
    public PlayableDirector director;   // Assign your Timeline if you use one

    [Header("Flow Settings")]
    public bool pauseOnStart = true;
    public bool resumeOnSuccess = true;
    public bool resumeOnFail = false;

    [Tooltip("Wait this many seconds after Success before resuming the Timeline (use 0 for immediate).")]
    public float resumeDelayOnSuccess = 0f;

    [Tooltip("Wait this many seconds after Fail before resuming the Timeline (use 0 for immediate).")]
    public float resumeDelayOnFail = 0f;

    [Header("QTE Events")]
    public UnityEvent onQTEStart;       // Runs once when the QTE begins
    public UnityEvent onDuringQTE;      // Runs every frame while active
    public UnityEvent onPlayerInput;    // Runs when PlayerInput() is called
    public UnityEvent onQTESuccess;     // Runs immediately when Succeed() is called
    public UnityEvent onQTEFail;        // Runs immediately when Fail() is called
    public UnityEvent onQTEFinished;    // Runs after success/fail delay + Timeline resume

    public bool IsActive { get; private set; }

    // --- Public API ---

    /// <summary>Start the QTE (usually from a Timeline Signal).</summary>
    public void StartQTE()
    {
        if (IsActive) return;
        IsActive = true;

        if (pauseOnStart && director && director.state == PlayState.Playing)
            director.Pause();

        onQTEStart?.Invoke();
    }

    /// <summary>Call this when the player performs an input action (e.g., button press).</summary>
    public void PlayerInput()
    {
        if (!IsActive) return;
        onPlayerInput?.Invoke();
    }

    /// <summary>Mark the QTE as successful and optionally resume the Timeline after a delay.</summary>
    public void Succeed()
    {
        if (!IsActive) return;
        IsActive = false;

        onQTESuccess?.Invoke();
        StartCoroutine(FinishRoutine(success: true, resumeDelayOnSuccess, resumeOnSuccess));
    }

    /// <summary>Mark the QTE as failed and optionally resume the Timeline after a delay.</summary>
    public void Fail()
    {
        if (!IsActive) return;
        IsActive = false;

        onQTEFail?.Invoke();
        StartCoroutine(FinishRoutine(success: false, resumeDelayOnFail, resumeOnFail));
    }

    /// <summary>Cancel without triggering success/fail (optional utility).</summary>
    public void Cancel()
    {
        if (!IsActive) return;
        IsActive = false;
        StartCoroutine(FinishRoutine(success: false, delay: 0f, doResume: false));
    }

    // --- Internals ---

    private void Update()
    {
        if (IsActive)
            onDuringQTE?.Invoke();
    }

    private IEnumerator FinishRoutine(bool success, float delay, bool doResume)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        if (doResume && director)
            director.Resume();

        onQTEFinished?.Invoke();
    }
}
