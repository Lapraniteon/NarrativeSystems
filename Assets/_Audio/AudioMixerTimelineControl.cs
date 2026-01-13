using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerTimelineControl : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName;

    [Range(-80f, 0f)]
    public float value;

    void Update()
    {
        mixer.SetFloat(parameterName, value);
    }
}