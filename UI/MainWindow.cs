using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClanGenModTool.ObjectTypes;
using ClanGenModTool.Textures;
using ClanGenModTool.UI.SubWindows;
using ClanGenModTool.Utilities;
using ClanGenModTool.Windowing;
using ImGuiNET;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL4;

namespace ClanGenModTool.UI
{
	public class MainWindow : Window
	{
		bool mPatrolEditorActive = false, mImplementException = false;
		bool mThoughtEditorActive = false, mNameEditorActive = false, mClanEditorActive = false;
		bool mCatEditorActive = false;
		public static EditorConfig editorConfig = new EditorConfig();
		public static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Treefire33\\ClanGenModTool", "editor.config");
		PatrolEditor patrolEdit = new PatrolEditor();
		ThoughtEditor thoughtEdit = new ThoughtEditor();
		NameEditor nameEdit = new NameEditor();
		ClanEditor clanEdit = new ClanEditor();

		public MainWindow()
		{
			this.LoadEvent += delegate {
				editorConfig = JsonConvert.DeserializeObject<EditorConfig>(File.ReadAllText(configPath))!;
				PatrolEditor.BeforeDrawEditor();
				ThoughtEditor.BeforeDrawEditor();
				//FromJsonTo.GenerateDictEntryForFacet(".\\Resources\\ExampleJSONs\\ranges.json");
				/*foreach(string key in Constants.traitRanges.Keys)
				{
					for(int i = 0; i < Constants.traitRanges[key].Count; i++)
					{
						Console.WriteLine(Constants.traitRanges[key][i].Item1);
						Console.WriteLine(Constants.traitRanges[key][i].Item2);
					}
				}*/

				foreach(string s in Constants.accessories)
				{
					Console.WriteLine($"\"{s}BELL\",");
				}
				foreach(string s in Constants.accessories)
				{
					Console.WriteLine($"\"{s}BOW\",");
				}
				foreach(string s in Constants.accessories)
				{
					Console.WriteLine($"\"{s}NYLON\",");
				}
			};
			this.DrawEvent += Render;
			this.CloseEvent += delegate { };
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
			if(mCatEditorActive)
			{
				clanEdit.catEditor.Draw(ref mCatEditorActive);
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
						foreach(SessionHistory sh in editorConfig.sessionHistory)
						{
							if(ImGui.MenuItem(sh.path))
							{
								Editor.publicLoadedPath = sh.path;
								switch(sh.type)
								{
									case "patrol": Editor.LoadSkip(ref mPatrolEditorActive); patrolEdit.LoadEditor(); break;
									case "thought": Editor.LoadSkip(ref mThoughtEditorActive); thoughtEdit.LoadEditor(); break;
									case "name": Editor.LoadSkip(ref mNameEditorActive); nameEdit.LoadEditor(); break;
									case "clan": Editor.LoadSkip(ref mClanEditorActive); clanEdit.LoadEditor(); break;
									case "cat": CatEditor.LoadSkip(ref mCatEditorActive); clanEdit.catEditor.LoadEditor(); break;
								}
								SetAllOtherEditorsInactive(sh.type);
							}
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
						if(ImGui.MenuItem("Select Clan Cats File"))
						{
							CatEditor.Load(ref mCatEditorActive);
							SetAllOtherEditorsInactive("cat");
							clanEdit.catEditor.LoadEditor();
							Title = "ClanGen Modding Tool   -   Cat Editing";
						}
						ImGui.EndMenu();
					}
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
				mCatEditorActive = false;
				break;
				case "thought":
				mPatrolEditorActive = false;
				mNameEditorActive = false;
				mClanEditorActive = false;
				mCatEditorActive = false;
				break;
				case "name":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mClanEditorActive = false;
				mCatEditorActive = false;
				break;
				case "clan":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mNameEditorActive = false;
				mCatEditorActive = false;
				break;
				case "cat":
				mPatrolEditorActive = false;
				mThoughtEditorActive = false;
				mNameEditorActive = false;
				break;
			}
		}
	}
}
