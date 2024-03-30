using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using OpenTK.Windowing.Common.Input;
using System.Collections.Generic;
using System.Numerics;
using ClanGenModTool.Util;

// ReSharper disable All

namespace ClanGenModTool.UI.SubWindows;

public class CatEditor : Editor
{
	public List<Cat>? LoadedCats = null;
	private readonly Dictionary<string, Cat> catDict = [];
	private Cat currentCat;
	public static string LoadedCatPath, LoadedCatJson;
	public Dictionary<string, List<Relationship>> CurrentRelationships; // 
	private int currentRelationship = 0;								// 
	public string RelationshipDirectory;                                // 
	public Dictionary<string, Condition> CurrentConditions;				//	
	private int currentCondition = 0;									// 
	public string ConditionsDirectory;									// 

	public void LoadEditor()
	{
		try
		{
			LoadedCats = JsonConvert.DeserializeObject<List<Cat>>(LoadedCatJson!)!;
			currentCat = LoadedCats[0];
			foreach(Cat cat in LoadedCats)
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
		if(LoadedJson == null)
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
			DrawSelectCat();
			DrawAttributesWindow();
			if(CurrentRelationships != null)
			{
				DrawRelationshipEditor();
			}
			if(CurrentConditions != null)
			{
				DrawConditionsEditor();
			}
		}
	}

