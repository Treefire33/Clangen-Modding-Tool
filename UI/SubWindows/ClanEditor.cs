using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using System.Numerics;

namespace ClanGenModTool.UI.SubWindows;

public class ClanEditor : Editor
{
	public static Clan LoadedClan;
	private bool catEditorOpened = false;
	public void LoadEditor()
	{
		try
		{
			LoadedClan = JsonConvert.DeserializeObject<Clan>(LoadedJson!)!;
		}
		catch(Exception ex)
		{
			ErrorBox.Draw("Invalid JSON!\n" + ex);
		}
	}

	public CatEditor CatEditor = new();

	public void Draw(ref bool continueDraw)
	{
		if(LoadedJson == null)
		{
			ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.Once);
			ImGui.SetNextWindowPos(new Vector2(0, 19));
			if(ImGui.Begin("Select Clan JSON First!", ImGuiWindowFlags.Popup))
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
			DrawAttributesWindow();
		}
		switch (catEditorOpened)
		{
			case true when CatEditor.LoadedCats != null:
			{
				CatEditor.Draw(ref catEditorOpened);
				CatEditor.OpenedThroughClanEditor = catEditorOpened;
				break;
			}
			case true:
			{
				if(File.Exists(LoadedPath.Replace(LoadedPath.Split('\\').Last(), null) + LoadedClan.clanname + "\\" + "clan_cats.json"))
				{
					
					CatEditor.Load(LoadedPath.Replace(LoadedPath.Split('\\').Last(), null) + LoadedClan.clanname + "\\" + "clan_cats.json");
				}
				else
				{
					CatEditor.Load();
				}
				CatEditor.LoadEditor();
				CatEditor.OpenedThroughClanEditor = catEditorOpened;
				break;
			}
		}
	}

	private void DrawAttributesWindow()
	{
		ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
		if(ImGui.Begin("Clan Attributes Editor", ImGuiWindowFlags.NoCollapse))
		{
			ImGui.TextColored(new Vector4(255, 0, 0, 255), "Edit at your own risk!");
			if(ImGui.BeginTabBar("clanEdit"))
			{
				if(ImGui.BeginTabItem("General"))
				{
					ImGui.InputText("Clan Name: ", ref LoadedClan.clanname, 32);
					ImGui.InputInt("Clan Age: ", ref LoadedClan.clanage, 1, 1);
					ImGui.InputText("Clan Biome: ", ref LoadedClan.biome, 32);
					ImGui.InputText("Camp Background: ", ref LoadedClan.camp_bg, 32);
					ImGui.InputText("Game Mode: ", ref LoadedClan.gamemode, 32);
					ImGui.InputText("Instructor: ", ref LoadedClan.instructor, 32);
					ImGui.InputInt("Reputation: ", ref LoadedClan.reputation, 1, 1);
					ImGui.InputText("Starting Season: ", ref LoadedClan.starting_season, 32);
					ImGui.InputText("Temperament: ", ref LoadedClan.temperament, 32);
				}
			}
			ImGui.TextWrapped("Clan Cats: " + LoadedClan.clan_cats);
			if(ImGui.Checkbox("Toggle Cat Editor", ref catEditorOpened)) {}
			ImGui.End();
		}
	}

	public void Save()
	{
		if(!string.IsNullOrEmpty(LoadedPath))
		{
			string newJson = JsonConvert.SerializeObject(LoadedClan, Formatting.Indented);
			File.WriteAllText(LoadedPath, newJson);
		}
		CatEditor.Save();
	}
}