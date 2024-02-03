using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using System.Numerics;

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
					string listName = NameFromStatus(cat);
					if(ImGui.Button($"{listName}|{cat.ID}"))
					{
						currentCat = cat;
					}
				}
				ImGui.End();
			}
		}

		string toAdd = "1";
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
					ImGui.PushID(90);
					if(ImGui.BeginCombo("", currentCat.parent1))
					{
						foreach(string s in NameList())
						{
							string fix = catDict.FirstOrDefault(x => NameFromStatus(x.Value) == s).Key;
							bool selected = currentCat.parent1.Equals(fix);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.parent1 = fix;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					ImGui.PopID();
					ImGui.SameLine();
					CatChip(currentCat.parent1);
				}
				else
				{
					if(ImGui.Button("Add Parent"))
					{
						currentCat.parent1 = "1";
					}
				}
				if(currentCat.parent2 != null)
				{
					ImGui.Text($"Parent 2:");
					ImGui.SameLine();
					ImGui.PushID(91);
					if(ImGui.BeginCombo("", currentCat.parent2))
					{
						foreach(string s in NameList())
						{
							string fix = catDict.FirstOrDefault(x => NameFromStatus(x.Value) == s).Key;
							bool selected = currentCat.parent2.Equals(fix);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.parent2 = fix;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					ImGui.PopID();
					ImGui.SameLine();
					CatChip(currentCat.parent2);
				}
				else
				{
					if(ImGui.Button("Add Second Parent"))
					{
						currentCat.parent2 = "1";
					}
				}
				ImGui.Text("Adoptive Parents: ");
				if(currentCat.adoptive_parents != null)
				{
					ImGui.Indent(3);
					for(int i = 0; i < currentCat.adoptive_parents.Count; i++)
					{
						CatChip(currentCat.adoptive_parents[i]);
						ImGui.SameLine();
						ImGui.PushID(currentCat.adoptive_parents[i]);
						if(ImGui.Button("Remove"))
						{
							currentCat.adoptive_parents.Remove(currentCat.adoptive_parents[i]);
						}
						ImGui.PopID();
					}
					ImGui.Unindent();
				}
				ImGui.PushID(92);
				if(ImGui.BeginCombo("", toAdd))
				{
					foreach(string s in NameList())
					{
						string fix = catDict.FirstOrDefault(x => NameFromStatus(x.Value) == s).Key;
						bool selected = toAdd.Equals(fix);
						ImGui.Selectable(s, ref selected);
						if(selected)
							toAdd = fix;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				ImGui.PopID();
				ImGui.SameLine();
				if(ImGui.Button("Add Adoptive Parent"))
				{
					if(currentCat.adoptive_parents == null)
						currentCat.adoptive_parents = new List<string>();
					currentCat.adoptive_parents.Add(toAdd);
				}
				if(ImGui.InputInt("Age (Moons): ", ref currentCat.moons)) { }
				ImGui.Indent(); ImGui.Text($"Dead For: {currentCat.dead_moons} moons"); ImGui.Unindent();
				if(ImGui.InputText("Prefix", ref currentCat.name_prefix, 25)) { }
				if(ImGui.InputText("Suffix", ref currentCat.name_suffix, 25)) { }
				if(ImGui.InputText("Status", ref currentCat.status, 25)) { }
				if(ImGui.BeginCombo("Backstory", currentCat.backstory))
				{
					foreach(string s in Background.backgrounds)
					{
						bool selected = currentCat.backstory.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							currentCat.backstory = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(currentCat.mentor is not null)
				{
					ImGui.Text("Mentor (if apprentice):");
					ImGui.SameLine();
					if(ImGui.BeginCombo("", currentCat.mentor))
					{
						foreach(string s in NameList())
						{
							string fix = catDict.FirstOrDefault(x => NameFromStatus(x.Value) == s).Key;
							bool selected = currentCat.mentor.Equals(fix);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.mentor = fix;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					ImGui.SameLine();
					CatChip(currentCat.mentor);
				}
				if(currentCat.former_mentor is not null)
				{
					ImGui.Text("Former Mentors:");
					foreach(string mentor in currentCat.former_mentor)
					{
						CatChip(mentor);
					}
				}
				ImGui.End();
			}
		}

		private void CatChip(string ID) //Creates a button that relates a Cat's id to the Cat's object.
		{
			if(catDict.ContainsKey(ID))
			{
				Cat c = catDict[ID];
				if(ImGui.Button(NameFromStatus(c)))
				{
					currentCat = c;
				}
			}
		}

		private List<string> NameList()
		{
			List<string> names = new List<string>();
			foreach(Cat cat in catDict.Values)
			{
				string listName = NameFromStatus(cat);
				names.Add(listName);
			}
			return names;
		}

		private string NameFromStatus(Cat cat)
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
			return listName;
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
