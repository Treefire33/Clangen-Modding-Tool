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
using ClanGenModTool.UI;


namespace ClanGenModTool.Mod
{
	public class ModMenus
	{
		public static List<ModItem> mods = new List<ModItem>();
		static string modName = "", modPath = "", modSpritesPath = "", modPatrolsPath = "";
		static string loadedSprPath = "", loadedPtrlPath = "", loadedModPath = "";

		public static void DrawModCreationMenu(ref bool render)
		{
			ImGui.SetNextWindowSize(new Vector2(350, 250), ImGuiCond.Always);
			if(ImGui.Begin("Create Mod", ImGuiWindowFlags.None))
			{
				ImGui.InputText("Mod Name", ref modName, 400);
				if(ImGui.Button("Select Mod Path"))
				{
					var dialog = new FileDialog();
					if(dialog.ShowFolderDialog())
					{
						loadedModPath = dialog.SelectedPath;
					}
				}
				ImGui.Text($"Mod\'s Path:\n{loadedModPath}");
				modPath = loadedModPath; 
				if(ImGui.Button("Select Sprite Path"))
				{
					var dialog = new FileDialog();
					if(dialog.ShowFolderDialog())
					{
						loadedSprPath = dialog.SelectedPath;
					}
				}
				ImGui.Text($"Mod\'s Sprite Path:\n{loadedSprPath}");
				modSpritesPath = loadedSprPath;
				if(ImGui.Button("Select Patrol Path"))
				{
					var dialog = new FileDialog();
					if(dialog.ShowFolderDialog())
					{
						loadedPtrlPath = dialog.SelectedPath;
					}
				}
				ImGui.Text($"Mod\'s Patrol Path:\n{loadedPtrlPath}");
				modPatrolsPath = loadedPtrlPath;
				if(ImGui.Button("Create"))
				{
					mods.Add(new ModItem { Name = modName, SpritesPath = modSpritesPath, PatrolsPath = modPatrolsPath, ModPath = modPath });
					modName = "";
				}
			}
		}

		public static void ToJson(int specificMod)
		{
			string json = JsonConvert.SerializeObject(mods[specificMod], Formatting.Indented);
			File.WriteAllText($"{mods[specificMod].ModPath}/{mods[specificMod].Name}.clangenmod", json);
		}
		public static void ToJson()
		{
			for(int i = 0; i < mods.Count; i++)
			{
				string json = JsonConvert.SerializeObject(mods[i], Formatting.Indented);
				File.WriteAllText($"{mods[i].ModPath}/{mods[i].Name}.clangenmod", json);
			}
		}
	}
}
