using UnityEngine;

public class LevelSelectorAnimation : MonoBehaviour
{
    [SerializeField] private EntityTypes _type;
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _specialLevel;
    [SerializeField] private string _specialString;
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (!_specialLevel)
            _anim.SetTrigger(_type.ToString());
        else
            _anim.SetTrigger(_specialString);
    }
}
