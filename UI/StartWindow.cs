using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using ImGuiNET;
using ClanGenModTool.UI.SubWindows;
using ClanGenModTool.Mod;
using ClanGenModTool.ObjectTypes;
using Texture = ClanGenModTool.Textures.Texture;
using Newtonsoft.Json;
using System.Numerics;

namespace ClanGenModTool.UI
{
	public class StartWindow
	{
		readonly IWindow mWindow;
		bool mPatrolEditorActive = false, mModCreationMenuActive = false, mImplementException = false;
		bool mThoughtEditorActive = false, mNameEditorActive = false, mClanEditorActive = false;
		bool mCatEditorActive = false;
		public static EditorConfig editorConfig = new EditorConfig();
		public static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Treefire33\\ClanGenModTool", "editor.config");
		public Texture Background = null;


		public StartWindow()
		{
			WindowManager.CreateWindow(out mWindow, new(1600, 900), 
				onConfigIO: () =>
				{
					unsafe
					{
						ImGui.GetIO().Fonts.AddFontFromFileTTF(Path.Combine("Resources", "Fonts", "clangen.ttf"), 15.0f/*, null, ImGui.GetIO().Fonts.GetGlyphRangesDefault()*/);
						ImGui.GetIO().Fonts.Build();
					}
				}
			);
			mWindow.Load += () => WindowManager.RegisterRenderDelegate(mWindow, Render);
			mWindow.Load += () => {
				editorConfig = JsonConvert.DeserializeObject<EditorConfig>(File.ReadAllText(configPath))!;
			};
			mWindow.Closing += () => { string s = JsonConvert.SerializeObject(editorConfig); File.WriteAllText(configPath, s); };
			mWindow.Run();
			mWindow.Dispose();
		}

		//FileDialog fd = new FileDialog();
		void DrawMainMenu()
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
						mWindow.Close();
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
							mWindow.Title = "ClanGen Modding Tool   -   Patrol Editing";
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
							mWindow.Title = "ClanGen Modding Tool   -   Thought List Editing";
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
							mWindow.Title = "ClanGen Modding Tool   -   Name Editing";
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
							mWindow.Title = "ClanGen Modding Tool   -   Clan Editing";
						}
						if(ImGui.MenuItem("Select Clan Cats File"))
						{
							CatEditor.Load(ref mCatEditorActive);
							SetAllOtherEditorsInactive("cat");
							clanEdit.catEditor.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Cat Editing";
						}
						ImGui.EndMenu();
					}
					ImGui.EndMenu();
				}
				ImGui.EndMenuBar();
			}
			//ImGui.SetNextWindowSize(new(350, 350));
			/*if(ImGui.Begin("Config/Settings Editor", ImGuiWindowFlags.MenuBar))
			{
				if(ImGui.BeginTabBar("editorGeneral"))
				{
					if(ImGui.BeginTabItem("Settings Editor"))
					{
						ImGui.Text("This feature is in development!");
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Editor Config Editor"))
					{
						ImGui.Text(editorConfig.patrolPath + "\n(Patrol JSONs Path)");
						ImGui.PushID(110);if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.patrolPath = fd.SelectedPath;
						}
						ImGui.PopID();
						ImGui.Text(editorConfig.thoughtPath + "\n(Thought JSONs Path)");
						ImGui.PushID(111);if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.thoughtPath = fd.SelectedPath;
						}ImGui.PopID();
						ImGui.Text(editorConfig.clanPath + "\n(Clan JSON Path)");
						ImGui.PushID(112);if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.clanPath = fd.SelectedPath;
						}
						ImGui.PopID();
						ImGui.Text(editorConfig.gameConfigPath + "\n(Game Config Path)");
						ImGui.PushID(113);if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.gameConfigPath = fd.SelectedPath;
						}
						ImGui.PopID();
						ImGui.Text(editorConfig.settingsPath + "\n(Settings JSON Path)");
						ImGui.PushID(114);if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.settingsPath = fd.SelectedPath;
						}
						ImGui.PopID();
						//if(ImGui.Checkbox("Background Enabled", ref editorConfig.backgroundEnabled)) { }
						if(ImGui.Button("Apply Changes"))
						{
							string serialized = JsonConvert.SerializeObject(editorConfig);
							File.WriteAllText(configPath, serialized);
						}
						ImGui.EndTabItem();
					}
					ImGui.EndTabBar();
				}
				ImGui.End();
			}*/
		}

		PatrolEditor patrolEdit = new PatrolEditor();
		ThoughtEditor thoughtEdit = new ThoughtEditor();
		NameEditor nameEdit = new NameEditor();
		ClanEditor clanEdit = new ClanEditor();

		bool runOnce = false;
		public void Render(GL gl, double delta, ImGuiController controller)
		{
			gl.Viewport(mWindow.FramebufferSize);

			gl.ClearColor(.45f, .55f, .60f, 1f);
			gl.Clear((uint)ClearBufferMask.ColorBufferBit);

			Theme.SetImGuiTheme(ImGui.GetStyle());

			ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
			ImGui.DockSpaceOverViewport();

			DrawMainMenu();

			
			if(mPatrolEditorActive)
			{
				patrolEdit.BeforeDrawEditor(gl);
				patrolEdit.Draw(ref mPatrolEditorActive);
			}
			if(mThoughtEditorActive)
			{
				thoughtEdit.BeforeDrawEditor(gl);
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
			if(mImplementException)
			{
				ErrorBox.Draw("This feature has not been implemented!", ref mImplementException);
			}
			if(mModCreationMenuActive)
			{
				ModMenus.DrawModCreationMenu(ref mModCreationMenuActive);
			}

			/* render our ImGUI controller */
			controller.Render();
		}

		public void SetAllOtherEditorsInactive(string type)
		{
			switch(type)
			{
				case "patrol":
					mModCreationMenuActive = false;
					mThoughtEditorActive = false;
					mNameEditorActive = false;
					mClanEditorActive = false;
					mCatEditorActive = false; 
				break;
				case "thought":
					mModCreationMenuActive = false;
					mPatrolEditorActive = false;
					mNameEditorActive = false;
					mClanEditorActive = false;
					mCatEditorActive = false;
				break;
				case "name":
					mModCreationMenuActive = false;
					mPatrolEditorActive = false;
					mThoughtEditorActive = false;
					mClanEditorActive = false;
					mCatEditorActive = false;
				break;
				case "clan":
					mModCreationMenuActive = false;
					mPatrolEditorActive = false;
					mThoughtEditorActive = false;
					mNameEditorActive = false;
					mCatEditorActive = false;
				break;
				case "cat":
					mModCreationMenuActive = false;
					mPatrolEditorActive = false;
					mThoughtEditorActive = false;
					mNameEditorActive = false;
				break;
			}
		}
	}
}
