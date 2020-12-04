using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum AvailableAbilities
    {
        Hook = 0,
        Smoke = 1
    }

    public AvailableAbilities currentAbility;
}
