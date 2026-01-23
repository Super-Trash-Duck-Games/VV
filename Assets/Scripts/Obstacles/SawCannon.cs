using UnityEngine;

public class SawCannon : MonoBehaviour
{

    [SerializeField] private AmmoSaw _saw;

    void Start()
    {
        
    }

    public void Shoot()
    {
        var saw =  Instantiate(_saw, transform.position, Quaternion.identity);
        saw.Shoot(transform.up);
    }

}
