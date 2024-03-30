using System.Reflection;
using ClanGenModTool.ObjectTypes;
using ClanGenModTool.UI.SubWindows;
using ClanGenModTool.Windowing;
using ImGuiNET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClanGenModTool.UI;

public class MainWindow : Window
{
	private bool mPatrolEditorActive, mThoughtEditorActive;
	private bool mNameEditorActive, mClanEditorActive;
	public static EditorConfig EditorConfig = new();
	public static string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Treefire33\\ClanGenModTool", "editor.config");
	private readonly PatrolEditor patrolEdit = new();
	private readonly ThoughtEditor thoughtEdit = new();
	private readonly NameEditor nameEdit = new();
	private readonly ClanEditor clanEdit = new();

	public MainWindow()
	{
		LoadEvent += delegate {
			EditorConfig = JsonConvert.DeserializeObject<EditorConfig>(File.ReadAllText(ConfigPath))!;
			PatrolEditor.BeforeDrawEditor();
			ThoughtEditor.BeforeDrawEditor();
			Title = "ClanGen Mod Tool - Menu";
		};
		DrawEvent += Render;
		CloseEvent += () => { string s = JsonConvert.SerializeObject(EditorConfig); File.WriteAllText(ConfigPath, s); };
	}

	public void Render()
	{
		DrawMenuBar();

		if(mPatrolEditorActive)
		{
			patrolEdit.Draw(ref mPatrolEditorActive);
		}
		if(mThoughtEditorActive)
		{
			thoughtEdit.Draw(ref mThoughtEditorActive);
		}
		if(mNameEditorActive)
		{
			nameEdit.Draw(ref mNameEditorActive);
		}
		if(mClanEditorActive)
		{
			clanEdit.Draw(ref mClanEditorActive);
		}

		//ImGui.ShowDemoWindow();
	}

	public void DrawMenuBar()
	{
		if(ImGui.BeginMainMenuBar())
		{
			if(ImGui.BeginMenu("File"))
			{
				if(mPatrolEditorActive && ImGui.MenuItem("Save"))
				{
					patrolEdit.Save();
				}
				if(mThoughtEditorActive && ImGui.MenuItem("Save"))
				{
					thoughtEdit.Save();
				}
				if(mNameEditorActive && ImGui.MenuItem("Save"))
				{
					nameEdit.Save();
				}
				if(mClanEditorActive && ImGui.MenuItem("Save"))
				{
					clanEdit.Save();
				}
				if(ImGui.BeginMenu("Session History"))
				{
					foreach (SessionHistory sh in EditorConfig.SessionHistory.Where(sh => ImGui.MenuItem(sh.Path)))
					{
						Editor.PublicLoadedPath = sh.Path;
						switch(sh.Type)
						{
							case "patrol": Editor.LoadSkip(ref mPatrolEditorActive); patrolEdit.LoadEditor(); break;
							case "thought": Editor.LoadSkip(ref mThoughtEditorActive); thoughtEdit.LoadEditor(); break;
							case "name": Editor.LoadSkip(ref mNameEditorActive); nameEdit.LoadEditor(); break;
							case "clan": Editor.LoadSkip(ref mClanEditorActive); clanEdit.LoadEditor(); break;
						}
						SetAllOtherEditorsInactive(sh.Type);
					}

					ImGui.EndMenu();
				}
				if(ImGui.MenuItem("Close"))
				{
					Close();
				}
				ImGui.EndMenu();
			}
			if(ImGui.BeginMenu("Resource Editing"))
			{
				if(ImGui.BeginMenu("Patrol"))
				{
					if(ImGui.MenuItem("Select Patrols"))
					{
						Editor.Load(ref mPatrolEditorActive, "patrol");
						SetAllOtherEditorsInactive("patrol");
						patrolEdit.LoadEditor();
						Title = "ClanGen Modding Tool   -   Patrol Editing";
					}
					ImGui.EndMenu();
				}
				if(ImGui.BeginMenu("Thoughts"))
				{
					if(ImGui.MenuItem("Select Thoughts"))
					{
						Editor.Load(ref mThoughtEditorActive, "thought");
						SetAllOtherEditorsInactive("thought");
						thoughtEdit.LoadEditor();
						Title = "ClanGen Modding Tool   -   Thought List Editing";
					}
					ImGui.EndMenu();
				}
				if(ImGui.BeginMenu("Names"))
				{
					if(ImGui.MenuItem("Select Names File"))
					{
						Editor.Load(ref mNameEditorActive, "name");
						SetAllOtherEditorsInactive("name");
						nameEdit.LoadEditor();
						Title = "ClanGen Modding Tool   -   Name Editing";
					}
					ImGui.EndMenu();
				}
				ImGui.EndMenu();
			}
			if(ImGui.BeginMenu("Save Editing"))
			{
				if(ImGui.BeginMenu("Clan"))
				{
					if(ImGui.MenuItem("Select Clan File"))
					{
						Editor.Load(ref mClanEditorActive, "clan");
						SetAllOtherEditorsInactive("clan");
						clanEdit.LoadEditor();
						Title = "ClanGen Modding Tool   -   Clan Editing";
					}
					ImGui.EndMenu();
				}
				ImGui.EndMenu();
			}
			if(ImGui.IsKeyDown(ImGuiKey.RightCtrl) && ImGui.BeginMenu("Credits"))
			{
				ImGui.Text("Treefire33");
				ImGui.EndMenu();
			}
			ImGui.EndMenuBar();
		}
	}

	public void SetAllOtherEditorsInactive(string type)
	{
		switch(type)
		{
			case "patrol":
				mThoughtEditorActive = false;
				mNameEditorActive = false;
				mClanEditorActive = false;
				break;
			case "thought":
				mPatrolEditorActive = false;
				mNameEditorActive = false;
				mClanEditorActive = false;
				break;
			case "name":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mClanEditorActive = false;
				break;
			case "clan":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mNameEditorActive = false;
				break;
			case "cat":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mNameEditorActive = false;
				break;
		}
	}
}