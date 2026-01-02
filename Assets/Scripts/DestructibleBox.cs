using UnityEngine;

public class DestructibleBox : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private Collider2D _boxCollider;
    [SerializeField] private ParticleSystem _ps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _box.SetActive(false);
            _ps.Play();
            _boxCollider.enabled = false;
            Destroy(this.gameObject, 1);
        }
    }
}
