using UnityEngine;

public class AccesoryItem : MonoBehaviour
{
    public Accesories accesorieType;
    public AccesoriesDisplay kk;
    public LineRenderer lr;
    public Transform hangPoint;
    public SpringJoint2D sj;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        hangPoint = transform.GetChild(0);
        hangPoint.parent = null;
        sj = GetComponent<SpringJoint2D>();
        sj.connectedAnchor = hangPoint.position;

    }

    private void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hangPoint.position);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            kk.WearHat(accesorieType);
        }
    }
}
