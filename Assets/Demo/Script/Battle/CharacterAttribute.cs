using System.Collections.Generic;

/// <summary>
/// 记录属性值、
/// </summary>
public class CharacterAttribute
{
    public int MaxDp => _maxDp;
    public int Dp => _dp;
    public int MaxHp => _maxHp;
    public int Hp => _hp;
    public int MaxStr => _maxStr;
    public int Str => _str;
    public int MaxDex => _maxDex;
    public int Dex => _dex;
    public int MaxCon => _maxCon;
    public int Con => _con;
    public int MaxSpr => _maxSpr;
    public int Spr => _spr;
    public int MaxWis => _maxWis;
    public int Wis => _wis;
    public int MaxLuk => _maxLuk;
    public int Luk => _luk;
    
    //DP、HP、力量、灵巧、体力、精力、智力、运
    private int _maxDp;
    private int _dp;
    private int _maxHp;
    private int _hp;
    private int _maxStr;
    private int _str;
    private int _maxDex;
    private int _dex;
    private int _maxCon;
    private int _con;
    private int _maxSpr;
    private int _spr;
    private int _maxWis;
    private int _wis;
    private int _maxLuk;
    private int _luk;
    /// <summary>
    /// 配置表中的主动技能Id，抽到角色时，角色身上的主动技能
    /// </summary>
    private List<long> _activeSkillIds;
    /// <summary>
    /// 养成过程中获得的附加主动技能、例如宝珠技能
    /// </summary>
    private List<long> _additionalActiveSkillIds;
    /// <summary>
    /// 被动技能
    /// </summary>
    private List<long> _passiveSkillIds;
    
    public List<long> ActiveSkillIds => _activeSkillIds;
    public List<long> AdditionalActiveSkillIds => _additionalActiveSkillIds;
    public List<long> PassiveSkillIds => _passiveSkillIds;

    public CharacterAttribute(InitCharacterModel model)
    {
        _maxDp=model._maxDp;
        _dp=model._dp;
        _maxHp=model._maxHp;
        _hp=model._hp;
        _maxStr=model._maxStr;
        _str=model._str;
        _maxDex=model._maxDex;
        _dex=model._dex;
        _maxCon=model._maxCon;
        _con=model._con;
        _maxSpr=model._maxSpr;
        _spr=model._spr;
        _maxWis=model._maxWis;
        _wis=model._wis;
        _maxLuk=model._maxLuk;
        _luk=model._luk;
        _activeSkillIds = model.ActiveSkillIds;
        _additionalActiveSkillIds = model.AdditionalActiveSkillIds;
        _passiveSkillIds = model.PassiveSkillIds;
    }

    public void SetHp(int value)
    {
        _hp = value;
    }

    public void SetDp(int value)
    {
        _dp = value;
    }

    public void SetStr(int value)
    {
        _str = value;
    }

    public void SetDex(int value)
    {
        _dex = value;
    }

    public void SetCon(int value)
    {
        _con = value;
    }

    public void SetSpr(int value)
    {
        _spr = value;
    }

    public void SetWis(int value)
    {
        _wis = value;
    }

    public void SetLuk(int value)
    {
        _luk = value;
    }
    
}