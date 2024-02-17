﻿using ClanGenModTool.ObjectTypes;
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
					if(ImGui.Button($"{listName} | {cat.ID}"))
					{
						currentCat = cat;
						whitePatchesEnabled = currentCat.white_patches != null;
						heterochromiaEnabled = currentCat.eye_colour2 != null;
						vitiligoEnabled = currentCat.vitiligo != null;
						pointsEnabled = currentCat.points != null;
					}
				}
				ImGui.End();
			}
		}

		string toAdd = "1", scarToAdd = "ONE";
		private bool heterochromiaEnabled;
		private bool whitePatchesEnabled;
		private bool vitiligoEnabled;
		private bool pointsEnabled;

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Cat Editor", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.TextColored(new(255, 0, 0, 255), "Certain fields are locked to prevent game breaking bugs from happening.");
				ImGui.Text($"ID: {currentCat.ID}");
				if(ImGui.InputInt("Experience", ref currentCat.experience, 1, 10))
				{
					if(currentCat.experience < 0)
						currentCat.experience = 0;
					else if(currentCat.experience > 321)
						currentCat.experience = 321;
				}
				ImGui.Checkbox("Hide Special Suffix", ref currentCat.specsuffix_hidden);
				if(ImGui.BeginCombo("Gender: ", currentCat.gender))
				{
					foreach(string s in new string[] { "male", "female" })
					{
						bool selected = currentCat.gender.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							currentCat.gender = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(ImGui.BeginCombo("Gender Alignment", currentCat.gender_align))
				{
					foreach(string s in new string[] { "male", "female", "nonbinary" })
					{
						bool selected = currentCat.gender_align.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							currentCat.gender_align = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
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
					currentCat.adoptive_parents ??= new List<string>();
					currentCat.adoptive_parents.Add(toAdd);
				}
				if(ImGui.InputInt("Age (Moons): ", ref currentCat.moons)) { }
				ImGui.Indent(); ImGui.Text($"Dead For: {currentCat.dead_moons} moons"); ImGui.Unindent();
				if(ImGui.InputText("Prefix", ref currentCat.name_prefix, 25)) { }
				if(ImGui.InputText("Suffix", ref currentCat.name_suffix, 25)) { }
				if(ImGui.BeginCombo("Status", currentCat.status))
				{
					foreach(string s in Constants.statuses)
					{
						bool selected = currentCat.status.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							currentCat.status = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(ImGui.BeginCombo("Backstory", currentCat.backstory))
				{
					foreach(string s in Constants.backgrounds)
					{
						bool selected = currentCat.backstory.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							currentCat.backstory = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(currentCat.mentor is not null && currentCat.status.Contains("apprentice"))
				{
					ImGui.Text("Mentor:");
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
				ImGui.Text("Current Facets: " + currentCat.facets);
				if(ImGui.BeginCombo("Trait", currentCat.trait))
				{
					foreach(string s in Constants.traits)
					{
						bool selected = currentCat.trait.Equals(s);
						if(ImGui.Selectable(s, ref selected))
						{
							currentCat.facets = GenerateFacetsFromTrait(currentCat.trait);
						}
						if(selected)
						{
							currentCat.trait = s;
						}
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(ImGui.Checkbox("Dead", ref currentCat.dead)) { }
				if(ImGui.Checkbox("Paralyzed", ref currentCat.paralyzed)) { }
				if(ImGui.Checkbox("No Kits", ref currentCat.no_kits)) { }
				if(ImGui.Checkbox("Exiled", ref currentCat.exiled)) { }
				if(ImGui.CollapsingHeader("Sprites"))
				{
					if(ImGui.BeginCombo("Pelt Pattern", currentCat.pelt_name))
					{
						foreach(string s in Constants.PeltNames)
						{
							bool selected = currentCat.pelt_name.Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.pelt_name = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(currentCat.pelt_name == "Tortie" || currentCat.pelt_name == "Calico")
					{
						if(ImGui.BeginCombo("Tortie Pattern", currentCat.pattern != null ? currentCat.pattern : ""))
						{
							foreach(string s in Constants.TortiePatterns)
							{
								bool selected = currentCat.pattern != null ? currentCat.pattern.Equals(s) : false;
								ImGui.Selectable(s, ref selected);
								if(selected)
									currentCat.pattern = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						if(ImGui.BeginCombo("Tortie Base", currentCat.tortie_base ?? ""))
						{
							foreach(string s in Constants.TortiePeltNames)
							{
								bool selected = currentCat.tortie_base != null && currentCat.tortie_base.Equals(s);
								ImGui.Selectable(s, ref selected);
								if(selected)
									currentCat.tortie_base = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						if(ImGui.BeginCombo("Tortie Colour", currentCat.tortie_color ?? ""))
						{
							foreach(string s in Constants.PeltColours)
							{
								bool selected = currentCat.tortie_color != null && currentCat.tortie_color.Equals(s);
								ImGui.Selectable(s, ref selected);
								if(selected)
									currentCat.tortie_color = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						if(ImGui.BeginCombo("Pattern", currentCat.tortie_pattern != null ? currentCat.tortie_pattern : ""))
						{
							foreach(string s in Constants.TortiePeltNames)
							{
								bool selected = currentCat.tortie_pattern != null && currentCat.tortie_pattern.Equals(s);
								ImGui.Selectable(s, ref selected);
								if(selected)
									currentCat.tortie_pattern = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
					}
					else
					{
						currentCat.pattern = null;
						currentCat.tortie_base = null;
						currentCat.tortie_color = null;
						currentCat.tortie_pattern = null;
					}
					if(ImGui.BeginCombo("Pelt Colour", currentCat.pelt_color))
					{
						foreach(string s in Constants.PeltColours)
						{
							bool selected = currentCat.pelt_color.Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.pelt_color = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(ImGui.BeginCombo("Pelt Length", currentCat.pelt_length))
					{
						foreach(string s in new string[] { "short", "medium", "long" })
						{
							bool selected = currentCat.pelt_length.Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.pelt_length = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(ImGui.SliderInt("Sprite (Kit)", ref currentCat.sprite_kitten, 0, 2)) { }
					if(ImGui.SliderInt("Sprite (Adolescent)", ref currentCat.sprite_adolescent, 3, 5)) { }
					if(ImGui.SliderInt("Sprite (Adult)", ref currentCat.sprite_adult, currentCat.pelt_length == "long" ? 6 : 9, currentCat.pelt_length == "long" ? 8 : 11)) { }
					if(ImGui.SliderInt("Sprite (Senior)", ref currentCat.sprite_senior, 12, 14)) { }
					if(ImGui.SliderInt("Sprite (Paralyzed)", ref currentCat.sprite_para_adult, 15, 17)) { }
					if(ImGui.BeginCombo("Eye Colour 1", currentCat.eye_colour))
					{
						foreach(string s in Constants.EyeColours)
						{
							bool selected = currentCat.eye_colour.Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								currentCat.eye_colour = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					ImGui.Checkbox("Enable Heterochromia", ref heterochromiaEnabled);
					if(heterochromiaEnabled)
					{
						if(ImGui.BeginCombo("Eye Colour 2", currentCat.eye_colour2))
						{
							foreach(string s in Constants.EyeColours)
							{
								bool selected = currentCat.eye_colour2 != null ? currentCat.eye_colour2.Equals(s) : false;
								ImGui.Selectable(s, ref selected);
								if(selected)
									currentCat.eye_colour2 = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
					}
					else
					{
						currentCat.eye_colour2 = null;
					}
					if(ImGui.Checkbox("Flipped Sprite", ref currentCat.reverse)) { }
					ImGui.Checkbox("Have White Patches", ref whitePatchesEnabled);
					if(whitePatchesEnabled)
					{
						ImExtended.Combo("White Patches", ref currentCat.white_patches, Constants.WhitePatches);
					}
					else { currentCat.white_patches = null; }
					ImExtended.Combo("White Patches Tint", ref currentCat.white_patches_tint!, Constants.WhitePatchesTints);
					ImGui.Checkbox("Have Vitiligo", ref vitiligoEnabled);
					if(vitiligoEnabled)
					{
						ImExtended.Combo("Vitiligo", ref currentCat.vitiligo, Constants.Vitiligo);
					}
					else { currentCat.vitiligo = null; }
					ImGui.Checkbox("Have Points", ref pointsEnabled);
					if(pointsEnabled)
					{
						ImExtended.Combo("Points", ref currentCat.points, Constants.PointMarkings);
					}
					else { currentCat.points = null; }
					ImExtended.Combo("Skin Colour", ref currentCat.skin!, Constants.SkinColours);
					ImExtended.Combo("Tint", ref currentCat.tint!, Constants.PeltTints);
					ImExtended.Combo("Accessory", ref currentCat.accessory!, Constants.accessories);
					ImExtended.Combo("Scars", ref scarToAdd!, Constants.scars);
					ImGui.SameLine();
					if(ImGui.Button("Add Selected Scar"))
					{
						currentCat.scars.Add(scarToAdd);
					}
					ImGui.Text("Current Scars:");
					if(currentCat.scars.Count > 0)
						foreach(string scar in currentCat.scars)
						{
							ImGui.Text(scar);
						}
				}
				ImGui.End();
			}
		}

		private string GenerateFacetsFromTrait(string traitName)
		{
			if(!Constants.traits.Contains(traitName))
				return string.Empty;
			int lawful = 0, social = 0, anger = 0, stable = 0;
			Random r = new Random();
			lawful = r.Next(Constants.traitRanges[traitName][0].Item1, Constants.traitRanges[traitName][0].Item2);
			social = r.Next(Constants.traitRanges[traitName][1].Item1, Constants.traitRanges[traitName][1].Item2);
			anger = r.Next(Constants.traitRanges[traitName][2].Item1, Constants.traitRanges[traitName][2].Item2);
			stable = r.Next(Constants.traitRanges[traitName][3].Item1, Constants.traitRanges[traitName][3].Item2);
			return $"{lawful},{social},{anger},{stable}";
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
