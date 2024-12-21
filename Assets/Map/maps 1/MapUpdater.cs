using UnityEngine;
using UnityEngine.UI;

public class MapUpdater : MonoBehaviour
{
    public RawImage mapRawImage;     // Assign your RawImage in the Inspector
    public Texture[] mapStates;      // Assign your textures in the Inspector

    private int currentIndex = 0;

    // Call this method when a puzzle is solved or to advance the map state
    public void AdvanceMapState()
    {
        currentIndex++;
        if (currentIndex < mapStates.Length)
        {
            mapRawImage.texture = mapStates[currentIndex];
        }
        else
        {
            // If there are no more states, remain on the last one
            mapRawImage.texture = mapStates[mapStates.Length - 1];
        }
    }

    // If you want to set a specific map state by index:
    public void SetMapState(int puzzleIndex)
    {
        if (puzzleIndex >= 0 && puzzleIndex < mapStates.Length)
        {
            currentIndex = puzzleIndex;
            mapRawImage.texture = mapStates[currentIndex];
        }
    }
}
