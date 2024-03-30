using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataMapRender")]
public class DataMapRender : SerializedScriptableObject
{
    public List<DataEachMapRender> dataMapRenders;
}

public class DataEachMapRender
{
    public string NameMap;

    public Sprite spriteRender;

    public Sprite spriteBgLobby;

    public string Description;
}