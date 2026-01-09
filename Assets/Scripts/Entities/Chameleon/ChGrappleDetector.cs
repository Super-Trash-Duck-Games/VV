using UnityEngine;

public class ChGrappleDetector : MonoBehaviour
{
    private Chameleon _chameleon;

    [SerializeField] private CircleCollider2D _circleCol;


    private void Start()
    {
        _chameleon = GetComponentInParent<Chameleon>();

        if (_circleCol == null) _circleCol = GetComponent<CircleCollider2D>();
        _circleCol.radius = _chameleon.CHPackage.grappleDetectionLenght;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GrapplePoint")
        {
            _chameleon.OnEnterGrapple(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GrapplePoint")
        {
            _chameleon.OnLeaveGrapple(collision);

        }
    }
}