	private void DrawSelectCat()
	{
		ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
		if(ImGui.Begin("Cat Select", ImGuiWindowFlags.NoCollapse))
		{
			if(ImGui.Button("Add Cat"))
			{
				string neededID = (int.Parse(LoadedCats.Last().ID) + 1).ToString();
				//loadedCats.Add((Cat)loadedCats.Last().Clone());
				LoadedCats.Add(Cat.Default(neededID));
				ClanEditor.LoadedClan.clan_cats += "," + LoadedCats[LoadedCats.Count - 1].ID;
			}
			if(ImGui.Button("Add Random Cat"))
			{
				string neededID = (int.Parse(LoadedCats.Last().ID) + 1).ToString();
				//loadedCats.Add((Cat)loadedCats.Last().Clone());
				LoadedCats.Add(Cat.Random(neededID));
				ClanEditor.LoadedClan.clan_cats += "," + LoadedCats[LoadedCats.Count - 1].ID;
			}
			for(int i = 0; i < LoadedCats.Count; i++)
			{
				string listName = NameFromStatus(LoadedCats[i]);
				if(ImGui.Button($"{listName} | {LoadedCats[i].ID}"))
				{
					currentCat = LoadedCats[i];
					whitePatchesEnabled = currentCat.white_patches != null;
					heterochromiaEnabled = currentCat.eye_colour2 != null;
					vitiligoEnabled = currentCat.vitiligo != null;
					pointsEnabled = currentCat.points != null;
					primarySkill = currentCat.skill_dict.primary.Split(',')[0];
					primaryInterest = int.Parse(currentCat.skill_dict.primary.Split(',')[1]);
					secondarySkill = currentCat.skill_dict.secondary != null ? currentCat.skill_dict.secondary.Split(',')[0] : "HUNTER";
					secondaryInterest = currentCat.skill_dict.secondary != null ? int.Parse(currentCat.skill_dict.secondary.Split(',')[1]) : 1;
					hiddenSkill = currentCat.skill_dict.hidden != null ? currentCat.skill_dict.hidden.Split(',')[0] : "HUNTER";
					hiddenInterest = currentCat.skill_dict.hidden != null ? int.Parse(currentCat.skill_dict.hidden.Split(',')[1]) : 1;
				}
				ImGui.SameLine();
				ImGui.PushID(listName + LoadedCats[i].ID);
				if(ImGui.Button("Del"))
				{
					ClanEditor.LoadedClan.clan_cats = ClanEditor.LoadedClan.clan_cats.Replace("," + LoadedCats[i].ID, null);
					LoadedCats.Remove(LoadedCats[i]);
				}
				ImGui.PopID();
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
			ImGui.TextColored(new(255, 0, 0, 255), "Edit at your own risk!");
			ImGui.Text($"ID: {currentCat.ID}");
			if(ImGui.SliderInt("Experience", ref currentCat.experience, 1, 321))
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
			if(ImGui.CollapsingHeader("Parents"))
			{
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
					if(ImGui.Button("Remove Parent"))
					{
						currentCat.parent1 = null;
					}
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
					if(ImGui.Button("Remove Parent 2"))
					{
						currentCat.parent2 = null;
					}
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
			}
			if(ImGui.InputInt("Age (Moons): ", ref currentCat.moons)) { }
			ImGui.Indent(); ImGui.Text($"Dead For: {currentCat.dead_moons} moons"); ImGui.Unindent();
			if(ImGui.InputText("Prefix", ref currentCat.name_prefix, 25)) { }
			if(ImGui.InputText("Suffix", ref currentCat.name_suffix, 25)) { }
			if(ImGui.BeginCombo("Status", currentCat.status))
			{
				foreach(string s in Constants.Statuses)
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
				foreach(string s in Constants.Backgrounds)
				{
					bool selected = currentCat.backstory.Equals(s);
					ImGui.Selectable(s, ref selected);
					if(selected)
						currentCat.backstory = s;
					ImGui.SetItemDefaultFocus();
				}
				ImGui.EndCombo();
			}
			if(ImGui.CollapsingHeader("Mentors"))
			{
				if(currentCat.mentor is not null && currentCat.status.Contains("apprentice"))
				{
					ImGui.Text("Mentor:");
					ImGui.SameLine();
					ImGui.PushID("mentor_combo");
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
					ImGui.PopID();
					ImGui.SameLine();
					CatChip(currentCat.mentor);
				}
				else if(currentCat.status.Contains("apprentice"))
				{
					if(ImGui.Button("Add Mentor"))
					{
						currentCat.mentor = LoadedCats[0].ID;
					}
				}
				else
				{
					ImGui.Text("Cat needs to be apprentice to have mentor");
				}
				CatListSelect(["Former Mentors: ", "Add Former Mentor"], ref currentCat.former_mentor, "former_mentor_combo");
			}
			if(ImGui.CollapsingHeader("Mates"))
			{
				CatListSelect(["Mates: ", "Add Mate"], ref currentCat.mate, "mate_combo");
				CatListSelect(["Previous Mates: ", "Add Prev Mate"], ref currentCat.previous_mates, "former_mate_combo");
			}
			if(ImGui.CollapsingHeader("Apprentices"))
			{
				CatListSelect(["Current Apprentices: ", "Add Apprentice"], ref currentCat.current_apprentice, "appr_combo");
				CatListSelect(["Previous Apprentices: ", "Add Prev Apprentice"], ref currentCat.former_apprentices, "prev_appr_combo");
			}
			ImGui.Text("Current Facets: " + currentCat.facets);
			if(ImGui.BeginCombo("Trait", currentCat.trait))
			{
				foreach(string s in Constants.Traits)
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
			if(ImGui.Checkbox("Dark Forest", ref currentCat.df)) { }
			if(ImGui.Checkbox("Outside", ref currentCat.outside)) { }
			if(ImGui.Checkbox("Prevent Fading", ref currentCat.prevent_fading)) { }
			if(ImGui.Checkbox("Favourite", ref currentCat.favourite)) { }
			if(ImGui.SliderInt("Opacity", ref currentCat.opacity, 0, 100)) { }
			if(ImGui.CollapsingHeader("Sprites"))
				DrawCatSpriteEditor();
			if(ImGui.CollapsingHeader("Skill Editing"))
				DrawSkillDictEditor();
			ImGui.End();
		}
	}

	string primarySkill, secondarySkill, hiddenSkill;
	int primaryInterest, secondaryInterest, hiddenInterest;
	private void DrawSkillDictEditor()
	{
		if(ImGui.BeginTabBar("skillDict"))
		{
			if(ImGui.BeginTabItem("Primary"))
			{
				ImExtended.Combo("Skill", ref primarySkill!, Constants.Skills, 2095);
				ImGui.PushID(2096);
				ImGui.SliderInt("Interest", ref primaryInterest!, 1, 29);
				ImGui.PopID();
				currentCat.skill_dict.primary = $"{primarySkill},{primaryInterest},{currentCat.status.Contains("apprentice") || currentCat.status.Contains("kit")}";
				ImGui.Text("Primary: " + currentCat.skill_dict.primary);
				ImGui.EndTabItem();
			}
			if(ImGui.BeginTabItem("Secondary"))
			{
				if(currentCat.skill_dict.secondary != null)
				{
					ImExtended.Combo("Skill", ref secondarySkill!, Constants.Skills, 2097);
					ImGui.PushID(2098);
					ImGui.SliderInt("Interest", ref secondaryInterest!, 1, 29);
					ImGui.PopID();
					currentCat.skill_dict.secondary = $"{secondarySkill},{secondaryInterest},{currentCat.status.Contains("apprentice") || currentCat.status.Contains("kit")}";
					ImGui.Text("Secondary: " + currentCat.skill_dict.secondary);
					ImGui.EndTabItem();
				}
				else
				{
					if(ImGui.Button("Add Secondary Skill"))
					{
						currentCat.skill_dict.secondary = "HUNTER,1,False";
					}
				}
			}
			if(ImGui.BeginTabItem("Hidden"))
			{
				if(currentCat.skill_dict.hidden != null)
				{
					ImExtended.Combo("Skill", ref hiddenSkill!, Constants.Skills, 2097);
					ImGui.PushID(2098);
					ImGui.SliderInt("Interest", ref hiddenInterest!, 1, 29);
					ImGui.PopID();
					currentCat.skill_dict.hidden = $"{hiddenSkill},{hiddenInterest},{currentCat.status.Contains("apprentice") || currentCat.status.Contains("kit")}";
					ImGui.Text("Secondary: " + currentCat.skill_dict.hidden);
					ImGui.EndTabItem();
				}
				else
				{
					if(ImGui.Button("Add Hidden Skill"))
					{
						currentCat.skill_dict.hidden = "HUNTER,1,False";
					}
				}
			}
			ImGui.EndTabBar();
		}
	}
	private void DrawCatSpriteEditor()
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
		ImExtended.Combo("Accessory", ref currentCat.accessory!, Constants.Accessories);
		ImExtended.Combo("Scars", ref scarToAdd!, Constants.Scars);
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

	private void DrawRelationshipEditor()
	{
		if(ImGui.Begin("Cat Relationships", ImGuiWindowFlags.None))
		{
			try
			{
				if (ImGui.SliderInt("Select Relationship", ref currentRelationship, 0, CurrentRelationships.Count - 1))
				{
				}

				ImGui.Text("Cat To: " + CurrentRelationships[currentCat.ID][currentRelationship].cat_to_id);
				if (ImGui.Checkbox("Mates", ref CurrentRelationships[currentCat.ID][currentRelationship].mates))
				{
					if (CurrentRelationships[currentCat.ID][currentRelationship].family)
					{
						CurrentRelationships[currentCat.ID][currentRelationship].mates = false;
					}
				}

				if (ImGui.Checkbox("Family", ref CurrentRelationships[currentCat.ID][currentRelationship].family))
				{
					if (CurrentRelationships[currentCat.ID][currentRelationship].mates)
					{
						CurrentRelationships[currentCat.ID][currentRelationship].family = false;
					}
				}

				if (!CurrentRelationships[currentCat.ID][currentRelationship].family && ImGui.SliderInt("Romantic Love",
					    ref CurrentRelationships[currentCat.ID][currentRelationship].romantic_love, 0, 100))
				{
				}

				if (ImGui.SliderInt("Platonic Love",
					    ref CurrentRelationships[currentCat.ID][currentRelationship].platonic_like, 0, 100))
				{
				}

				if (ImGui.SliderInt("Dislike", ref CurrentRelationships[currentCat.ID][currentRelationship].dislike, 0,
					    100))
				{
				}

				if (ImGui.SliderInt("Admiration",
					    ref CurrentRelationships[currentCat.ID][currentRelationship].admiration, 0, 100))
				{
				}

				if (ImGui.SliderInt("Comfort in Other",
					    ref CurrentRelationships[currentCat.ID][currentRelationship].comfortable, 0, 100))
				{
				}

				if (ImGui.SliderInt("Jealousy", ref CurrentRelationships[currentCat.ID][currentRelationship].jealousy,
					    0, 100))
				{
				}

				if (ImGui.SliderInt("Trust", ref CurrentRelationships[currentCat.ID][currentRelationship].trust, 0,
					    100))
				{
				}
			}
			catch
			{
				ImGui.Text("Cat is missing relationship file.");
			}
		}
	}

	private string illnessToAdd = "greencough", injuryToAdd = "claw-wound", permToAdd = "crooked jaw";
	private void DrawConditionsEditor()
	{
		if(ImGui.Begin("Cat Conditions", ImGuiWindowFlags.None))
		{
			try
			{
				if (ImGui.BeginTabBar("Condition"))
				{
					if (ImGui.BeginTabItem("Illnesses"))
					{
						//what if I hid an easter egg in the code?
						//(this is the easter egg, ping me on the ClanGen discord and say "I found the thing").
						//(what do you get? the feeling of accomplishment.)
						if (CurrentConditions[currentCat.ID].illnesses != null)
						{
							foreach (KeyValuePair<string, Illness> illness in
							         CurrentConditions[currentCat.ID].illnesses)
							{
								ImGui.Text(illness.Key);
								ImGui.SameLine();
								ImGui.PushID(illness.Key);
								if (ImGui.Button("Remove"))
								{
									CurrentConditions[currentCat.ID].illnesses.Remove(illness.Key);
									continue;
								}

								ImGui.PopID();
							}
						}
						ImGui.Separator();
						ImExtended.Combo("Illnesses", ref illnessToAdd, PresetIllnesses.IllnessPresetDict.Keys.ToArray());
						if(ImGui.Button("Add Illness"))
						{
							if (CurrentConditions[currentCat.ID].illnesses == null)
							{
								CurrentConditions[currentCat.ID].illnesses = new();
							}
							CurrentConditions[currentCat.ID].illnesses.Add(illnessToAdd, PresetIllnesses.IllnessPresetDict[illnessToAdd]);
						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Injuries"))
					{
						if (CurrentConditions[currentCat.ID].injuries != null)
						{
							foreach (KeyValuePair<string, Injury> injuries in CurrentConditions[currentCat.ID].injuries)
							{
								ImGui.Text(injuries.Key);
								ImGui.SameLine();
								ImGui.PushID(injuries.Key);
								if (ImGui.Button("Remove"))
								{
									CurrentConditions[currentCat.ID].injuries.Remove(injuries.Key);
									continue;
								}

								ImGui.PopID();
							}
						}
						ImGui.Separator();
						ImExtended.Combo("Injuries", ref injuryToAdd,
							PresetInjuries.InjuryPresetDict.Keys.ToArray());
						if(ImGui.Button("Add Injuries"))
						{
							if (CurrentConditions[currentCat.ID].injuries == null)
							{
								CurrentConditions[currentCat.ID].injuries = new();
							}
							CurrentConditions[currentCat.ID].injuries.Add(injuryToAdd,
								PresetInjuries.InjuryPresetDict[injuryToAdd]);
						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Perm Conditions"))
					{
						if(CurrentConditions[currentCat.ID].permanentConditions != null)
						{
							foreach(KeyValuePair<string, PermCondition> conditions in CurrentConditions[currentCat.ID].permanentConditions)
							{
								ImGui.Text(conditions.Key);
								ImGui.SameLine();
								ImGui.PushID(conditions.Key);
								if(ImGui.Button("Remove"))
								{
									CurrentConditions[currentCat.ID].permanentConditions.Remove(conditions.Key);
									continue;
								}

								ImGui.PopID();
							}
						}
						ImGui.Separator();
						ImExtended.Combo("Perm Conditions", ref permToAdd,
							PresetPermConditions.PermPresetDict.Keys.ToArray());
						if(ImGui.Button("Add Perm Condition"))
						{
							if(CurrentConditions[currentCat.ID].permanentConditions == null)
							{
								CurrentConditions[currentCat.ID].permanentConditions = new();
							}
							CurrentConditions[currentCat.ID].permanentConditions.Add(injuryToAdd,
								PresetPermConditions.PermPresetDict[permToAdd]);
						}
						ImGui.EndTabItem();
					}
					ImGui.EndTabBar();
				}
			}
			catch
			{
				ImGui.Text("Cat does not have conditions file.\n(in other words, your cat is perfectly healthy!)");
				if (ImGui.Button("Create Conditions File for Cat"))
				{
					CurrentConditions.Add(currentCat.ID, new Condition());
				}
			}
		}
	}

	private void CatListSelect(string[] titles, ref List<string> list, string id)
	{
		ImGui.Text(titles[0]);
		if(list != null)
		{
			ImGui.Indent(3);
			for(int i = 0; i < list.Count; i++)
			{
				CatChip(list[i]);
				ImGui.SameLine();
				ImGui.PushID(list[i]);
				if(ImGui.Button("Remove"))
				{
					list.Remove(list[i]);
				}
				ImGui.PopID();
			}
			ImGui.Unindent();
		}
		ImGui.PushID(id);
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
		if(ImGui.Button(titles[1]))
		{
			list ??= new List<string>();
			list.Add(toAdd);
		}
	}

	private string GenerateFacetsFromTrait(string traitName)
	{
		if(!Constants.Traits.Contains(traitName))
			return string.Empty;
		int lawful = 0, social = 0, anger = 0, stable = 0;
		Random r = new Random();
		lawful = r.Next(Constants.TraitRanges[traitName][0].Item1, Constants.TraitRanges[traitName][0].Item2);
		social = r.Next(Constants.TraitRanges[traitName][1].Item1, Constants.TraitRanges[traitName][1].Item2);
		anger = r.Next(Constants.TraitRanges[traitName][2].Item1, Constants.TraitRanges[traitName][2].Item2);
		stable = r.Next(Constants.TraitRanges[traitName][3].Item1, Constants.TraitRanges[traitName][3].Item2);
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

	public new static void Load()
	{
		LoadedCatPath = "";
		if(Dialog.ShowDialog("Select Json", "json"))
		{
			LoadedCatPath = Dialog.SelectedPath;
		}
		if(LoadedCatPath != null && LoadedCatPath != "")
			LoadedCatJson = File.ReadAllText(LoadedCatPath);
		else
			LoadedCatJson = "";
	}

	public static void Load(string forcedPath)
	{
		LoadedCatPath = forcedPath;
		if(LoadedCatPath != null && LoadedCatPath != "")
			LoadedCatJson = File.ReadAllText(LoadedCatPath);
		else
			LoadedCatJson = "";
	}

	public void Save()
	{
		if (LoadedCatPath != null && LoadedCatPath != "")
		{
			string newJson = JsonConvert.SerializeObject(LoadedCats, Formatting.Indented);
			File.WriteAllText(LoadedCatPath, newJson);
		}
		if(RelationshipDirectory != "")
		{
			foreach (List<Relationship> rel in CurrentRelationships.Values)
			{
				string newJson = JsonConvert.SerializeObject(rel, Formatting.Indented);
				File.WriteAllText(RelationshipDirectory + "\\" + CurrentRelationships.KeyByValue(rel) + "_relations.json", newJson);
			}
		}
		if(ConditionsDirectory != "")
		{
			foreach(Condition rel in CurrentConditions.Values)
			{
				string newJson = JsonConvert.SerializeObject(rel, Formatting.Indented);
				File.WriteAllText(ConditionsDirectory + "\\" + CurrentConditions.KeyByValue(rel) + "_conditions.json", newJson);
			}
		}
	}
}