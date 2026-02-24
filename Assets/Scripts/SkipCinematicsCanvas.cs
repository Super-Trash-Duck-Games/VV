using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkipCinematicsCanvas : MonoBehaviour
{
    public ChangeLevelManager _clm;
    private Canvas _canvas;
    [SerializeField] private float _skipSpeed;
    [SerializeField] private Image _cross;
    [SerializeField] private Image _circle;
    void Start()
    {
        if (_canvas == null) _canvas = GetComponent<Canvas>();
        if (_clm == null) _clm = FindFirstObjectByType<ChangeLevelManager>();
        _cross.enabled = false;
        _circle.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            StartCoroutine(OnSkipStart());
    }

    private IEnumerator OnSkipStart()
    {
        _cross.enabled = true;
        _circle.enabled = true;
        _circle.fillAmount = 0;
        float counter = 0f;
        while (!Input.GetButtonUp("Cancel"))
        {
            counter += Time.deltaTime * _skipSpeed;
            _circle.fillAmount = counter;
            if (counter > 1)
            {
                _clm.GoToNextScene();
                yield break;
            }
            yield return null;
        }

        _cross.enabled = false;
        _circle.enabled = false;
    }
}
