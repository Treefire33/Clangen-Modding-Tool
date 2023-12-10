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

namespace ClanGenModTool.UI.SubWindows
{
	public class PatrolEditor : Editor
	{
		public GL? GL;
		Patrol? loadedPatrol;
		List<Patrol> patrols = new List<Patrol>();
		int currentPatrol = 0;
		int currentSCOutcome = 0;
		int currentFCOutcome = 0;

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
					ImGui.SeparatorText("Advance Patrol (Default)");
					if(loadedPatrol.success_outcomes[currentSCOutcome] != null && ImGui.Button("Advance Patrol (Success)"))
					{
						previewedText = loadedPatrol.success_outcomes[currentSCOutcome].text;
					}
					if(loadedPatrol.fail_outcomes[currentFCOutcome] != null && ImGui.Button("Advance Patrol (Fail)"))
					{
						previewedText = loadedPatrol.fail_outcomes[currentFCOutcome].text;
					}
					ImGui.SeparatorText("Advance Patrol (Antagonize)");
					if(loadedPatrol.antag_success_outcomes != null && ImGui.Button("Antagonize Patrol (Success)"))
					{
						previewedText = loadedPatrol.antag_success_outcomes[currentSCOutcome].text; ;
					}
					if(loadedPatrol.antag_fail_outcomes != null && ImGui.Button("Antagonize Patrol (Fail)"))
					{
						previewedText = loadedPatrol.antag_fail_outcomes[currentFCOutcome].text;
					}
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
					if(ImGui.Button("Del"))
					{
						patrols.RemoveAt(i);
					}
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

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Patrol Attributes Editor", ImGuiWindowFlags.NoCollapse))
			{
				if(loadedPatrol != null)
				{
					ImGui.TextColored(new(255, 0, 0, 255), "This editor is not complete!");
					ImGui.InputText("Patrol ID", ref loadedPatrol.patrol_id, 400);
					if(ImGui.BeginCombo("Biome", loadedPatrol.biome[0]))
					{
						foreach(string s in new string[] { "forest", "plains", "mountainous", "beach", "wetlands", "desert", "Any" })
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
							bool selected = loadedPatrol.season[0].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
								loadedPatrol.season[0] = s;
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					if(loadedPatrol.tags.Count > 0)
					{
						if(ImGui.BeginCombo("Tags", loadedPatrol.tags[0]))
						{
							foreach(string s in new string[] { "patrol_rel", "new_cat", "pos_fail", "non-lethal", "disrespect", "other_clan", "clan_to_pl", "distrust", "halloween" })
							{
								bool selected = loadedPatrol.tags[0].Equals(s);
								ImGui.Selectable(s, ref selected);
								if(selected)
									loadedPatrol.tags[0] = s;
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						if(ImGui.IsItemHovered())
						{
							ImGui.BeginTooltip();
							ImGui.Text("Might be incomplete.");
							ImGui.EndTooltip();
						}
						if(ImGui.Button("Remove Tag"))
						{
							loadedPatrol.tags.RemoveAt(0);
						}
					}
					else
					{
						if(ImGui.Button("Add Tag"))
						{
							loadedPatrol.tags.Add("patrol_rel");
						}
					}
					if(ImGui.InputText("Patrol Art", ref loadedPatrol.patrol_art, 400)) { }
					if(ImGui.InputInt("Minimum Cats in Patrol", ref loadedPatrol.min_cats, 1)) { }
					if(ImGui.InputInt("Maximum Cats in Patrol", ref loadedPatrol.max_cats, 1)) { }
					if(ImGui.InputInt("Patrol Weight", ref loadedPatrol.weight, 1)) { }
					if(ImGui.InputTextMultiline("Intro Text", ref loadedPatrol.intro_text, 2400, new(350, 150))) { }
					if(ImGui.InputTextMultiline("Decline Text", ref loadedPatrol.decline_text, 2400, new(350, 150))) { }
					if(ImGui.InputInt("Chance of Success", ref loadedPatrol.chance_of_success, 1)) { }
					if(loadedPatrol.min_max_status != null) { LoadMinMaxStatusEditor(); }
					ImGui.SeparatorText("Outcome Editing");
					DrawOutcomeEditor();
					ImGui.End();
				}
			}
		}

		private void LoadMinMaxStatusEditor()
		{
			if(loadedPatrol != null)
			{
				ImGui.Text("Min Max Status:");
				if(ImGui.BeginChildFrame(2, new(600, 80)))
				{
					if(ImGui.BeginTabBar("MinMaxTabs"))
					{
						if(loadedPatrol.min_max_status.normaladult != null)
						{
							if(ImGui.BeginTabItem("Adult Status"))
							{
								int min = loadedPatrol.min_max_status.normaladult[0];
								ImGui.InputInt("Min: ", ref min);
								int max = loadedPatrol.min_max_status.normaladult[1];
								ImGui.InputInt("Max: ", ref max);
								ImGui.EndTabItem();
							}
						}
						if(loadedPatrol.min_max_status.leader != null)
						{
							if(ImGui.BeginTabItem("Leader Status"))
							{
								int min = loadedPatrol.min_max_status.leader[0];
								ImGui.InputInt("Min: ", ref min);
								int max = loadedPatrol.min_max_status.leader[1];
								ImGui.InputInt("Max: ", ref max);
								ImGui.EndTabItem();
							}
						}
						if(loadedPatrol.min_max_status.apprentice != null)
						{
							if(ImGui.BeginTabItem("Apprentice Status"))
							{
								int min = loadedPatrol.min_max_status.apprentice[0];
								ImGui.InputInt("Min: ", ref min);
								int max = loadedPatrol.min_max_status.apprentice[1];
								ImGui.InputInt("Max: ", ref max);
								ImGui.EndTabItem();
							}
						}
						if(loadedPatrol.min_max_status.allapprentices != null)
						{
							if(ImGui.BeginTabItem("All Apprentices Status"))
							{
								int min = loadedPatrol.min_max_status.allapprentices[0];
								ImGui.InputInt("Min: ", ref min);
								int max = loadedPatrol.min_max_status.allapprentices[1];
								ImGui.InputInt("Max: ", ref max);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndChildFrame();
					}
				}
			}
		}

		private void DrawOutcomeEditor()
		{
			if(ImGui.BeginChildFrame(1, new(600, 250)))
			{
				if(ImGui.BeginTabBar("OutcomeEditorTabs"))
				{
					if(ImGui.BeginTabItem("Success"))
					{
						ImGui.Text("Select Success Outcome:");
						ImGui.SameLine();
						ImGui.InputInt("", ref currentSCOutcome, 1);

						if(currentSCOutcome < 0)
							currentSCOutcome = 0;
						else if(currentSCOutcome > loadedPatrol!.success_outcomes.Count - 1)
							currentSCOutcome = loadedPatrol.success_outcomes.Count - 1;

						SuccessOutcome sc = loadedPatrol!.success_outcomes[currentSCOutcome];

						ImGui.Text($"Success Outcome {currentSCOutcome}:");
						ImGui.Indent();
						ImGui.InputText("Success Text", ref sc.text, 2400);
						ImGui.InputInt("Experience Gained", ref sc.exp, 1);
						ImGui.InputInt("Weight", ref sc.weight, 1);
						ImGui.Unindent();
						if(ImGui.Button("Add Success Outcome"))
						{
							loadedPatrol.success_outcomes.Add(new SuccessOutcome { text = "successful_patrol", exp = 0, weight = 0 });
							currentSCOutcome = loadedPatrol.success_outcomes.Count - 1;
						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Fail"))
					{
						ImGui.Text("Select Fail Outcome:");
						ImGui.SameLine();
						ImGui.InputInt("", ref currentFCOutcome, 1);

						if(currentFCOutcome < 0)
							currentFCOutcome = 0;
						else if(currentFCOutcome > loadedPatrol!.fail_outcomes.Count - 1)
							currentFCOutcome = loadedPatrol.fail_outcomes.Count - 1;

						FailOutcome fc = loadedPatrol!.fail_outcomes[currentFCOutcome];

						ImGui.Text($"Fail Outcome {currentFCOutcome}:");
						ImGui.Indent();
						ImGui.InputText("Fail Text", ref fc.text, 2400);
						ImGui.InputInt("Experience Gained", ref fc.exp, 1);
						ImGui.InputInt("Weight", ref fc.weight, 1);
						ImGui.Unindent();
						if(ImGui.Button("Add Fail Outcome"))
						{
							loadedPatrol.fail_outcomes.Add(new FailOutcome { text = "failed_patrol", exp = 0, weight = 0 });
							currentFCOutcome = loadedPatrol.fail_outcomes.Count - 1;

						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Antag Success"))
					{
						if(loadedPatrol.antag_success_outcomes != null && loadedPatrol.antag_success_outcomes.Count > 0)
						{
							ImGui.Text("Select Antag Success Outcome:");
							ImGui.SameLine();
							ImGui.InputInt("", ref currentSCOutcome, 1);

							if(currentSCOutcome < 0)
								currentSCOutcome = 0;
							else if(currentSCOutcome > loadedPatrol!.antag_success_outcomes.Count - 1)
								currentSCOutcome = loadedPatrol.antag_success_outcomes.Count - 1;

							AntagSuccessOutcome sc = loadedPatrol!.antag_success_outcomes[currentSCOutcome];

							ImGui.Text($"Success Outcome {currentSCOutcome}:");
							ImGui.Indent();
							ImGui.InputText("Success Text", ref sc.text, 2400);
							ImGui.InputInt("Experience Gained", ref sc.exp, 1);
							ImGui.InputInt("Weight", ref sc.weight, 1);
							ImGui.Unindent();
						}
						else
						{
							ImGui.TextColored(new(255, 0, 0, 255), "Add antag success event first!");
							loadedPatrol.antag_success_outcomes = new List<AntagSuccessOutcome>();
						}
						if(ImGui.Button("Add Antag Success Outcome"))
						{
							loadedPatrol.antag_success_outcomes.Add(new AntagSuccessOutcome { text = "successful_patrol", exp = 0, weight = 0 });
							currentSCOutcome = loadedPatrol.antag_success_outcomes.Count - 1;
						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Antag Fail"))
					{
						if(loadedPatrol.antag_fail_outcomes != null && loadedPatrol.antag_fail_outcomes.Count > 0)
						{
							ImGui.Text("Select Antag Fail Outcome:");
							ImGui.SameLine();
							ImGui.InputInt("", ref currentFCOutcome, 1);

							if(currentFCOutcome < 0)
								currentFCOutcome = 0;
							else if(currentFCOutcome > loadedPatrol!.antag_fail_outcomes.Count - 1)
								currentFCOutcome = loadedPatrol.antag_fail_outcomes.Count - 1;

							AntagFailOutcome fc = loadedPatrol!.antag_fail_outcomes[currentFCOutcome];

							ImGui.Text($"Fail Outcome {currentFCOutcome}:");
							ImGui.Indent();
							ImGui.InputText("Fail Text", ref fc.text, 2400);
							ImGui.InputInt("Experience Gained", ref fc.exp, 1);
							ImGui.InputInt("Weight", ref fc.weight, 1);
							ImGui.Unindent();
						}
						else
						{
							ImGui.TextColored(new(255, 0, 0, 255), "Add antag fail event first!");
							loadedPatrol.antag_fail_outcomes = new List<AntagFailOutcome>();
						}
						if(ImGui.Button("Add Antag Fail Outcome"))
						{
							loadedPatrol.antag_fail_outcomes.Add(new AntagFailOutcome { text = "failed_patrol", exp = 0, weight = 0 });
							currentFCOutcome = loadedPatrol.antag_fail_outcomes.Count - 1;
						}
					}
					ImGui.EndChildFrame();
				}
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
