using UnityEngine;
using System.Collections;

public class ContainerDispalyDeactivateGameObject : ContainerDisplayAction
{
    public override void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
