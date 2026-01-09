using System.Collections;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private SpriteRenderer _arrow;

    private Coroutine _onDoorway;

    void Start()
    {
        if (_anim == null) _anim = GetComponent<Animator>();
        _arrow.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _anim.SetTrigger("Open");
            _arrow.enabled = true;
            _onDoorway = StartCoroutine(OnDoorway());
        }
    }

    private IEnumerator OnDoorway()
    {
        while (true)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                ChangeLevelManager.Instance.GoToNextScene();
            }
            yield return null; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _anim.SetTrigger("Close");
            _arrow.enabled = false;
            StopCoroutine(_onDoorway);
        }
    }
}