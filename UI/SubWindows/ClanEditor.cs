using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using System.Numerics;

namespace ClanGenModTool.UI.SubWindows
{
	public class ClanEditor : Editor
	{
		public static Clan loadedClan = null;
		bool catEditorOpened = false;
		public void LoadEditor()
		{
			try
			{
				loadedClan = JsonConvert.DeserializeObject<Clan>(loadedJson!)!;
			}
			catch(Exception ex)
			{
				ErrorBox.Draw("Invalid JSON!\n" + ex);
			}
		}

		public void BeforeDrawEditor()
		{
			
		}

		public CatEditor catEditor = new CatEditor();

		public void Draw(ref bool continueDraw)
		{
			if(loadedJson == null)
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
				//DrawSelectThought();
				DrawAttributesWindow();
			}
			if(catEditorOpened && catEditor.loadedCats != null)
			{
				catEditor.Draw(ref catEditorOpened);
				CatEditor.openedThroughClanEditor = catEditorOpened;
			}
			else if(catEditorOpened)
			{
				CatEditor.Load();
				catEditor.LoadEditor();
				CatEditor.openedThroughClanEditor = catEditorOpened;
			}
		}

		private void DrawSelectThought()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Example Select", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.End();
			}
		}

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Clan Attributes Editor", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.TextColored(new(255, 0, 0, 255), "Edit at your own risk!");
				if(ImGui.BeginTabBar("clanEdit"))
				{
					if(ImGui.BeginTabItem("General"))
					{
						ImGui.InputText("Clan Name: ", ref loadedClan.clanname, 32);
						ImGui.InputInt("Clan Age: ", ref loadedClan.clanage, 1, 1);
						ImGui.InputText("Clan Biome: ", ref loadedClan.biome, 32);
						ImGui.InputText("Camp Background: ", ref loadedClan.camp_bg, 32);
						ImGui.InputText("Game Mode: ", ref loadedClan.gamemode, 32);
						ImGui.InputText("Instructor: ", ref loadedClan.instructor, 32);
						ImGui.InputInt("Reputation: ", ref loadedClan.reputation, 1, 1);
						ImGui.InputText("Starting Season: ", ref loadedClan.starting_season, 32);
						ImGui.InputText("Temperament: ", ref loadedClan.temperament, 32);
					}
				}
				ImGui.TextWrapped("Clan Cats: " + loadedClan.clan_cats);
				if(ImGui.Checkbox("Toggle Cat Editor", ref catEditorOpened)) {}
				ImGui.End();
			}
		}

		public void Save()
		{
			if(loadedPath != null && loadedPath != "")
			{
				string newJson = JsonConvert.SerializeObject(loadedClan, Formatting.Indented);
				File.WriteAllText(loadedPath, newJson);
			}
			catEditor.Save();
		}
	}
}
