using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioSnippetPlayer : MonoBehaviour
{
    public float interval = 30f;         // Time between each snippet
    public float snippetLength = 3f;     // Duration of each snippet

    private AudioSource audioSource;
    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip == null)
        {
            Debug.LogWarning("AudioSource has no clip assigned.");
            enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            PlayRandomSnippet();
        }

        // Stop playback after snippet length
        if (audioSource.isPlaying && audioSource.time >= audioSource.clip.length)
        {
            audioSource.Stop();
        }
    }

    void PlayRandomSnippet()
    {
        float maxStartTime = audioSource.clip.length - snippetLength;

        if (maxStartTime <= 0f)
        {
            Debug.LogWarning("Clip is too short for snippet playback.");
            return;
        }

        float startTime = Random.Range(0f, maxStartTime);
        audioSource.time = startTime;
        audioSource.Play();
        Invoke(nameof(StopSnippet), snippetLength);
    }

    void StopSnippet()
    {
        audioSource.Stop();
    }
}
