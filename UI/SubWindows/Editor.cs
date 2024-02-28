using ClanGenModTool.ObjectTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.UI.SubWindows;

public class Editor
{
	protected static string? LoadedJson;
	protected static string? LoadedPath;
	public static string? PublicLoadedPath
	{
		get => LoadedPath;
		set => LoadedPath = value;
	}
	protected static FileDialog Dialog = new();

	public static void Load()
	{
		LoadedPath = "";
		if(Dialog.ShowDialog("Select Json", "json"))
		{
			LoadedPath = Dialog.SelectedPath;
			MainWindow.EditorConfig.SessionHistory.Add(new SessionHistory { Path = LoadedPath, Type = "patrol"});
		}

		LoadedJson = !string.IsNullOrEmpty(LoadedPath) ? File.ReadAllText(LoadedPath) : null;
	}

	public static void Load(ref bool editorActive)
	{
		LoadedPath = "";
		if(Dialog.ShowDialog("Select Json", "json"))
		{
			LoadedPath = Dialog.SelectedPath;
			MainWindow.EditorConfig.SessionHistory.Add(new SessionHistory { Path = LoadedPath, Type = "patrol" });
		}
		LoadedJson = !string.IsNullOrEmpty(LoadedPath) ? File.ReadAllText(LoadedPath) : null;
		editorActive = true;
	}

	public static void Load(ref bool editorActive, string type)
	{
		LoadedPath = "";
		if(Dialog.ShowDialog("Select Json", "json"))
		{
			LoadedPath = Dialog.SelectedPath;
			MainWindow.EditorConfig.SessionHistory.Add(new SessionHistory { Path = LoadedPath, Type = type });
		}
		LoadedJson = !string.IsNullOrEmpty(LoadedPath) ? File.ReadAllText(LoadedPath) : null;
		editorActive = true;
	}

	public static void LoadSkip(ref bool editorActive)
	{
		LoadedJson = !string.IsNullOrEmpty(LoadedPath) ? File.ReadAllText(LoadedPath) : null;
		editorActive = true;
	}
}