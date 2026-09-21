using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PemancarEvent : MonoBehaviour
{
    // Delegate & Event Publisher
    public static event Action SaatTombolDitekan;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[PemancarEvent] Tombol Space ditekan, memancarkan event!");
            SaatTombolDitekan?.Invoke();
        }
    }
}