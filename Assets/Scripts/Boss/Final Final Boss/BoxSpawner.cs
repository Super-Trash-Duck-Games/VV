using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private DestructibleBox _boxPrefab;
    [SerializeField] private Transform _right, _left;
    [SerializeField] private float _delay;
    [SerializeField] private int _pileAmount;
    private List<DestructibleBox> _destructibleBoxes = new List<DestructibleBox>();

    public void CreateBoxWall(bool right)
    {
        StartCoroutine(InstantiateBoxPile(right));
    }

    private IEnumerator InstantiateBoxPile(bool right)
    {
        DestructibleBox box;
        int counter = 0;
        while (counter < _pileAmount)
        {
            counter++;

            box = Instantiate(_boxPrefab, null);
            _destructibleBoxes.Add(box);

            if (right) box.transform.position = _right.position;
            else box.transform.position = _left.position;

            yield return null;
        }
    }

    public void DestroBoxPile()
    {
        foreach (var box in _destructibleBoxes)
        {
            box.Destroy();
        }

        _destructibleBoxes.Clear();
    }
}
