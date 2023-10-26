/// <summary>
/// 玩家、敌人都用这个类来表示，这个类里执行各项操作，放技能，受伤、造成伤害等
/// </summary>
public class BattleCharacter
{

    /// <summary>
    /// 角色当前站位顺序，从左往右数
    /// </summary>
    private int _index;
    /// <summary>
    /// 区分玩家和敌人
    /// </summary>
    public bool IsPlayer { get; private set; }
    /// <summary>
    /// 记录玩家各项属性值
    /// </summary>
    public CharacterAttribute Attribute;

    public BattleCharacter(int index, CharacterAttribute attribute, bool isPlayer)
    {
        _index = index;
        Attribute = attribute;
        IsPlayer = isPlayer;
    }
    /// <summary>
    /// 受到伤害时调用
    /// </summary>
    public void GetDamage()
    {
        
    }

    public void OnDead()
    {
        
    }
    
    
}