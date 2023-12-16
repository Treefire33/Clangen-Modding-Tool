using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using System.Numerics;
using System.Diagnostics;
using ClanGenModTool.UI.SubWindows;
using ClanGenModTool.Mod;
using ClanGenModTool.ObjectTypes;
using Texture = ClanGenModTool.Textures.Texture;
using Newtonsoft.Json;

namespace ClanGenModTool.UI
{
	public class StartWindow
	{
		readonly IWindow mWindow;
		bool mPatrolEditorActive = false, mModCreationMenuActive = false, mImplementException = false;
		bool mThoughtEditorActive = false, mNameEditorActive = false, mClanEditorActive;
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
						ImGui.GetIO().Fonts.AddFontFromFileTTF(Path.Combine("Resources", "Fonts", "NotoSans-Regular.ttf"), 15.0f/*, null, ImGui.GetIO().Fonts.GetGlyphRangesDefault()*/);
						ImGui.GetIO().Fonts.Build();
					}
				}
			);
			mWindow.Load += () => WindowManager.RegisterRenderDelegate(mWindow, Render);
			mWindow.Load += () => {
				editorConfig = JsonConvert.DeserializeObject<EditorConfig>(File.ReadAllText(configPath))!;
				Console.WriteLine(editorConfig.patrolPath);
			};
			mWindow.Run();
			mWindow.Dispose();
		}

		FileDialog fd = new FileDialog();
		void DrawMainMenu()
		{
			if(ImGui.BeginMainMenuBar())
			{
				if(ImGui.BeginMenu("File"))
				{
					if(ImGui.BeginMenu("Sprite"))
					{
						if(ImGui.BeginMenu("Pelt"))
						{
							if(ImGui.MenuItem("Select Pelt"))
							{
								Console.WriteLine("This feature has not been implemented");
								mImplementException = true;
							}
							if(ImGui.MenuItem("New Pelt Pattern"))
							{
								Console.WriteLine("This feature has not been implemented");
								mImplementException = true;
							}
							ImGui.EndMenu();
						}
						if(ImGui.BeginMenu("Lineart"))
						{
							if(ImGui.MenuItem("Select Lines"))
							{
								Console.WriteLine("This feature has not been implemented");
								mImplementException = true;
							}
							ImGui.EndMenu();
						}
						ImGui.EndMenu();
					}
					if(ImGui.BeginMenu("Patrol"))
					{
						if(ImGui.MenuItem("Select Patrols"))
						{
							Editor.Load(ref mPatrolEditorActive);
							mModCreationMenuActive = false;
							mThoughtEditorActive = false;
							mNameEditorActive = false;
							mClanEditorActive = false;
							patrolEdit.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Patrol Editing";
						}
						ImGui.EndMenu();
					}
					if(ImGui.BeginMenu("Thoughts"))
					{
						if(ImGui.MenuItem("Select Thoughts"))
						{
							Editor.Load(ref mThoughtEditorActive);
							mModCreationMenuActive = false;
							mPatrolEditorActive = false;
							mNameEditorActive = false;
							mClanEditorActive = false;
							thoughtEdit.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Thought List Editing";
						}
						ImGui.EndMenu();
					}
					if(ImGui.BeginMenu("Names"))
					{
						if(ImGui.MenuItem("Select Names File"))
						{
							Editor.Load(ref mNameEditorActive);
							mModCreationMenuActive = false;
							mPatrolEditorActive = false;
							mThoughtEditorActive = false;
							mClanEditorActive = false;
							nameEdit.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Name Editing";
						}
						ImGui.EndMenu();
					}
					if(ImGui.BeginMenu("Clan"))
					{
						if(ImGui.MenuItem("Select Clan File"))
						{
							Editor.Load(ref mClanEditorActive);
							mModCreationMenuActive = false;
							mPatrolEditorActive = false;
							mThoughtEditorActive = false;
							mNameEditorActive = false;
							clanEdit.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Clan Editing";
						}
						ImGui.EndMenu();
					}
					if(mPatrolEditorActive && ImGui.MenuItem("Save Patrol File"))
					{
						patrolEdit.Save();
					}
					if(mThoughtEditorActive && ImGui.MenuItem("Save Thought File"))
					{
						thoughtEdit.Save();
					}
					if(mNameEditorActive && ImGui.MenuItem("Save Name File"))
					{
						nameEdit.Save();
					}
					if(mClanEditorActive && ImGui.MenuItem("Save Clan File"))
					{
						clanEdit.Save();
					}
					if(ImGui.MenuItem("Close"))
					{
						mWindow.Close();
					}
					ImGui.EndMenu();
				}
				ImGui.EndMenuBar();
			}
			ImGui.SetNextWindowSize(new(350, 350));
			if(ImGui.Begin("Config/Settings Editor", ImGuiWindowFlags.MenuBar))
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
						if(ImGui.Button("Select Path"))
						{
							fd.ShowFolderDialog();
							editorConfig.patrolPath = fd.SelectedPath;
						}
						ImGui.Text(editorConfig.thoughtPath + "\n(Thought JSONs Path)");
						if(ImGui.Button(" Select Path "))
						{
							fd.ShowFolderDialog();
							editorConfig.thoughtPath = fd.SelectedPath;
						}
						ImGui.Text(editorConfig.clanPath + "\n(Clan JSON Path)");
						if(ImGui.Button("  Select Path  "))
						{
							fd.ShowFolderDialog();
							editorConfig.clanPath = fd.SelectedPath;
						}
						ImGui.Text(editorConfig.gameConfigPath + "\n(Game Config Path)");
						if(ImGui.Button("    Select Path   "))
						{
							fd.ShowFolderDialog();
							editorConfig.gameConfigPath = fd.SelectedPath;
						}
						ImGui.Text(editorConfig.settingsPath + "\n(Settings JSON Path)");
						if(ImGui.Button("    Select Path    "))
						{
							fd.ShowFolderDialog();
							editorConfig.settingsPath = fd.SelectedPath;
						}
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
			}
		}

		PatrolEditor patrolEdit = new PatrolEditor();
		ThoughtEditor thoughtEdit = new ThoughtEditor();
		NameEditor nameEdit = new NameEditor();
		ClanEditor clanEdit = new ClanEditor();

		public void Render(GL gl, double delta, ImGuiController controller)
		{
			gl.Viewport(mWindow.FramebufferSize);

			gl.ClearColor(.45f, .55f, .60f, 1f);
			gl.Clear((uint)ClearBufferMask.ColorBufferBit);

			Theme.SetImGuiTheme(ImGui.GetStyle());

			ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
			ImGui.DockSpaceOverViewport();

			/*if(Background == null)
			{
				Background = new Texture(gl, "./Resources/Images/Bliss.jpg");
				Background.Bind(TextureUnit.Texture31);
			}*/

			DrawMainMenu();

			/*if(editorConfig.backgroundEnabled)
			{
				ImGui.GetBackgroundDrawList().AddImage(new IntPtr(Background._handle), new Vector2(12, 12), new Vector2(Background.width, Background.height), new Vector2(0, 1), new Vector2(1, 0));
			}*/

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
	}
}
