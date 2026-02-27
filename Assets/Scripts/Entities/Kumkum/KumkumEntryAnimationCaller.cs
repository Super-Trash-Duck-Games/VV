using System;
using UnityEngine;

public class KumkumEntryAnimationCaller : MonoBehaviour
{
    public Action OnEntryDone;

    public void OnEntry()
    {
        OnEntryDone?.Invoke();
    }
}
