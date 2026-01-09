using System.Linq;
using UnityEngine;

public class AccesoriesDisplay : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private View _view;

    [SerializeField] private Accesory[] _accesories;
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

    public void WearHat(Accesories type)
    {
        foreach (var acc in _accesories)
        {
            acc.sr.enabled = false;
        }

        _accesories.First(x => x.type == type).sr.enabled = true;
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