using ClanGenModTool.ObjectTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.UI.SubWindows
{
	public class Editor
	{
		protected static string? loadedJson;
		protected static string? loadedPath;

		public static void Load()
		{
			loadedPath = "";
			var dialog = new FileDialog();
			if(dialog.ShowDialog("Select Json", "json"))
			{
				loadedPath = dialog.SelectedPath;
			}
			if(loadedPath != null && loadedPath != "")
				loadedJson = File.ReadAllText(loadedPath);
			else
				loadedJson = null;
		}

		public static void Load(ref bool editorActive)
		{
			loadedPath = "";
			var dialog = new FileDialog();
			if(dialog.ShowDialog("Select Json", "json"))
			{
				loadedPath = dialog.SelectedPath;
			}
			if(loadedPath != null && loadedPath != "")
				loadedJson = File.ReadAllText(loadedPath);
			else
				loadedJson = null;
			editorActive = true;
		}
	}
}
