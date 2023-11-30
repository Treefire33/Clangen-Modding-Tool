using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using System.Numerics;
using Newtonsoft.Json;
using StbImageSharp;
using Silk.NET.SDL;
using System.IO;
using ClanGenModTool.Textures;
using Texture = ClanGenModTool.Textures.Texture;
using ClanGenModTool.ObjectTypes;

namespace ClanGenModTool.UI.SubWindows
{
	public class ThoughtEditor : Editor
	{
		List<Thought> thoughts = new List<Thought>();

		Thought? loadedThought = null;
		int currentThought;

		public void LoadEditor()
		{
			try
			{
				thoughts = JsonConvert.DeserializeObject<List<Thought>>(loadedJson!)!;
			}
			catch(Exception ex)
			{
				ErrorBox.Draw("Invalid JSON!\n" + ex);
			}
		}

		public void AssignToLoadedThought()
		{
			loadedThought = thoughts[currentThought];
		}

		public void Draw(ref bool continueDraw)
		{
			if(loadedJson == null)
			{
				ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.Once);
				ImGui.SetNextWindowPos(new Vector2(0, 19));
				if(ImGui.Begin("Select Thought List First!", ImGuiWindowFlags.Popup))
				{
					if(ImGui.Button("Select"))
					{
						Load();
					}

					ImGui.End();
				}

				return;
			}

			if(continueDraw)
			{
				DrawSelectThought();
			}
		}

		private void DrawSelectThought()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Thought List Select", ImGuiWindowFlags.NoCollapse) && thoughts != null)
			{
				for(int i = 0; i < thoughts.Count; i++)
				{
					try
					{
						if(ImGui.Button(thoughts[i].id))
						{
							currentThought = i;
							AssignToLoadedThought();
						}
						ImGui.SameLine();
						if(ImGui.Button("Del"))
						{
							thoughts.RemoveAt(i);
						}
					}catch(Exception e) {
						ErrorBox.Draw("Not a though json!"+e);
					}
				}
				/*if(ImGui.Button("Add Thought"))
				{
					Thought t = new Thought();
					t.DefaultThought();
					thoughts.Add(t);
				}*/
				ImGui.End();
			}
		}

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Thought Attributes Editor", ImGuiWindowFlags.NoCollapse))
			{
				if(loadedThought != null)
				{

				}
			}
		}

		public void Save()
		{
			if(loadedPath != null && loadedPath != "")
			{
				string newJson = JsonConvert.SerializeObject(thoughts, Formatting.Indented);
				File.WriteAllText(loadedPath, newJson);
			}
		}
	}
}
