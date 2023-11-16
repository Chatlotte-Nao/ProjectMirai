// using System.Collections.Generic;
//
// namespace EGamePlay.Combat
// {
//     /// <summary>
//     /// 浮点型修饰器
//     /// </summary>
//     public class FloatModifier
//     {
//         public float Value;
//     }
//
//     /// <summary>
//     /// 浮点型修饰器集合
//     /// </summary>
//     public class FloatModifierCollection
//     {
//         public float TotalValue { get; private set; }
//         private List<FloatModifier> Modifiers { get; } = new List<FloatModifier>();
//
//         public float AddModifier(FloatModifier modifier)
//         {
//             Modifiers.Add(modifier);
//             Update();
//             return TotalValue;
//         }
//
//         public float RemoveModifier(FloatModifier modifier)
//         {
//             Modifiers.Remove(modifier);
//             Update();
//             return TotalValue;
//         }
//
//         public void Update()
//         {
//             TotalValue = 0;
//             foreach (var item in Modifiers)
//             {
//                 TotalValue += item.Value;
//             }
//         }
//     }
//
//     public enum ModifierType : int
//     {
//         Add,
//         PctAdd,
//         FinalAdd,
//         FinalPctAdd,
//     }
//
//     /// <summary>
//     /// 【浮点型数值，用来表示角色的属性值，计算公式如下】
//     /// 第一层攻击力 = 英雄等级基础攻击力
//     /// 第二层攻击力 = (一层攻击力 + 固定装备加成) * (1 + 百分比装备加成)
//     /// 第三层攻击力 = (二层攻击力 + 固定buff加成) * (1 + 百分比buff加成)
//     /// 英雄总攻击力 = 第三层攻击力
//     /// </summary>
//     public class FloatNumeric : Entity
//     {
//         public float Value { get; private set; }
//         /// <summary>
//         /// 基础数值
//         /// </summary>
//         public float baseValue { get; private set; }
//         /// <summary>
//         /// 固定装备加成数值
//         /// </summary>
//         public float add { get; private set; }
//         /// <summary>
//         /// 百分比装备加成
//         /// </summary>
//         public float pctAdd { get; private set; }
//         /// <summary>
//         /// 固定buff数值加成
//         /// </summary>
//         public float finalAdd { get; private set; }
//         /// <summary>
//         /// 百分比buff数值加成
//         /// </summary>
//         public float finalPctAdd { get; private set; }
//         private Dictionary<int, FloatModifierCollection> TypeModifierCollections { get; } = new Dictionary<int, FloatModifierCollection>();
//         public AttributeType AttributeType { get; set; }
//
//
//         public override void Awake()
//         {
//             baseValue = add = pctAdd = finalAdd = finalPctAdd = 0f;
//             TypeModifierCollections.Add(((int)ModifierType.Add), new FloatModifierCollection());
//             TypeModifierCollections.Add(((int)ModifierType.PctAdd), new FloatModifierCollection());
//             TypeModifierCollections.Add(((int)ModifierType.FinalAdd), new FloatModifierCollection());
//             TypeModifierCollections.Add(((int)ModifierType.FinalPctAdd), new FloatModifierCollection());
//         }
//
//         public float SetBase(float value)
//         {
//             baseValue = value;
//             Update();
//             return baseValue;
//         }
//
//         public float AddBase(float value)
//         {
//             baseValue += value;
//             Update();
//             return baseValue;
//         }
//
//         public float MinusBase(float value)
//         {
//             baseValue -= value;
//             if (baseValue < 0) baseValue = 0;
//             Update();
//             return baseValue;
//         }
//
//         public void AddModifier(ModifierType modifierType, FloatModifier modifier)
//         {
//             var value = TypeModifierCollections[((int)modifierType)].AddModifier(modifier);
//             if (modifierType == ModifierType.Add) add = value;
//             if (modifierType == ModifierType.PctAdd) pctAdd = value;
//             if (modifierType == ModifierType.FinalAdd) finalAdd = value;
//             if (modifierType == ModifierType.FinalPctAdd) finalPctAdd = value;
//             Update();
//         }
//
//         public void RemoveModifier(ModifierType modifierType, FloatModifier modifier)
//         {
//             var value = TypeModifierCollections[((int)modifierType)].RemoveModifier(modifier);
//             if (modifierType == ModifierType.Add) add = value;
//             if (modifierType == ModifierType.PctAdd) pctAdd = value;
//             if (modifierType == ModifierType.FinalAdd) finalAdd = value;
//             if (modifierType == ModifierType.FinalPctAdd) finalPctAdd = value;
//             Update();
//         }
//
//         private void Update()
//         {
//             var value1 = baseValue;
//             var value2 = (value1 + add) * (100 + pctAdd) / 100f;
//             var value3 = (value2 + finalAdd) * (100 + finalPctAdd) / 100f;
//             Value = value3;
//             Parent.GetComponent<AttributeComponent>().OnNumericUpdate(this);
//         }
//     }
// }