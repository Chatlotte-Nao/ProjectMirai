// using NPOI.SS.UserModel;
// using NPOI.XSSF.UserModel;
// using System;
// using System.IO;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
// public class ScenarioImporter : AssetPostprocessor
// {
//     static readonly string IMPORT_PATH = "Assets/Design/Scenarios/";
//
//     //static readonly string	EXPORT_PATH		= "Assets/ResAssetBundles/Scenario/";
//     static readonly int TEXT_CELLNUM = 9;
//     static readonly int MAX_CELLNUM = 15;
//
//     public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
//         string[] movedFromAssetPaths)
//     {
//         var dirty = false;
//         foreach (string importedAsset in importedAssets)
//         {
//             if (!importedAsset.StartsWith(IMPORT_PATH)) continue;
//             if (!importedAsset.EndsWith(".xlsx")) continue;
//
//             string language = importedAsset.Replace(IMPORT_PATH, "").Split('/')[0];
//             language = string.Empty;
//             using (FileStream fs = new FileStream(importedAsset, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
//             {
//                 ReadBook(new XSSFWorkbook(fs), language);
//                 dirty = true;
//             }
//         }
//
//         if (dirty) AssetDatabase.SaveAssets();
//     }
//
//     private static void ReadBook(IWorkbook book, string language)
//     {
//         for (int i = 0; i < book.NumberOfSheets; i++)
//         {
//             ReadSheet(book.GetSheetAt(i), language);
//         }
//     }
//
//     private static void ReadSheet(ISheet sheet, string language)
//     {
//         if (sheet.SheetName.StartsWith("#")) return;
//
//         var t = typeof(ScenarioScript);
//         string assetName = ResourceManager.LOCALIZE_PATH + language + "/Scenario/" + sheet.SheetName + "_" + language +
//                            ".asset";
//
//         ScriptableObject so = GetAsset(t, assetName);
//         int num = 0;
//
//         for (int r = sheet.FirstRowNum; r <= sheet.LastRowNum; r++)
//         {
//             IRow row = sheet.GetRow(r);
//             if (row == null) continue;
//             if (row.LastCellNum == 0) continue;
//
//             ICell cell = row.GetCell(0);
//             string flag = null;
//             if (cell != null) flag = cell.ToString();
//
//             if (string.IsNullOrEmpty(flag))
//             {
//                 if (AddCommand(so, t, row)) num++;
//             }
//             // else if (flag == ":")
//             // {
//             //     AddJumpLabel(so, t, row, num);
//             // }
//         }
//
//         EditorUtility.SetDirty(so);
//     }
//
//     // private static void AddJumpLabel(ScriptableObject so, Type t, IRow row, int num)
//     // {
//     //     if (row.LastCellNum < 2) return;
//     //
//     //     ICell cell = row.GetCell(1);
//     //     string value = null;
//     //     if (cell != null) value = cell.ToString();
//     //
//     //     if (string.IsNullOrEmpty(value)) return;
//     //
//     //     var info = new Game.ScriptEngine.ScenarioScript.JumpInfo();
//     //     info.label = value;
//     //     info.dest = num;
//     //
//     //     FieldInfo fi = t.GetField("jumpTable");
//     //     Type ft = fi.FieldType;
//     //     ft.GetMethod("Add").Invoke(fi.GetValue(so), new object[] {info});
//     // }
//
//     private static bool AddCommand(ScriptableObject so, Type t, IRow row)
//     {
//         if (row.LastCellNum < 1) return false;
//
//         var o = new ScenarioScript.CommandAction();
//         var cellNum = Mathf.Min(row.LastCellNum, MAX_CELLNUM);
//
//         for (int i = 1; i < cellNum; i++)
//         {
//             string value = GetCell(row, i);
//
//             if (string.IsNullOrEmpty(value))
//             {
//                 if (i > 1) continue;
//                 if (i == 1)
//                 {
//                     if (string.IsNullOrEmpty(GetCell(row, TEXT_CELLNUM))) return false;
//                     value = "Text";
//                 }
//             }
//
//             if (i == 1)
//             {
//                 o.command = value;
//             }
//             else
//             {
//                 o.args.Add(value);
//             }
//         }
//
//         FieldInfo fi = t.GetField("commands");
//         Type ft = fi.FieldType;
//         ft.GetMethod("Add").Invoke(fi.GetValue(so), new object[] {o});
//         return true;
//     }
//
//     private static string GetCell(IRow row, int cellnum)
//     {
//         ICell cell = row.GetCell(cellnum);
//         string value = null;
//         if (cell != null) value = cell.ToString();
//         return value;
//     }
//
//     private static ScriptableObject GetAsset(Type t, string assetName)
//     {
//         ScriptableObject so = (ScriptableObject) AssetDatabase.LoadAssetAtPath(assetName, t);
//         if (so == null)
//         {
//             so = ScriptableObject.CreateInstance(t);
//             AssetDatabase.CreateAsset(so, assetName);
//         }
//         else
//         {
//             // foreach (var f in new string[] {"commands", "jumpTable"})
//             // {
//             //     FieldInfo fi = t.GetField(f);
//             //     Type ft = fi.FieldType;
//             //     ft.GetMethod("Clear").Invoke(fi.GetValue(so), null);
//             // }
//             foreach (var f in new string[] {"commands"})
//             {
//                 FieldInfo fi = t.GetField(f);
//                 Type ft = fi.FieldType;
//                 ft.GetMethod("Clear").Invoke(fi.GetValue(so), null);
//             }
//         }
//         return so;
//     }
// }