using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace ClanGenModTool.UI.SubWindows
{
	public class CatEditor : Editor
	{
		public List<Cat> loadedCats = null;
		Dictionary<string, Cat> catDict = new Dictionary<string, Cat>();
		Cat currentCat = null;
		public static string loadedCatPath, loadedCatJson;

		public void LoadEditor()
		{
			try
			{
				loadedCats = JsonConvert.DeserializeObject<List<Cat>>(loadedJson!)!;
				currentCat = loadedCats[0];
				foreach(Cat cat in loadedCats)
				{
					catDict.Add(cat.ID, cat);
				}
			}
			catch(Exception ex)
			{
				ErrorBox.Draw("Invalid JSON!\n" + ex);
			}
		}

		public void BeforeDrawEditor(GL gl)
		{
			throw new NotImplementedException();
		}

		public void Draw(ref bool continueDraw)
		{
			if(loadedJson == null)
			{
				ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.Once);
				ImGui.SetNextWindowPos(new Vector2(0, 19));
				if(ImGui.Begin("Select Cat JSON First!", ImGuiWindowFlags.Popup))
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
				DrawAttributesWindow();
			}
		}

		private void DrawSelectThought()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Cat Select", ImGuiWindowFlags.NoCollapse))
			{
				foreach(Cat cat in loadedCats)
				{
					string listName = "";
					switch(cat.status)
					{
						case "newborn":
						case "kitten":
							listName = cat.name_prefix + "kit";
						break;
						case "medicine cat apprentice":
						case "mediator apprentice":
						case "apprentice":
							listName = cat.name_prefix + "paw";
						break;
						case "elder":
						case "deputy":
						case "medicine cat":
						case "mediator":
						case "warrior":
						case "former Clancat":
							listName = cat.name_prefix + cat.name_suffix;
						break;
						case "leader":
							listName = cat.name_prefix + "star";
						break;
						case "kittypet":
						case "loner":
						case "rogue":
							listName = cat.name_prefix;
						break;
						default:
							listName = "Invalid Status!";
						break;
					}
					if(ImGui.Button($"{listName}| {cat.ID}"))
					{
						currentCat = cat;
					}
				}
				ImGui.End();
			}
		}

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Cat Editor", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.TextColored(new(255, 0, 0, 255), "Certain fields are locked to prevent game breaking bugs from happening.");
				ImGui.Text($"ID: {currentCat.ID}");
				ImGui.Checkbox("Hide Special Suffix", ref currentCat.specsuffix_hidden);
				if(currentCat.parent1 != null)
				{
					ImGui.Text($"Parent 1: ");
					ImGui.SameLine(); 
					CatChip(currentCat.parent1);
				}
				else
					ImGui.Text($"No Parent");
				if(currentCat.parent2 != null)
				{
					ImGui.Text($"Parent 2: "); 
					ImGui.SameLine(); 
					CatChip(currentCat.parent2);
				}
				ImGui.Text("Adoptive Parents: ");
				if(currentCat.adoptive_parents != null)
				{
					ImGui.Indent(3);
					foreach(string catId in currentCat.adoptive_parents)
					{
						CatChip(catId);
					}
					ImGui.Unindent();
				}
				if(ImGui.InputInt("Age (Moons): ", ref currentCat.moons)) { }
				ImGui.Indent(); ImGui.Text($"Dead For: {currentCat.dead_moons} moons");
				if(ImGui.InputText("Prefix", ref currentCat.name_prefix, 25)) { }
				if(ImGui.InputText("Suffix", ref currentCat.name_suffix, 25)) { }
				if(ImGui.InputText("Status", ref currentCat.status, 25)) { }
				ImGui.End();
			}
		}

		private void CatChip(string ID) //Creates a button that relates a Cat's id to the Cat's object.
		{
			if(catDict.ContainsKey(ID))
			{
				Cat c = catDict[ID];
				if(ImGui.Button($"{c.name_prefix}{c.name_suffix}"))
				{
					currentCat = c;
				}
			}
		}

		public static void Load()
		{
			loadedCatPath = "";
			var dialog = new FileDialog();
			if(dialog.ShowDialog("Select Json", "json"))
			{
				loadedCatPath = dialog.SelectedPath;
			}
			if(loadedCatPath != null && loadedCatPath != "")
				loadedJson = File.ReadAllText(loadedCatPath);
			else
				loadedCatJson = null;
		}

		public void Save()
		{
			if(loadedCatPath != null && loadedCatPath != "")
			{
				string newJson = JsonConvert.SerializeObject(loadedCats, Formatting.Indented);
				File.WriteAllText(loadedCatPath, newJson);
			}
		}
	}
}
