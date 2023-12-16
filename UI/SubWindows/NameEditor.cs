using Silk.NET.OpenGL;
using ImGuiNET;
using System.Numerics;
using Newtonsoft.Json;
using Texture = ClanGenModTool.Textures.Texture;
using ClanGenModTool.ObjectTypes;

namespace ClanGenModTool.UI.SubWindows
{
	public class NameEditor : Editor
	{
		Name? loadedName = null;
		bool drawWarning = true;

		public void LoadEditor()
		{
			try
			{
				loadedName = JsonConvert.DeserializeObject<Name>(loadedJson!)!;
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
				if(ImGui.Begin("Select Name JSON first!", ImGuiWindowFlags.Popup))
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
				ErrorBox.Draw("This editor is not usable in full! \n You can only edit suffixes as of now.", ref drawWarning);
				DrawPrefixEditor();
				DrawSuffixEditor();
				DrawSpecialEditor();
			}
		}

		private void DrawPrefixEditor()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Prefix Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
			{
				
			}	
		}

		string selectedSuffix = "fur";
		List<string> selectedPeltSuffix = new List<string> { "fur" }, selectedTortieSuffix = new List<string> { "fur" }, selectedAnimalSuffix = new List<string> { "fur" };
		int previewIndex = 0;
		private void DrawSuffixEditor()
		{
			ImGui.SetNextWindowSize(new Vector2(450, 400), ImGuiCond.Once);
			if(ImGui.Begin("Suffix Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
			{
				if(ImGui.BeginTabBar("Suffixes"))
				{
					if(ImGui.BeginTabItem("Normal Suffixes"))
					{
						if(ImGui.BeginCombo("Suffix", loadedName.normal_suffixes[previewIndex]))
						{
							foreach(string s in loadedName.normal_suffixes)
							{
								bool selected = loadedName.normal_suffixes.Equals(s);
								ImGui.Selectable(s, ref selected);
								if(selected)
								{
									selectedSuffix = s;
									previewIndex = loadedName.normal_suffixes.IndexOf(s);
								}
								ImGui.SetItemDefaultFocus();
							}
							ImGui.EndCombo();
						}
						ImGui.InputText("Edit Suffix", ref selectedSuffix, 400);
						if(ImGui.Button("Add Suffix"))
						{
							loadedName.normal_suffixes.Add("suffix");
							previewIndex = loadedName.normal_suffixes.Count - 1;
							selectedSuffix = loadedName.normal_suffixes[previewIndex];
						}
						if(ImGui.Button("Remove Selected Suffix"))
						{
							loadedName.normal_suffixes.RemoveAt(previewIndex);
							previewIndex--;
							if(previewIndex < 0)
								previewIndex = 0;
							selectedSuffix = loadedName.normal_suffixes[previewIndex];
						}
						loadedName.normal_suffixes[previewIndex] = selectedSuffix;
						ImGui.EndTabItem();
					}
					return; //Prevent program from creating the other suffix categories
					if(ImGui.BeginTabItem("Pelt Suffixes"))
					{
						if(ImGui.BeginTabBar("PeltSuffixes"))
						{
							List<List<string>> types = loadedName.pelt_suffixes.Enumerate();
							foreach(List<string> l in types)
							{
								if(ImGui.BeginTabItem(loadedName.pelt_suffixes.LookUp(types.IndexOf(l))))
								{
									if(ImGui.BeginCombo("Suffix", l[previewIndex]))
									{
										foreach(string s in l)
										{
											bool selected = l.Equals(s);
											ImGui.Selectable(s, ref selected);
											if(selected)
											{
												selectedPeltSuffix[selectedPeltSuffix.IndexOf(s)] = s;
												previewIndex = l.IndexOf(s);
											}
											ImGui.SetItemDefaultFocus();
										}
										ImGui.EndCombo();
									}
									//ImGui.InputText("Edit Suffix", ref selectedPeltSuffix, 400);
									if(ImGui.Button("Add Suffix"))
									{
										l.Add("suffix");
										previewIndex = l.Count - 1;
										//selectedPeltSuffix = l[previewIndex];
									}
									if(ImGui.Button("Remove Selected Suffix"))
									{
										l.RemoveAt(previewIndex);
										previewIndex--;
										if(previewIndex < 0)
											previewIndex = 0;
										//selectedPeltSuffix = l[previewIndex];
									}
									//l[previewIndex] = selectedPeltSuffix;
									ImGui.EndTabItem();
								}
							}
							ImGui.EndTabBar();
						}
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Tortie Suffixes"))
					{
						ImGui.EndTabItem();
					}
					if(ImGui.BeginTabItem("Animal Suffixes"))
					{
						ImGui.EndTabItem();
					}
					ImGui.EndTabBar();
				}
			}
		}

		private void DrawSpecialEditor()
		{
			ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
			if(ImGui.Begin("Special Name Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
			{

			}
		}

		public void Save()
		{
			if(loadedPath != null && loadedPath != "")
			{
				string newJson = JsonConvert.SerializeObject(loadedName, Formatting.Indented);
				File.WriteAllText(loadedPath, newJson);
			}
		}
	}
}
