using UnityEngine;

public class GController : Controller
{
    private GModel _gModel;
    private Gargantuar _gargantuar;
    protected float _y;
    public GController(GModel model, Gargantuar gargantuar) : base(model)
    {
        _gModel = model;
        _gargantuar = gargantuar;
    }

    protected override void Move()
    {
        base.Move();
        _y = Input.GetAxisRaw("Vertical");
    }

    protected override void Special()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (_x != 0)
            {
                if (_x > 0)
                    _gModel.Special(new Vector2(1, 0));
                else
                    _gModel.Special(new Vector2(-1, 0)); //1 does not turn into -1 because the gameobject is already scaled to -1 on X by being mirrored
            }
            else if (_y != 0)
            {
                if (_y > 0)
                    _gModel.Special(new Vector2(0, 1));
                else
                    _gModel.Special(new Vector2(0, -1));
            }
            else
            {
                if (_gargantuar.transform.localScale.x > 0)
                    _gModel.Special(new Vector2(1, 0));
                else
                    _gModel.Special(new Vector2(-1, 0)); //1 does not turn into -1 because the gameobject is already scaled to -1 on X by being mirrored
            }
        }
    }
}