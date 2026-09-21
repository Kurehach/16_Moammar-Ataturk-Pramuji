using UnityEngine;

public class PenerimaEvent : MonoBehaviour
{
    // Subscriber / Event Listener
    private void OnEnable()
    {
        PemancarEvent.SaatTombolDitekan += Respon;
    }

    private void OnDisable()
    {
        PemancarEvent.SaatTombolDitekan -= Respon;
    }

    void Respon()
    {
        Debug.Log("[PenerimaEvent] Event diterima! Merespon pelepasan tombol Space.");
    }
}