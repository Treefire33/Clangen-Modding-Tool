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
		bool outcomeToggle = true;
		bool antagToggle = false;
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
				patrolPreviewImg = new Texture(GL, "./Resources/patrolPreview.png");
				patrolPreviewImg.Bind(TextureUnit.Texture0);
				ImGui.Image(new IntPtr(patrolPreviewImg._handle), new(600, 500));
				ImGui.SetCursorPos(new(293, 150));
				ImGui.PushTextWrapPos(350);
				ImGui.TextWrapped(previewedText);
				ImGui.PopTextWrapPos();
				if(loadedPatrol != null)
				{
					ImGui.SetCursorPos(new(0, 550));
					if(ImGui.Button("Start Patrol"))
					{
						previewedText = loadedPatrol.intro_text;
					}
					ImGui.SameLine();
					if(ImGui.Button("Advance Patrol (Success)"))
					{
						previewedText = loadedPatrol.success_outcomes[currentSCOutcome].text;
					}
					ImGui.SameLine();
					if(ImGui.Button("Advance Patrol (Fail)"))
					{
						previewedText = loadedPatrol.fail_outcomes[currentFCOutcome].text;
					}
					ImGui.SameLine();
					if(ImGui.Button("Decline Patrol"))
					{
						previewedText = loadedPatrol.decline_text;
					}
					if(ImGui.Button("Antagonize Patrol (Success)") && loadedPatrol.antag_success_outcomes != null)
					{
						previewedText = loadedPatrol.antag_success_outcomes[currentSCOutcome].text; ;
					}
					if(ImGui.Button("Antagonize Patrol (Fail)") && loadedPatrol.antag_fail_outcomes != null)
					{
						previewedText = loadedPatrol.antag_fail_outcomes[currentFCOutcome].text;
					}
				}
				ImGui.End();
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
					ImGui.InputText("Patrol Art", ref loadedPatrol.patrol_art, 400);
					ImGui.InputInt("Minimum Cats in Patrol", ref loadedPatrol.min_cats, 1);
					ImGui.InputInt("Maximum Cats in Patrol", ref loadedPatrol.max_cats, 1);
					ImGui.InputInt("Patrol Weight", ref loadedPatrol.weight, 1);
					ImGui.InputTextMultiline("Intro Text", ref loadedPatrol.intro_text, 2400, new(350, 150));
					ImGui.InputTextMultiline("Decline Text", ref loadedPatrol.decline_text, 2400, new(350, 150));
					ImGui.InputInt("Chance of Success", ref loadedPatrol.chance_of_success, 1);
					if(loadedPatrol.min_max_status != null)
					{
						ImGui.Text("Min Max Status editing is currently not supported!");
					}
					ImGui.SeparatorText("Outcome Editing");
					ImGui.Checkbox("Editing Success Outcomes?", ref outcomeToggle);
					ImGui.Checkbox("Editing Antag Outcomes?", ref antagToggle);
					DrawOutcomesEditor(antagToggle);
					if(!antagToggle)
					{
						if(ImGui.Button("Add Success Outcome"))
						{
							loadedPatrol.success_outcomes.Add(new SuccessOutcome { text = "successful_patrol", exp = 0, weight = 0 });
							currentSCOutcome = loadedPatrol.success_outcomes.Count - 1;
							outcomeToggle = true;
						}
						if(ImGui.Button("Add Fail Outcome"))
						{
							loadedPatrol.fail_outcomes.Add(new FailOutcome { text = "failed_patrol", exp = 0, weight = 0 });
							currentFCOutcome = loadedPatrol.fail_outcomes.Count - 1;
							outcomeToggle = false;
						}
					}
					if(antagToggle)
					{
						if(loadedPatrol.antag_success_outcomes == null)
						{
							loadedPatrol.antag_success_outcomes = new List<AntagSuccessOutcome>();
							loadedPatrol.antag_fail_outcomes = new List<AntagFailOutcome>();
						}
						if(ImGui.Button("Add Antag Success Outcome"))
						{
							loadedPatrol.antag_success_outcomes.Add(new AntagSuccessOutcome { text = "successful_patrol", exp = 0, weight = 0 });
							currentSCOutcome = loadedPatrol.antag_success_outcomes.Count - 1;
							outcomeToggle = true;
						}
						if(ImGui.Button("Add Antag Fail Outcome"))
						{
							loadedPatrol.antag_fail_outcomes.Add(new AntagFailOutcome { text = "failed_patrol", exp = 0, weight = 0 });
							currentFCOutcome = loadedPatrol.antag_fail_outcomes.Count - 1;
							outcomeToggle = false;
						}
					}
				}

				ImGui.End();
			}
		}

		private void DrawOutcomesEditor(bool antag)
		{
			if(!antag)
			{
				if(outcomeToggle)
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
				}
				else if(!outcomeToggle)
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
				}
			}
			else
			{
				if(loadedPatrol.antag_success_outcomes != null && loadedPatrol.antag_success_outcomes.Count > 0)
				{
					if(outcomeToggle)
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
					else if(!outcomeToggle)
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
				}
				else
				{
					ImGui.TextColored(new(255, 0, 0, 255), "Add an antag event first!");
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
