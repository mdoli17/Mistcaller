using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPuzzle : MonoBehaviour
{
    public Lever SpikeLever;
    public Lever CageLever;
    public GameObject spikes;
    public GameObject cage;
    public GameObject upperCage;

    private void Start() {
        SpikeLever.LeverInteractDelegate += OpenSpikes;
        CageLever.LeverInteractDelegate += OpenCages;
    }

    void OpenSpikes()
    {
        spikes.SetActive(false);
    }

    void OpenCages()
    {
        upperCage.SetActive(false);
        cage.SetActive(true);
    }
}
