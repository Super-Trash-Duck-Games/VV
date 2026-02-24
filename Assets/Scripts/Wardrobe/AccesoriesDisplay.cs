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

        EventManager.Subscribe("Accesory", OnAccesory);
    }

    private void OnEnable()
    {
        var placer = FindFirstObjectByType<PersistentAccesoryPlacer>();
        if (placer == null) return;
        if (placer.usingAHat)
            WearHat(placer.current);
    }

    private void OnDestroy()
    {
        EventManager.UnSubsctribe("Accesory", OnAccesory);
    }

    private void OnAccesory(params object[] parameters)
    {
        WearHat((Accesories)parameters[0]);
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