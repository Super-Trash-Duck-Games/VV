using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private bool _initial;
    [SerializeField] private bool _final;

    [SerializeField] private Animator _anim;
    [SerializeField] private AnimationClip _clip;
    void Start()
    {
        if (_initial) _anim.SetTrigger("Initial");
        if (_final) _anim.SetTrigger("Final");

        StartCoroutine(ChangeLevelAfterCinematic());
    }

    private IEnumerator ChangeLevelAfterCinematic()
    {
        float cinematicLenght = _clip.length;

        yield return new WaitForSeconds(cinematicLenght);

        ChangeLevelManager.Instance.GoToNextScene();
    }
}