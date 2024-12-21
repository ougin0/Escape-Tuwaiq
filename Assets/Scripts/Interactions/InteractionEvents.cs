using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class InteractionEvents : MonoBehaviour
{
    public UnityEvent onSelectEntered;
    public UnityEvent onSelectExited;

    void Awake()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener((args) => onSelectEntered.Invoke());
            interactable.selectExited.AddListener((args) => onSelectExited.Invoke());
        }
    }
}
