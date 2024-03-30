using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class DataMap
{
    public TypeMap TypeMap;

    public TypeShapeMap TypeShapeMap;

    public List<DataSpecialObject> DataSpecialObjects;
}

public class DataSpecialObject
{
    public TypeSpecialObject specialObj;

    public Vector3 position;

    public Quaternion rotation;
}
