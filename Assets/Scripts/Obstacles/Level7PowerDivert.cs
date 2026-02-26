using UnityEngine;

public class Level7PowerDivert : MonoBehaviour
{
    public SpriteRenderer fanPower, doorPower;
    public Color activeColor, inActiveColor;
    public AreaEffector2D areaEffector;
    public ZapMachine zapMachine;
    public GameObject fanPS;

    void Start()
    {
        fanPower.color = activeColor;
        doorPower.color = inActiveColor;
        zapMachine.doorOpen += DivertToDoor;
    }

    private void OnDestroy()
    {
        zapMachine.doorOpen -= DivertToDoor;
    }

    private void DivertToDoor(bool divert)
    {
        if (divert)
        {
            fanPower.color = inActiveColor;
            doorPower.color = activeColor;
            areaEffector.enabled = false;
            fanPS.SetActive(false);
        }
        else
        {
            fanPower.color = activeColor;
            doorPower.color = inActiveColor;
            areaEffector.enabled = true;
            fanPS.SetActive(true);
        }
    }

}
