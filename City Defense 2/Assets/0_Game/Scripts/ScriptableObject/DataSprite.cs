using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataSprite")]
public class DataSprite : SerializedScriptableObject
{
    public List<List<List<Sprite>>> ListSpriteIconChar;

    public List<Sprite> ListSpriteGroup;

    public List<Sprite> ListSpriteBgTier;

    public List<Sprite> ListSpriteBgTierUnder;

    public List<Sprite> ListSpriteTabLobby;

    public List<List<List<Sprite>>> ListSpriteIconEnermy;

    public Sprite GetSpriteChar(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return ListSpriteIconChar[(int)typeGroup][(int)typeTier][(int)typeId];
    }

    public Sprite GetSpriteGroup(TypeGroup typeGroup)
    {
        return ListSpriteGroup[(int)typeGroup];
    }

    public Sprite GetSpriteBgTier(TypeTier typeTier)
    {
        return ListSpriteBgTier[(int)typeTier];
    }

    public Sprite GetSpriteBgTierUnder(TypeTier typeTier)
    {
        return ListSpriteBgTierUnder[(int)typeTier];
    }

    public Sprite GetSpriteTabLobby(TypeTabLobby typeTabLobby)
    {
        return ListSpriteTabLobby[(int)typeTabLobby];
    }

    public Sprite GetSpriteIconEnermy(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return ListSpriteIconEnermy[(int)typeGroup][(int)typeTier][(int)typeId];
    }
}
