using UnityEngine;

public class BossHandCameraMovement : MonoBehaviour
{

    [SerializeField] private BossHand _bossHand;
    private float _speed;

    void Start()
    {
        _bossHand ??= FindFirstObjectByType<BossHand>();
        _speed = _bossHand.Speed;
    }

    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
