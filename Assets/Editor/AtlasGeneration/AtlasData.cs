using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atlases : List<AtlasData> { }

public class AtlasData
{
    public int ID;
    public string Name;
    public byte[] Image;
}
