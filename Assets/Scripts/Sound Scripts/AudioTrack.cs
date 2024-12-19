using UnityEngine;

public enum TrackType
{
    Music,
    Dialogue,
    SFX
}

public enum LocationType
{
    PlayerLocated,
    SceneLocated
}

[System.Serializable]
public class AudioTrack
{
    public TrackType trackType;
    public string trackName;
    public AudioClip clip;

    public LocationType locationType = LocationType.PlayerLocated;
    public Transform roomTransform; // If scene-located, assign this in the editor

    // New field: If false, scene-located tracks = SFX only
    public bool allowCustomTrackType = false;

    // AudioSource settings for scene-located audio
    [Range(0f, 1f)] public float volume = 1f;
    public float pitch = 1f;
    [Range(0f, 1f)] public float spatialBlend = 1f;
    public float minDistance = 1f;
    public float maxDistance = 20f;
}
