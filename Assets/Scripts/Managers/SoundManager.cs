using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Library Reference")]
    public AudioLibrary audioLibrary;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource dialogueSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayTrack(TrackType type, string trackName)
    {
        AudioTrack track = audioLibrary.GetTrack(type, trackName);
        if (track == null || track.clip == null)
        {
            Debug.LogWarning($"Audio track not found or clip missing: {type}, {trackName}");
            return;
        }

        switch (type)
        {
            case TrackType.Music:
                PlayMusic(track.clip);
                break;
            case TrackType.Dialogue:
                PlayDialogue(track.clip);
                break;
            case TrackType.SFX:
                if (track.locationType == LocationType.PlayerLocated)
                {
                    PlaySFX(track.clip);
                }
                else if (track.locationType == LocationType.SceneLocated && track.roomTransform != null)
                {
                    // Scene-located SFX with custom params
                    PlaySpatialSFX(track.clip,
                                   track.roomTransform.position,
                                   track.volume, track.pitch, track.spatialBlend, track.minDistance, track.maxDistance);
                }
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayDialogue(AudioClip clip)
    {
        dialogueSource.Stop();
        dialogueSource.loop = false;
        dialogueSource.clip = clip;
        dialogueSource.Play();
    }

    public void StopDialogue()
    {
        dialogueSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySpatialSFX(AudioClip clip, Vector3 position, float volume, float pitch, float spatialBlend, float minDistance, float maxDistance)
    {
        if (clip == null)
        {
            Debug.LogWarning("No clip provided to PlaySpatialSFX.");
            return;
        }

        GameObject audioObj = new GameObject("SpatialSFX_" + clip.name);
        audioObj.transform.position = position;

        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.spatialBlend = spatialBlend;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.playOnAwake = false;
        source.loop = false;

        source.Play();
        StartCoroutine(DestroyAfterPlay(audioObj, clip.length));
    }

    private IEnumerator DestroyAfterPlay(GameObject audioObj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(audioObj);
    }
}
