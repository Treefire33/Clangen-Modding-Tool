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
using ClanGenModTool.ObjectTypes;
using Texture = ClanGenModTool.Textures.Texture;
using System.Linq.Expressions;

namespace ClanGenModTool.UI.SubWindows
{
	public class PatrolEditor : Editor
	{
		public GL? GL;
		Patrol? loadedPatrol;
		List<Patrol> patrols = new List<Patrol>();
		int currentPatrol = 0;

		public static bool fileEdited = false;
		public Texture patrolPreviewImg;

		public void LoadEditor()
		{
			try
			{
				patrols = JsonConvert.DeserializeObject<List<Patrol>>(loadedJson!)!;
			}
			catch(Exception ex)
			{
				ErrorBox.Draw("Invalid JSON!\n" + ex);
			}
		}

		public void AssignToLoadedPatrol()
		{
			loadedPatrol = patrols[currentPatrol];
		}

		public void BeforeDrawEditor(GL gl)
		{
			if(GL == null)
			{
				GL = gl;
				patrolPreviewImg = new Texture(GL, "./Resources/Images/patrolPreview.png");
				patrolPreviewImg.Bind(TextureUnit.Texture0);
			}
		}

		public void Draw(ref bool continueDraw)
		{
			if(loadedJson == null)
			{
				ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.Once);
				ImGui.SetNextWindowPos(new Vector2(0,19));
				if(ImGui.Begin("Select Patrol First!", ImGuiWindowFlags.Popup))
				{
					if(ImGui.Button("Select"))
					{
						Load();
					}

					ImGui.End();
				}

				return;
			}

			if (continueDraw) 
			{
				DrawAttributesWindow();
				DrawPreviewWindow();
				DrawSelectWindow(patrols);
			}
		}

