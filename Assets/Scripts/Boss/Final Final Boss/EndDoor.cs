using System.Collections;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    [SerializeField] private PortalDoor _portalDoor;

    [SerializeField] private float _waittime;


    public void Activate()
    {
        StartCoroutine(ActivateFinalDoor());
    }

    private IEnumerator ActivateFinalDoor()
    {
        Rigidbody2D rb2d = _portalDoor.gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 1;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        Collider2D col = rb2d.GetComponent<Collider2D>();
        col.isTrigger = false;

        yield return new WaitForSeconds(_waittime);

        rb2d.gravityScale = 0;
        col.isTrigger = true;
    }
}
