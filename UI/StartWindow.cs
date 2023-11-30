﻿using Silk.NET.OpenGL.Extensions.ImGui;
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
using Silk.NET.SDL;

namespace ClanGenModTool.UI
{
	public class StartWindow
	{
		readonly IWindow mWindow;
		bool mPatrolEditorActive = false, mModCreationMenuActive = false, mImplementException = false;
		bool mThoughtEditorActive = false;

		public StartWindow()
		{
			WindowManager.CreateWindow(out mWindow, new(1600, 900));
			mWindow.Load += () => WindowManager.RegisterRenderDelegate(mWindow, Render);
			mWindow.Run();
			mWindow.Dispose();
		}
		
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
							thoughtEdit.LoadEditor();
							mWindow.Title = "ClanGen Modding Tool   -   Thought List Editing";
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
					if(ImGui.MenuItem("Close"))
					{
						mWindow.Close();
					}

					ImGui.EndMenu();
				}
				ImGui.EndMenuBar();
			}
		}

		PatrolEditor patrolEdit = new PatrolEditor();
		ThoughtEditor thoughtEdit = new ThoughtEditor();

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
				patrolEdit.GL = gl;
				patrolEdit.Draw(ref mPatrolEditorActive);
			}
			if(mThoughtEditorActive)
			{
				thoughtEdit.Draw(ref mThoughtEditorActive);
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