		string previewedText = "";
		private void DrawPreviewWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(615, 600), ImGuiCond.Once);
			if(ImGui.Begin("Patrol Preview", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.Image(new IntPtr(patrolPreviewImg._handle), new(600, 500));
				ImGui.SetCursorPos(new(300, 155));
				ImGui.BeginChildFrame(3, new(250, 160), ImGuiWindowFlags.NoBackground);
				ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.NavHighlight]);
				ImGui.PushTextWrapPos(225);
				ImGui.TextWrapped(previewedText);
				ImGui.PopTextWrapPos();
				ImGui.PopStyleColor();
				ImGui.EndChildFrame();
				ImGui.End();
			}
			ImGui.SetNextWindowSize(new(250, 300));
			if(ImGui.Begin("Preview Control", ImGuiWindowFlags.None))
			{
				if(loadedPatrol != null)
				{
					if(ImGui.Button("Start Patrol"))
					{
						previewedText = loadedPatrol.intro_text;
					}
					if(ImGui.Button("Decline Patrol"))
					{
						previewedText = loadedPatrol.decline_text;
					}
					if(ImGui.InputInt("Select Outcome", ref selectedOutcome)) { if(selectedOutcome < 0) { selectedOutcome = 0;  } }
					ImGui.SeparatorText("Advance Patrol (Default)");
					try
					{
						if(loadedPatrol.success_outcomes[selectedOutcome] != null && ImGui.Button("Advance Patrol (Success)"))
						{
							previewedText = loadedPatrol.success_outcomes[selectedOutcome].text;
						}
					}
					catch { }
					try
					{
						if(loadedPatrol.fail_outcomes[selectedOutcome] != null && ImGui.Button("Advance Patrol (Fail)"))
						{
							previewedText = loadedPatrol.fail_outcomes[selectedOutcome].text;
						}
					}
					catch { }
					ImGui.SeparatorText("Advance Patrol (Antagonize)");
					try
					{
						if(loadedPatrol.antag_success_outcomes != null && ImGui.Button("Antagonize Patrol (Success)"))
						{
							previewedText = loadedPatrol.antag_success_outcomes[selectedOutcome].text; ;
						}
						if(loadedPatrol.antag_fail_outcomes != null && ImGui.Button("Antagonize Patrol (Fail)"))
						{
							previewedText = loadedPatrol.antag_fail_outcomes[selectedOutcome].text;
						}
					}
					catch { }
				}
			}
		}

		private void DrawSelectWindow(List<Patrol> patrols)
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Patrol Select", ImGuiWindowFlags.NoCollapse) && patrols != null)
			{
				for(int i = 0; i < patrols.Count; i++)
				{
					if(ImGui.Button(patrols[i].patrol_id))
					{
						currentPatrol = i;
						AssignToLoadedPatrol();
					}
					ImGui.SameLine();
					ImGui.PushID(i);
					if(ImGui.Button("Del"))
					{
						patrols.RemoveAt(i);
					}
					ImGui.PopID();
				}
				if(ImGui.Button("Add Patrol"))
				{
					Patrol p = new Patrol();
					p.DefaultPatrol();
					patrols.Add(p);
				}
				ImGui.End();
			}
		}

		private int selectedTag = 0;
		string tempRelConstraint = ""; int tempRelVal = 0;
		string tempSkillConstraint = "";
		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(650, 800), ImGuiCond.Once);
			if(ImGui.Begin("Patrol Attributes Editor", ImGuiWindowFlags.NoCollapse))
			{
				if(loadedPatrol != null)
				{
					ImGui.TextColored(new(255, 0, 0, 255), "This editor is not complete!");
					ImGui.InputText("Patrol ID", ref loadedPatrol.patrol_id, 400);
					if(ImGui.BeginCombo("Biome", loadedPatrol.biome[0]))
					{
						foreach(string s in new string[] { "forest", "plains", "mountainous", "beach", "Any" })
						{
							bool selected = loadedPatrol.biome[0].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								loadedPatrol.biome[0] = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(ImGui.BeginCombo("Season", loadedPatrol.season[0]))
					{
						foreach(string s in new string[] { "greenleaf", "leaf-bare", "leaf-fall", "newleaf", "Any" })
						{
							bool selected = loadedPatrol.season[0].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								loadedPatrol.season[0] = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(ImGui.BeginCombo("Types", loadedPatrol.types[0]))
					{
						foreach(string s in new string[] { "border", "hunting", "herb-gathering", "training" })
						{
							bool selected = loadedPatrol.types[0].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								loadedPatrol.types[0] = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					LoadTagEditor();
					if(ImGui.InputText("Patrol Art", ref loadedPatrol.patrol_art, 400)) { }
					if(loadedPatrol.patrol_art_clean != null && ImGui.InputText("Patrol Art (Clean)", ref loadedPatrol.patrol_art_clean, 400)) { }
					if(ImGui.InputInt("Minimum Cats in Patrol", ref loadedPatrol.min_cats, 1)) { }
					if(ImGui.InputInt("Maximum Cats in Patrol", ref loadedPatrol.max_cats, 1)) { }
					if(loadedPatrol.min_max_status != null) { LoadMinMaxStatusEditor(); }
					else { if(ImGui.Button("Add Min-Max Status")) { loadedPatrol.min_max_status = new MinMaxStatus(); } }
					if(ImGui.InputInt("Patrol Weight", ref loadedPatrol.weight, 1)) { }
					if(ImGui.InputInt("Chance of Success", ref loadedPatrol.chance_of_success, 1)) { }
					LoadRelationshipConstraintEditor();
					if(ImGui.InputTextMultiline("Intro Text", ref loadedPatrol.intro_text, 2400, new(350, 150))) { }
					if(ImGui.InputTextMultiline("Decline Text", ref loadedPatrol.decline_text, 2400, new(350, 150))) { }
					DrawOutcomeEditor();
					ImGui.End();
				}
			}
		}

		private void LoadTagEditor()
		{
			if(ImGui.CollapsingHeader("Tag Editing"))
			{
				if(loadedPatrol.tags.Count > 0)
				{
					if(ImGui.InputInt("Selected Tag", ref selectedTag, 1, 1, ImGuiInputTextFlags.None))
					{
						if(selectedTag >= loadedPatrol.tags.Count)
							selectedTag = loadedPatrol.tags.Count - 1;
						else if(selectedTag < 0)
							selectedTag = 0;
					}
					if(ImGui.BeginCombo("Tags", loadedPatrol.tags[0]))
					{
						foreach(string s in new string[] { "romance", "rom_two_apps", "disaster", "new_cat", "halloween", "april_fools", "new_years" })
						{
							bool selected = loadedPatrol.tags[0].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								loadedPatrol.tags[0] = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(ImGui.Button("Add Tag"))
					{
						loadedPatrol.tags.Add("new_cat");
					}
					ImGui.SameLine();
					if(ImGui.Button("Remove Tag"))
					{
						loadedPatrol.tags.RemoveAt(selectedTag);
					}
				}
				else
				{
					if(ImGui.Button("Add Tag"))
					{
						loadedPatrol.tags.Add("new_cat");
					}
				}
			}
		}
		private void LoadMinMaxStatusEditor()
		{
			if(ImGui.CollapsingHeader("Mix Max Status Editing"))
			{
				if(loadedPatrol != null)
				{
					if(ImGui.BeginChildFrame(2, new(600, 80)))
					{
						if(ImGui.BeginTabBar("MinMaxTabs"))
						{
							if(loadedPatrol.min_max_status.apprentice != null)
							{
								if(ImGui.BeginTabItem("Apprentice"))
								{
									int min = loadedPatrol.min_max_status.apprentice[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.apprentice[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.medicine_cat_apprentice != null)
							{
								if(ImGui.BeginTabItem("Med Cat Apprentice"))
								{
									int min = loadedPatrol.min_max_status.medicine_cat_apprentice[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.medicine_cat_apprentice[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.medicine_cat != null)
							{
								if(ImGui.BeginTabItem("Med Cat"))
								{
									int min = loadedPatrol.min_max_status.medicine_cat[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.medicine_cat[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.deputy != null)
							{
								if(ImGui.BeginTabItem("Deputy"))
								{
									int min = loadedPatrol.min_max_status.deputy[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.deputy[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.warrior != null)
							{
								if(ImGui.BeginTabItem("Warrior"))
								{
									int min = loadedPatrol.min_max_status.warrior[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.warrior[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.warrior != null)
							{
								if(ImGui.BeginTabItem("Warrior"))
								{
									int min = loadedPatrol.min_max_status.warrior[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.warrior[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.leader != null)
							{
								if(ImGui.BeginTabItem("Leader"))
								{
									int min = loadedPatrol.min_max_status.leader[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.leader[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.healer_cats != null)
							{
								if(ImGui.BeginTabItem("Healers"))
								{
									int min = loadedPatrol.min_max_status.healer_cats[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.healer_cats[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.normal_adult != null)
							{
								if(ImGui.BeginTabItem("Normal Adult"))
								{
									int min = loadedPatrol.min_max_status.normal_adult[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.normal_adult[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							if(loadedPatrol.min_max_status.all_apprentices != null)
							{
								if(ImGui.BeginTabItem("All Apprentices Status"))
								{
									int min = loadedPatrol.min_max_status.all_apprentices[0];
									ImGui.InputInt("Min: ", ref min);
									int max = loadedPatrol.min_max_status.all_apprentices[1];
									ImGui.InputInt("Max: ", ref max);
									ImGui.EndTabItem();
								}
							}
							ImGui.EndChildFrame();
						}
					}
				}
			}
		}
		private void LoadRelationshipConstraintEditor()
		{
			//ImGui.SeparatorText("Relation Constraints");
			if(ImGui.CollapsingHeader("Relation Constraints"))
			{
				if(loadedPatrol.relationship_constraint != null && loadedPatrol.relationship_constraint.Count > 0)
				{
					if(ImGui.BeginCombo("Constraints", tempRelConstraint))
					{
						foreach(string s in new string[] { "siblings", "mates", "mates_with_pl", "parent/child", "child/parent", "romantic_val", "platonic_val", "dislike_val", "comfortable_val", "jealousy_val", "trust_val" })
						{
							bool selected = tempRelConstraint.Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								tempRelConstraint = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(tempRelConstraint.Contains("val"))
					{
						if(ImGui.InputInt("Value of constraint", ref tempRelVal, 1, 5, ImGuiInputTextFlags.None))
						{
							if(tempRelVal > 100)
								tempRelVal = 100;
							else if(tempRelVal < 0)
								tempRelVal = 0;
							tempRelConstraint.Replace("val", tempRelVal.ToString());
						}
					}
					if(ImGui.Button("Add Selected Constraint"))
					{
						loadedPatrol.relationship_constraint.Add(tempRelConstraint);
						tempRelConstraint = "";
						tempRelVal = 0;
					}
				}
				else
				{
					if(ImGui.Button("Add Relationship Constraint"))
					{
						tempRelConstraint = "siblings";
						tempRelVal = 0;
						loadedPatrol.relationship_constraint.Add(tempRelConstraint);
						tempRelConstraint = "";
						tempRelVal = 0;
					}
				}
			}
		}

		string outcomeType = "success", tempSkill = "", tempTrait = "", tempCanHave = "p_l";
		int selectedOutcome = 0;
		Outcome currentOutcome;
		List<Outcome> outcomesToSearch = new List<Outcome>();
		private void DrawOutcomeEditor()
		{
			ImGui.SetNextWindowSize(new(600, 250));
			if(ImGui.Begin("Outcome Editor", ImGuiWindowFlags.None))
			{
				if(ImGui.BeginCombo("Select Outcome Type", outcomeType))
				{
					foreach(string s in new string[] { "success", "fail", "antag_success", "antag_fail" })
					{
						bool selected = outcomeType.Equals(s);
						ImGui.Selectable(s, ref selected);
						if(selected)
							outcomeType = s;
						ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}
				if(ImGui.BeginCombo("Select Outcomes", currentOutcome != null ? currentOutcome.text : ""))
				{
					switch(outcomeType)
					{
						case "success":
							outcomesToSearch = loadedPatrol.success_outcomes;
						break;
						case "fail":
							outcomesToSearch = loadedPatrol.fail_outcomes;
						break;
						case "antag_success":
							outcomesToSearch = loadedPatrol.antag_success_outcomes;
						break;
						case "antag_fail":
							outcomesToSearch = loadedPatrol.antag_fail_outcomes;
						break;
					}
					if(outcomesToSearch != null)
					{
						foreach(Outcome o in outcomesToSearch)
						{
							bool selected = outcomeType.Equals(o);
							ImGui.Selectable(o.text, ref selected);
							if(selected)
								currentOutcome = o;
							ImGui.SetItemDefaultFocus();
						}
					}
					else
					{
						ImGui.Text("This outcome type has no existing outcomes!");
					}
					ImGui.EndCombo();
				}
				if(ImGui.Button($"Add {outcomeType[0].ToString().ToUpper() + outcomeType[1..]} Outcome"))
				{
					if(outcomesToSearch == null)
					{
						outcomesToSearch = [new Outcome { text = "outcome", exp = 0, weight = 0 }];
					}
					else
					{
						outcomesToSearch.Add(new Outcome { text = "outcome", exp = 0, weight = 0 });
					}
					Console.WriteLine(outcomesToSearch.ToString() + " " + outcomeType + " Wrote to list\n" + outcomesToSearch[0]);
				}
				ImGui.SeparatorText("Outcome Value Editor");
				if(currentOutcome != null)
				{
					//selectedOutcome = outcomesToSearch!.IndexOf(currentOutcome);
					if(currentOutcome.text != null && ImGui.InputText("Outcome Text", ref currentOutcome.text, 2600)) { }
					if(ImGui.InputInt("Experience Gained", ref currentOutcome.exp)) { }
					if(ImGui.InputInt("Weight", ref currentOutcome.weight)) { }
					if(ImGui.BeginChildFrame(55, new(ImGui.GetWindowSize().X, 80)))
					{
						#region Can Have Stat Editing
						ImGui.Text("Stat Cat Candidates:");
						if(currentOutcome.can_have_stat != null)
						{
							foreach(string cats in currentOutcome.can_have_stat)
							{
								ImGui.Text(cats);
							}
						}
						if(ImGui.BeginCombo("Cats", tempCanHave))
						{
							foreach(string c in new string[] { "p_l", "r_c", "app1", "app2", "not_pl_rc", "any", "adult", "app", "healer" })
							{
								bool selected = outcomeType.Equals(c);
								ImGui.Selectable(c, ref selected);
								if(selected)
									tempCanHave = c;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						if(ImGui.Button("Add")) 
						{
							if(currentOutcome.can_have_stat != null)
								currentOutcome.can_have_stat.Add(tempCanHave);
							else
							{
								currentOutcome.can_have_stat = [tempCanHave];
							}
						}
						ImGui.SameLine();
						if(currentOutcome.can_have_stat != null && ImGui.Button("Remove")) { currentOutcome.can_have_stat.Remove(tempCanHave); }
						#endregion
						ImGui.EndChildFrame();
					}
					if(ImGui.InputInt("Outsider Reputation Effect", ref currentOutcome.outsider_rep)) { }
					if(ImGui.InputInt("Other Clan Reputation Effect", ref currentOutcome.other_clan_rep)) { }
					if(currentOutcome.art != null) { if(ImGui.InputText("Art", ref currentOutcome.art, 2600)) { } } else { }
					if(currentOutcome.art_clean != null) if(ImGui.InputText("Art Clean", ref currentOutcome.art_clean, 2600)) { }
				}
				ImGui.EndChildFrame();
			}
		}

		public void Save()
		{
			if(loadedPath != null && loadedPath != "")
			{
				string newJson = JsonConvert.SerializeObject(patrols, Formatting.Indented);
				File.WriteAllText(loadedPath, newJson);
			}
		}
	}
}