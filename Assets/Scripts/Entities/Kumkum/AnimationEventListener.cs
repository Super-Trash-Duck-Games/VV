using System;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public Action OnEntryDone;
    public Action OnMorphDone;

    public void OnEntryAnimationDone()
    {
        OnEntryDone?.Invoke();
    }

    public void OnMorphAnimationDone()
    {
        OnMorphDone?.Invoke();
    }
}
