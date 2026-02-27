using UnityEngine;

public class NoAccesorySpot : MonoBehaviour
{
    public PersistentAccesoryPlacer pap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if( pap == null ) pap = FindFirstObjectByType<PersistentAccesoryPlacer>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) pap.NoAccesory();
    }
}
