using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    GameObject GetPlayer();

}

public class PlayerBehaviour : DbService , IPlayer
{
    protected override void OnRegisterServices()
    {
        Services.RegisterAs<IPlayer>(this);
    }

    protected override void OnObjectDestroyed()
    {
        Services.Unregister(this);
    }

    public GameObject GetPlayer()
    {
        return this.gameObject;
    }
}
