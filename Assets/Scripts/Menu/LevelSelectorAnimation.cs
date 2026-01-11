using UnityEngine;

public class LevelSelectorAnimation : MonoBehaviour
{
    [SerializeField] private EntityTypes _type;
    [SerializeField] private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetTrigger(_type.ToString()); 
    }
}
