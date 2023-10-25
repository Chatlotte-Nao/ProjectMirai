
using System.Collections.Generic;

public class BattleDataModel
{
    
}
/// <summary>
/// 从配置表中读取出来的数据，用来进行首次给角色赋值
/// </summary>
public class InitCharacterModel : BattleDataModel
{
    public int index;
    public int _maxDp;
    public int _dp;
    public int _maxHp;
    public int _hp;
    public int _maxStr;
    public int _str;
    public int _maxDex;
    public int _dex;
    public int _maxCon;
    public int _con;
    public int _maxSpr;
    public int _spr;
    public int _maxWis;
    public int _wis;
    public int _maxLuk;
    public int _luk;
    public bool IsPlayer;
    public List<long> ActiveSkillIds;
    public List<long> AdditionalActiveSkillIds;
    public List<long> PassiveSkillIds;
}