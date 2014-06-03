using UnityEngine;
using System.Collections;

public class StandardGameObjectInitilizer : GameObjectInitilizer {

    public override GameObject InitilizeGameObject(Object prefab)
    {
        return GameObject.Instantiate(prefab) as GameObject;
    }
}
