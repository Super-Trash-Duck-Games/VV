using UnityEngine;

public class WaterTubeDecoration : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private int _tube;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetInteger("Tube", _tube);
    }
}
