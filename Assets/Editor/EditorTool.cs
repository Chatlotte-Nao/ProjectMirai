// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text.RegularExpressions;
// using UnityEditor;
// using UnityEngine;
// using OfficeOpenXml;
//
// public class EditorTool
// {
// [MenuItem("Assets/找出引用到的预制体和场景")]
// 	private static void Find()
// 	{
// 		string rootPath = Application.dataPath;
// 		List<string> withoutExtensions = new List<string>() {".prefab", ".unity"};
// 		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
// 		if (!string.IsNullOrEmpty(path))
// 		{
// 			string guid = AssetDatabase.AssetPathToGUID(path);
// 			string[] files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories)
// 				.Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
// 			int startIndex = 0;
// 			UnityEngine.Debug.Log("匹配开始" + path);
// 			EditorApplication.update = delegate()
// 			{
// 				string file = files[startIndex];
// 		
// 				bool isCancel =
// 					EditorUtility.DisplayCancelableProgressBar("匹配资源中", file,
// 						(float) startIndex / (float) files.Length);
// 		
// 				if (Regex.IsMatch(File.ReadAllText(file, System.Text.Encoding.UTF8), guid))
// 				{
// 					
// 					Debug.Log(file);
// 					// UnityEngine.Debug.Log(file,
// 					// 	AssetDatabase.LoadAssetAtPath(GetRelativeAssetsPath(file), typeof(System.Object)));
// 				}
// 		
// 				startIndex++;
// 				if (isCancel || startIndex >= files.Length)
// 				{
// 					EditorUtility.ClearProgressBar();
// 					EditorApplication.update = null;
// 					startIndex = 0;
// 					UnityEngine.Debug.Log("匹配结束");
// 				}
// 			};
// 		}
// 	}
// 	
// 	public class CheckResLocalizeDiffWindow : EditorWindow
// 	{
// 		//简中
// 		public static bool IsCheckZh_Cn;
//
// 		//日语
// 		public static bool IsCheckJa;
//
// 		//繁中
// 		public static bool IsCheckZh_Tw;
//
// 		//韩语
// 		public static bool IsChecko;
//
// 		//英文
// 		public static bool IsCheckEn;
//
// 		//泰语
// 		public static bool IsCheckThai;
//
// 		private List<string> CheckAreaList = new List<string>();
//
// 		private static void OpenWindow()
// 		{
// 			CheckResLocalizeDiffWindow window = GetWindow<CheckResLocalizeDiffWindow>();
// 			window.titleContent = new GUIContent("检查资源区别");
// 			window.Show();
// 			window.Focus();
// 		}
//
// 		private void OnGUI()
// 		{
// 			IsCheckZh_Cn = GUILayout.Toggle(IsCheckZh_Cn, "检查简中");
// 			IsCheckJa = GUILayout.Toggle(IsCheckJa, "检查日语");
// 			IsCheckZh_Tw = GUILayout.Toggle(IsCheckZh_Tw, "检查繁中");
// 			IsChecko = GUILayout.Toggle(IsChecko, "检查韩语");
// 			IsCheckEn = GUILayout.Toggle(IsCheckEn, "检查英语");
// 			IsCheckThai = GUILayout.Toggle(IsCheckThai, "检查泰语");
// 			if (GUILayout.Button("开始检查"))
// 			{
// 				AddCheckArea();
// 				CheckDiff();
// 			}
// 		}
//
// 		/// <summary>
// 		/// 添加打上勾的语言
// 		/// </summary>
// 		private void AddCheckArea()
// 		{
// 			CheckAreaList.Clear();
// 			if (IsCheckZh_Cn)
// 			{
// 				CheckAreaList.Add("zh-cn");
// 			}
//
// 			if (IsCheckJa)
// 			{
// 				CheckAreaList.Add("ja");
// 			}
//
// 			if (IsCheckZh_Tw)
// 			{
// 				CheckAreaList.Add("zh-tw");
// 			}
//
// 			if (IsChecko)
// 			{
// 				CheckAreaList.Add("ko");
// 			}
//
// 			if (IsCheckEn)
// 			{
// 				CheckAreaList.Add("en");
// 			}
//
// 			if (IsCheckThai)
// 			{
// 				CheckAreaList.Add("Thai");
// 			}
// 		}
//
// 		/// <summary>
// 		/// 开始检查
// 		/// </summary>
// 		private void CheckDiff()
// 		{
// 			string _filePath = Application.streamingAssetsPath + "/资源差异.xlsx";
// 			string _sheetName = "详情";
// 			FileInfo _excelName = new FileInfo(_filePath);
// 			if (_excelName.Exists)
// 			{
// 				_excelName.Delete();
// 				_excelName = new FileInfo(_filePath);
// 			}
//
// 			Queue<List<string>> AssetQueue = new Queue<List<string>>();
// 			List<List<string>> AssetList = new List<List<string>>();
// 			for (int i = 0; i < CheckAreaList.Count; i++)
// 			{
// 				string searchpath = string.Format("Assets/ResLocalize/{0}/Textures", CheckAreaList[i]);
// 				string[] folder = new string[] {searchpath};
// 				string[] guids = AssetDatabase.FindAssets(null, folder);
// 				List<string> Asset = new List<string>();
// 				for (int j = 0; j < guids.Length; j++)
// 				{
// 					string filepath = AssetDatabase.GUIDToAssetPath(guids[j]);
// 					int index = filepath.LastIndexOf('/');
// 					string filename = filepath.Substring(index + 1, filepath.Length - index - 1);
// 					Asset.Add(filename);
// 				}
//
// 				AssetQueue.Enqueue(Asset);
// 				AssetList.Add(Asset);
// 			}
//
// 			List<string> lastlist = AssetQueue.Peek();
// 			while (AssetQueue.Count > 0)
// 			{
// 				var list = AssetQueue.Dequeue();
// 				if (list.Count == 0)
// 				{
// 					continue;
// 				}
//
// 				lastlist = list.Intersect(lastlist).ToList();
// 			}
//
// 			using (ExcelPackage package = new ExcelPackage(_excelName))
// 			{
// 				ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_sheetName);
// 				worksheet.Cells[1, 1].Value = "共有";
// 				for (int i = 0; i < CheckAreaList.Count; i++)
// 				{
// 					worksheet.Cells[1, i + 2].Value = CheckAreaList[i];
// 				}
//
// 				for (int i = 0; i < lastlist.Count; i++)
// 				{
// 					worksheet.Cells[i + 2, 1].Value = lastlist[i];
// 				}
//
// 				for (int i = 0; i < AssetList.Count; i++)
// 				{
// 					var list = AssetList[i];
// 					for (int j = 0; j < list.Count; j++)
// 					{
// 						worksheet.Cells[j + 2, i + 2].Value = list[j];
// 					}
// 				}
//
// 				package.Save();
// 			}
// 		}
//
// 		[MenuItem("Tools/本地化/检查ResLocalize各语言Textures文件夹的差异",false,0)]
// 		public static void CheckResLocalizeDiff()
// 		{
// 			OpenWindow();
// 		}
// 	}
// }
//
