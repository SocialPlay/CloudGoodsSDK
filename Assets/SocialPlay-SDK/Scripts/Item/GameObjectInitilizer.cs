using UnityEngine;
using System.Collections;

public abstract class GameObjectInitilizer : MonoBehaviour
{
    public static GameObjectInitilizer initilizer = null;

    void Awake()
    {
        if (initilizer == null)
        {
            initilizer = this;
        }
    }

    public abstract GameObject InitilizeGameObject(UnityEngine.Object prefab);
}
