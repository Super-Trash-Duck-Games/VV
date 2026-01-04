using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    public static MorphManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private Entity _player;
    [SerializeField] private Entity _currentMorph;
    [SerializeField] private Entity[] _entities;

    private Dictionary<EntityTypes, Entity> _entitiesByType = new Dictionary<EntityTypes, Entity>();

    void Start()
    {
        _player = _entities[0];
        foreach (var entity in _entities)
        {
            var ent = Instantiate(entity);
            _entitiesByType.Add(entity.type, ent);
            ent.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Morph(EntityTypes.Kumkum);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            Morph(EntityTypes.Gargantuar);
        if (Input.GetKeyDown(KeyCode.Keypad3))
            Morph(EntityTypes.Chameleon);
        if (Input.GetKeyDown(KeyCode.Keypad4))
            Morph(EntityTypes.Zap);
        if (Input.GetKeyDown(KeyCode.Keypad5))
            Morph(EntityTypes.Crocodile);

        if (Input.GetKeyDown(KeyCode.I)) MorphBack();
    }

    public void Morph(EntityTypes entity)
    {
        _currentMorph = _entitiesByType[entity];
        _currentMorph.gameObject.SetActive(true);
        _currentMorph.transform.position = _player.transform.position;
        _player.gameObject.SetActive(false);
    }

    private void MorphBack()
    {
        _player.transform.position = _currentMorph.transform.position;
        //_player.gameObject.SetActive(true);
        _currentMorph.MorphBack(_player);
    }

}
