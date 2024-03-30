
public static class Help
{
    // use to contain all const string

    public const string DATA_LEVEL = "Level";

    public const float NUMBER_CONFIG_SCREEN_W = 1;

    public const float NUMBER_CONFIG_SCREEN_H = 1;

    public const float TIME_SNIPER_STUN = 0.5f;
    
    public delegate void OnDie();
}

public enum TypeAlly
{
    Barel,
    MeleeAlly,
    RangeAlly
}

public enum TypeEnermy
{
    EnermyNormal,
    EnermyRange,
    EnermySpecial,
}

public enum TypeMap
{
    NewYork,
    Shahara,
    Tokyo,
    Slovakia
}

public enum TypeSpecialObject
{
    special1,
    special2,
    special3,
}

public enum TypeShapeMap
{
    Shape1,
    Shape2,
    Shape3,
    Shape4
}

public enum TypeShader
{
    Default,
    GetHit,
    Die
}

public enum TypeRender
{
    Mesh,
    Sprite
}

public enum TypeSetting
{
    Music,
    Sound,
    Haptic
}

[System.Flags]
public enum TypeUnlock
{
    None = 0,
    Coin = 1,
    Gem = 1 << 1,
    Reward = 1 << 2,
}

public enum TypeSpin
{
    X2,
    X3,
    X4,
    X5
}

public enum DirectionSqawn
{
    Staight,
    Right,
    Left,
}

public enum TypeUiLevelPack
{
    Normal,
    MaxTier
}

public enum TypeEyesSkin
{
    eye_skin_1,
    eye_skin_3,
    eye_skin_4
    //eye_skin_5
}

public enum TypeShop
{
    Pack,
    SpecialWeapon,
    Gold,
    Gem
}

public enum TypeBuy
{
    Money,
    Gem,
    Reward,
    Gold
}

public enum TypeBonus
{
    None,
    Health,
    Attack,
    AttackSpeed,
    MoveSpeed,
    ReduceDamage,
    AttackRange
}

public enum TypeGoShop
{
    Default,
    Ticket,
    Gold,
    Nuclear,
    Gem
}

public class Buy
{
    public TypeBuy TypeBuy;

    public float Index;
}

public enum TypeCamera
{
    Lobby,
    MainGame
}

public enum TypeSpecialWeapon
{
    Hero,
    Boom
}

public enum EffectType
{
    None,
    StatChange,
    Stun,
    Bleed,
    Freeze,
    Poison,
    Burn,
    Block,
    Immune,
    Immunity,
    CounterAttack,
    Healing,
    Damage,
    Dodge,
    MultipleStatsChanger,
    BlockHealing
}

public enum TypeId
{
    Id0
}

public enum AllyId
{
    Melee,
    Range,
    Barrier,
    Hero
}

public enum TypeReward
{

}

public enum TypeLuckySpin
{
    Gold,
    Gem,
    Skin,
    Nuclear,
    Hero
}

public enum TypeAnimation
{
    Idle,
    Run,
    Attack,
    Skill,
    Falling,
    Break,
    GetHit,
    Die,
    Attack_L,
    Attack_R,
    Congratulation
}

public enum TypeWeapon
{
    Normal,
    OneShot,
    NuclearBoom,
    AOE,
    Melee,
    MeleeCrit,
    Range,
    RangeCrit,
    DOT
}

public enum TypeEffectAttack
{
    Slow,
    SniperStun,
    DOT,
    SlowOppressor,
    SpeedUp,
    DOTSniperEnermy,
    DOTGunnerEnermy
}

public enum TypePosition
{
    Sky,
    Ground,
    UnderGround
}

public enum TypeState
{
    Normal,
    Fly,
    Jump
}

public enum TypeGroup
{
    Barrier,
    Vanguard,
    Sniper,
    Gunner,
    Oppressor,
    Boss
}

public enum TypeTier
{
    C,
    B,
    A
}

public enum TypeSlotEquip
{
    Slot1,
    Slot2,
    Slot3
}

public enum TypeQuestGame
{
    None,
    Health_Percent_80,
    Health_Percent_60,
    Health_Percent_30,
    Use_Vanguard,
    Use_Sniper,
    Use_Gunner,
    Use_Oppressor,
    Use_Support_Item
}

public enum TypeSpecialIndex
{
    Vanguard_Ally_C_ReduceDamage,
    Vanguard_Ally_B_Index_Energy,
    Vanguard_Ally_A_HP_Lost,
    Vanguard_Ally_A_Damage_Inscrease,
    Sniper_Ally_C_Percent_Crit,
    Sniper_Ally_C_Index_Damage_Crit,
    Sniper_Ally_B_Percent_Stun,
    Sniper_Ally_B_Time_Stun,
    Sniper_Ally_A_Percent_OneShot,
    Sniper_Ally_A_Condition_HP_Percent,
    Gunner_Ally_C_Range_AOE,
    Gunner_Ally_C_Percent_Normal_Damage,
    Gunner_Ally_C_Percent_AOE_Damage,
    Gunner_Ally_C_Index_Damage_Earn_From_Melee,
    Gunner_Ally_C_Index_Damage_Earn_From_Range,
    Gunner_Ally_B_Range_AOE,
    Gunner_Ally_B_Percent_Damage_DOT_Per_Second,
    Gunner_Ally_B_Time_DOT,
    Gunner_Ally_A_Range_AOE,
    Gunner_Ally_A_Index_Reduce_Speed,
    Gunner_Ally_A_Time_Reduce_Speed,
    Oppressor_Ally_C_Time_Reduce_Speed,
    Oppressor_Ally_B_Percent_Crit,
    Opperssor_Ally_B_Index_Crit_Damage,
    Vanguard_Enemy_C_Index_Inscrease_Speed,
    Vanguard_Enemy_C_Time_Inscrease_Speed,
    Vanguard_Enemy_B_Index_Crazy_State,
    Sniper_Enemy_B_DOT_Time,
    Sniper_Enemy_B_Index_Damage_DOT,
    Gunner_Enemy_C_Range_AOE,
    Gunner_Enemy_C_Index_Damage_AOE_Enemy,
    Gunner_Enemy_B_Range_AOE,
    Gunner_Enemy_B_DOT_Time,
    Gunner_Enemy_B_Index_Damage_DOT,
    Gunner_Enemy_B_Index_Damage_AOE_Barrier,
    Oppressor_Enemy_C_Index_Damage_Earn_Has_Sheild,
    Oppressor_Enemy_C_Number_To_Break_Sheild,
    Oppressor_Enemy_B_Index_Damage_Earn_Crit_Has_Sheild,
}

public enum TypeTabLobby
{
    Shop,
    Stage,
    Unit
}

public enum TypePopupLobby
{
    SwapPopup,
    ProfileAllyPopup,
    EnergyPopup,
    ProfileEnermyPopup,
    RewardStagePopup
}

public enum TypeTabCardUnit
{
    All,
    Barrier,
    Vanguard,
    Sniper,
    Gunner,
    Oppressor
}