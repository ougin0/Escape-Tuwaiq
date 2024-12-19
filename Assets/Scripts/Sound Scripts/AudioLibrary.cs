using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public List<AudioTrack> tracks = new List<AudioTrack>();

    public AudioTrack GetTrack(TrackType type, string name)
    {
        foreach (var t in tracks)
        {
            if (t.trackType == type && t.trackName == name)
            {
                return t;
            }
        }
        return null;
    }

    public AudioClip GetClip(TrackType type, string name)
    {
        var track = GetTrack(type, name);
        return track != null ? track.clip : null;
    }
}
