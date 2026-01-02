using UnityEngine;

public class AccesoriesDisplay : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private View _view;

    private void Start()
    {
        if (TryGetComponent<Animator>(out Animator anim))
            _anim = anim;
        else
            _anim = gameObject.AddComponent<Animator>();
    }

    public void SetupView(View view)
    {
        _view = view;

    }
}

public enum Accesories
{
    Beach,
    Chef,
    Christmas,
    Cupcake,
    Pumpkin,
    Mariachi,
    Stash
}