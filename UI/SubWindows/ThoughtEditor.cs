using OpenTK.Graphics.OpenGL4;
using ImGuiNET;
using System.Numerics;
using Newtonsoft.Json;
using Texture = ClanGenModTool.Textures.Texture;
using ClanGenModTool.ObjectTypes;

namespace ClanGenModTool.UI.SubWindows
{
	public class ThoughtEditor : Editor
	{
		List<Thought> thoughts = new List<Thought>();

		Thought? loadedThought = null;
		int currentThought;

		public static Texture thoughtPreviewImg;

		public void LoadEditor()
		{
			try
			{
				thoughts = JsonConvert.DeserializeObject<List<Thought>>(loadedJson!)!;
				loadedThought = thoughts[0];
				currentThought = 0;
			}
			catch(Exception ex)
			{
				ErrorBox.Draw("Invalid JSON!\n" + ex);
			}
		}

		public void AssignToLoadedThought()
		{
			loadedThought = thoughts[currentThought];
		}

		public static void BeforeDrawEditor()
		{
			thoughtPreviewImg = Texture.LoadFromFile("./Resources/Images/thoughtPreview.png");
			thoughtPreviewImg.Use(TextureUnit.Texture1);
		}

		public void Draw(ref bool continueDraw)
		{
			if(loadedJson == null)
			{
				ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.Once);
				ImGui.SetNextWindowPos(new Vector2(0, 19));
				if(ImGui.Begin("Select Thought List First!", ImGuiWindowFlags.Popup))
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
				DrawPreviewWindow();
			}
		}

		private void DrawSelectThought()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Thought List Select", ImGuiWindowFlags.NoCollapse) && thoughts != null)
			{
				for(int i = 0; i < thoughts.Count; i++)
				{
					try
					{
						if(ImGui.Button(thoughts[i].id))
						{
							currentThought = i;
							AssignToLoadedThought();
						}
						ImGui.SameLine();
						if(ImGui.Button("Del"))
						{
							thoughts.RemoveAt(i);
						}
					} catch(Exception e) {
						ErrorBox.Draw("Not a thought json!" + e);
					}
				}
				/*if(ImGui.Button("Add Thought"))
				{
					Thought t = new Thought();
					t.DefaultThought();
					thoughts.Add(t);
				}*/
				ImGui.End();
			}
		}

		string previewedText = "";
		private void DrawPreviewWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Thought Preview", ImGuiWindowFlags.NoResize))
			{
				if(loadedThought != null)
				{
					previewedText = loadedThought.thoughts[currentSelected];
				}
				ImGui.Image(thoughtPreviewImg.Handle, new(600, 500));
				ImGui.SetCursorPosY(155);
				ImExtended.CenteredColoredText(new(0,0,0,255), previewedText, new(600,500));
				ImGui.End();
			}
		}

		string selectedThoughtText = "";
		int currentSelected = 0;

		private void DrawAttributesWindow()
		{
			ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.Once);
			if(ImGui.Begin("Thought List Attributes Editor", ImGuiWindowFlags.NoCollapse))
			{
				ImGui.TextColored(new(255,0,0,255), "this editor is not complete!");
				if(loadedThought != null)
				{
					if(currentSelected > loadedThought.thoughts.Count)
						currentSelected = 0;
					if(ImGui.BeginCombo("Select Thought", loadedThought.thoughts[currentSelected] ?? ""))
					{
						foreach(string s in loadedThought.thoughts)
						{
							bool selected = loadedThought.thoughts[currentSelected].Equals(s);
							ImGui.Selectable(s, ref selected);
							if(selected)
							{
								currentSelected = loadedThought.thoughts.IndexOf(s);
							}
							ImGui.SetItemDefaultFocus();
						}
						ImGui.EndCombo();
					}
					ThoughtTextEditor();
					LoadConstraintEditor();
				}
			}
		}

		private void LoadConstraintEditor()
		{
			if(loadedThought.random_status_constraint != null)
			{
				ImGui.Text(string.Format("Random Status Constraints:\n{0}", string.Join(", ", loadedThought.random_status_constraint)));
			}
			if(loadedThought.random_living_status != null)
			{
				ImGui.Text(string.Format("Random Living Constraints:\n{0}", string.Join(", ", loadedThought.random_living_status)));
			}
			if(loadedThought.relationship_constraint != null)
			{
				ImGui.Text(string.Format("Relationship Constraints:\n{0}", string.Join(", ", loadedThought.relationship_constraint)));
			}
			if(loadedThought.random_age_constraint != null)
			{
				ImGui.Text(string.Format("Random Status Constraints:\n{0}", string.Join(", ", loadedThought.random_age_constraint)));
			}
			if(loadedThought.main_trait_constraint != null)
			{
				ImGui.Text(string.Format("Main Trait Constraints:\n{0}", string.Join(", ", loadedThought.main_trait_constraint)));
			}
			if(loadedThought.main_backstory_constraint != null)
			{
				ImGui.Text(string.Format("Main Backstory Constraints:\n{0}", string.Join(", ", loadedThought.main_backstory_constraint)));
			}
			if(loadedThought.main_status_constraint != null)
			{
				ImGui.Text(string.Format("Main Status Constraints:\n{0}", string.Join(", ", loadedThought.main_status_constraint)));
			}
		}

		//string displayedThoughtText = "";

		private void ThoughtTextEditor()
		{
			selectedThoughtText = loadedThought!.thoughts[currentSelected];
			//displayedThoughtText = CreateWrapping(selectedThoughtText, 15);
			if(currentThought > loadedThought.thoughts.Count - 1)
				currentThought = loadedThought.thoughts.Count;
			else if(currentThought < 0)
				currentThought = 0;
			ImGui.InputTextMultiline("Thought Text", ref selectedThoughtText, 2500, new Vector2(350, 200));
			loadedThought!.thoughts[currentSelected] = selectedThoughtText;
			if(ImGui.Button("Add Thought"))
			{
				loadedThought.thoughts.Add("Thinking about placeholder values");
				currentSelected = loadedThought.thoughts.Count - 1;
			}
			ImGui.SameLine();
			if(ImGui.Button("Remove Thought"))
			{
				loadedThought.thoughts.RemoveAt(currentSelected);
				if(currentSelected > 0)
					currentSelected--;
				else
					currentSelected = 0;
			}
		}

		private string CreateWrapping(string wrappedText, int charsToIgnore)
		{
			string rendered = "";
			for(int i = 0; i < wrappedText.Length; i++)
			{
				rendered+= wrappedText[i];
				if(i % charsToIgnore == 0)
				{
					rendered.Insert(i, "\n");
				}
			}
			return rendered;
		}
		private string RemoveWrapping(string wrappedText)
		{
			string rendered = "";
			for(int i = 0; i < wrappedText.Length; i++)
			{
				if(wrappedText[i] == '\n')
				{
					wrappedText.Remove(i);
					continue;
				}
				rendered += wrappedText[i];
				
			}
			return rendered;
		}

		public void Save()
		{
			if(loadedPath != null && loadedPath != "")
			{
				string newJson = JsonConvert.SerializeObject(thoughts, Formatting.Indented);
				File.WriteAllText(loadedPath, newJson);
			}
		}
	}
}
