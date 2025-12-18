using UnityEngine;
using UnityEngine.UI;

public class QTEProgressBar : MonoBehaviour
{
    [Header("References")]
    public Slider slider;     // Assign in Inspector
    public MashE mashE;       // Assign in Inspector

    void Start()
    {
        if (slider) slider.value = 0f;
    }

    void Update()
    {
        if (mashE == null || slider == null) return;

        // Always show current progress (count 0–10)
        slider.value = Mathf.Clamp(mashE.CurrentCount, 0, mashE.pressesRequired);
    }

    // Optional: reset bar after success/fail
    public void ResetBar()
    {
        if (slider) slider.value = 0f;
    }
}
