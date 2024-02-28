using ImGuiNET;
using System.Numerics;
using Newtonsoft.Json;
using Texture = ClanGenModTool.Textures.Texture;
using ClanGenModTool.ObjectTypes;

namespace ClanGenModTool.UI.SubWindows;

public class NameEditor : Editor
{
	private Name? loadedName;

	public void LoadEditor()
	{
		try
		{
			loadedName = JsonConvert.DeserializeObject<Name>(LoadedJson!)!;
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
			DrawPrefixEditor();
			DrawSuffixEditor();
			DrawSpecialEditor();
		}
	}

	private int previewPrefix = 0;
	private void DrawPrefixEditor()
	{
		ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
		if(ImGui.Begin("Prefix Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
		{
			if(ImGui.BeginTabBar("Prefixes"))
			{
				if(ImGui.BeginTabItem("Normal Prefixes"))
				{
					BeginAffixSubEditor(ref loadedName.normal_prefixes, 1800, "Prefix", ref previewPrefix);
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Biome Prefixes"))
				{
					if(ImGui.BeginTabBar("BiomePrefixes"))
					{
						List<List<string>> types = loadedName.biome_prefixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.biome_prefixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.biome_prefixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1810 + i, "Prefix", ref previewPrefix);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Eye Prefixes"))
				{
					if(ImGui.BeginTabBar("EyePrefixes"))
					{
						List<List<string>> types = loadedName.eye_prefixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.eye_prefixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.eye_prefixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1840 + i, "Prefix", ref previewPrefix);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Colour Prefixes"))
				{
					if(ImGui.BeginTabBar("ColourPrefixes"))
					{
						List<List<string>> types = loadedName.colour_prefixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.colour_prefixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.colour_prefixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1860 + i, "Prefix", ref previewPrefix);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Animal Prefixes"))
				{
					BeginAffixSubEditor(ref loadedName.animal_prefixes, 1878, "Prefix", ref previewPrefix);
					ImGui.EndTabItem();
				}
			}
		}
	}

	//List<string> selectedPeltSuffix = new List<string> { "fur" }, selectedTortieSuffix = new List<string> { "fur" }, selectedAnimalSuffix = new List<string> { "fur" };
	private int previewIndex = 0;
	private void DrawSuffixEditor()
	{
		ImGui.SetNextWindowSize(new Vector2(450, 400), ImGuiCond.Once);
		if(ImGui.Begin("Suffix Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
		{
			if(ImGui.BeginTabBar("Suffixes"))
			{
				if(ImGui.BeginTabItem("Normal Suffixes"))
				{
					BeginAffixSubEditor(ref loadedName.normal_suffixes, 1600);
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Pelt Suffixes"))
				{
					if(ImGui.BeginTabBar("PeltSuffixes"))
					{
						List<List<string>> types = loadedName.pelt_suffixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.pelt_suffixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.pelt_suffixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1610+i);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Tortie Suffixes"))
				{
					if(ImGui.BeginTabBar("TortieSuffixes"))
					{
						List<List<string>> types = loadedName.tortie_pelt_suffixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.tortie_pelt_suffixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.tortie_pelt_suffixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1650 + i);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Animal Suffixes"))
				{
					BeginAffixSubEditor(ref loadedName.animal_suffixes, 1700);
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Biome Suffixes"))
				{
					if(ImGui.BeginTabBar("BiomeSuffixes"))
					{
						List<List<string>> types = loadedName.biome_suffixes.Enumerate();
						for(int i = 0; i < types.Count; i++)
						{
							List<string> l = loadedName.biome_suffixes.Enumerate()[i];
							if(ImGui.BeginTabItem(loadedName.biome_suffixes.LookUp(types.IndexOf(l))))
							{
								BeginAffixSubEditor(ref l, 1706 + i);
								ImGui.EndTabItem();
							}
						}
						ImGui.EndTabBar();
					}
					ImGui.EndTabItem();
				}
				ImGui.EndTabBar();
			}
		}
	}

	private int previewSpecial = 0;
	private void DrawSpecialEditor()
	{
		ImGui.SetNextWindowSize(new Vector2(200, 400), ImGuiCond.Once);
		if(ImGui.Begin("Special Name Editor", ImGuiWindowFlags.NoCollapse) && loadedName != null)
		{
			if(ImGui.BeginTabBar("Bar"))
			{
				if(ImGui.BeginTabItem("Status Based"))
				{
					if(ImGui.InputText(nameof(loadedName.special_suffixes.newborn), ref loadedName.special_suffixes.newborn, 2500)) { }
					if(ImGui.InputText(nameof(loadedName.special_suffixes.kitten), ref loadedName.special_suffixes.kitten, 2500)) { }
					if(ImGui.InputText(nameof(loadedName.special_suffixes.apprentice), ref loadedName.special_suffixes.apprentice, 2500)) { }
					if(ImGui.InputText(nameof(loadedName.special_suffixes.medicinecatapprentice), ref loadedName.special_suffixes.medicinecatapprentice, 2500)) { }
					if(ImGui.InputText(nameof(loadedName.special_suffixes.mediatorapprentice), ref loadedName.special_suffixes.mediatorapprentice, 2500)) { }
					if(ImGui.InputText(nameof(loadedName.special_suffixes.leader), ref loadedName.special_suffixes.leader, 2500)) { }
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Loner"))
				{
					BeginAffixSubEditor(ref loadedName.loner_names, 1760, "Loner Name", ref previewSpecial);
					ImGui.EndTabItem();
				}
				if(ImGui.BeginTabItem("Inappropriate"))
				{
					BeginAffixSubEditor(ref loadedName.inappropriate_names, 1770, "Inappropriate Name", ref previewSpecial);
					ImGui.EndTabItem();
				}
			}
		}
	}

	public void BeginAffixSubEditor(ref List<string> suffixes, int id)
	{
		ImGui.PushID(id);
		if(previewIndex > suffixes.Count)
		{
			previewIndex = 0;
		}
		if(ImGui.BeginCombo("Suffix", suffixes[previewIndex] ?? ""))
		{
			foreach(string s in suffixes)
			{
				bool selected = suffixes[previewIndex].Equals(s);
				ImGui.Selectable(s, ref selected);
				if(selected)
				{
					previewIndex = suffixes.IndexOf(s);
				}
				ImGui.SetItemDefaultFocus();
			}
			ImGui.EndCombo();
		}
		ImGui.PopID();
		ImGui.PushID(id+1);
		string current = suffixes[previewIndex];
		ImGui.InputText("Edit Suffix", ref current, 400);
		ImGui.PopID();
		ImGui.PushID(id+2);
		if(ImGui.Button("Add Suffix"))
		{
			suffixes.Add("suffix");
			previewIndex = suffixes.Count - 1;
		}
		ImGui.PopID();
		ImGui.PushID(id+3);
		if(ImGui.Button("Remove Selected Suffix"))
		{
			suffixes.RemoveAt(previewIndex);
			previewIndex--;
			if(previewIndex < 0)
				previewIndex = 0;
		}
		ImGui.PopID();
		suffixes[previewIndex] = current;
	}
	public void BeginAffixSubEditor(ref List<string> suffixes, int id, string affixType)
	{
		ImGui.PushID(id);
		if(previewIndex > suffixes.Count)
		{
			previewIndex = 0;
		}
		if(ImGui.BeginCombo(affixType, suffixes[previewIndex] ?? ""))
		{
			foreach(string s in suffixes)
			{
				bool selected = suffixes[previewIndex].Equals(s);
				ImGui.Selectable(s, ref selected);
				if(selected)
				{
					previewIndex = suffixes.IndexOf(s);
				}
				ImGui.SetItemDefaultFocus();
			}
			ImGui.EndCombo();
		}
		ImGui.PopID();
		ImGui.PushID(id + 1);
		string current = suffixes[previewIndex];
		ImGui.InputText("Edit "+affixType, ref current, 400);
		ImGui.PopID();
		ImGui.PushID(id + 2);
		if(ImGui.Button("Add "+affixType))
		{
			suffixes.Add("new_"+affixType);
			previewIndex = suffixes.Count - 1;
			current = suffixes[previewIndex];
		}
		ImGui.PopID();
		ImGui.PushID(id + 3);
		if(ImGui.Button("Remove Selected "+affixType))
		{
			suffixes.RemoveAt(previewIndex);
			previewIndex--;
			if(previewIndex < 0)
				previewIndex = 0;
		}
		ImGui.PopID();
		suffixes[previewIndex] = current;
	}
	public void BeginAffixSubEditor(ref List<string> suffixes, int id, string affixType, ref int prev)
	{
		ImGui.PushID(id);
		if(prev > suffixes.Count)
		{
			prev = 0;
		}
		if(ImGui.BeginCombo(affixType, suffixes[prev] ?? ""))
		{
			foreach(string s in suffixes)
			{
				bool selected = suffixes[previewIndex].Equals(s);
				ImGui.Selectable(s, ref selected);
				if(selected)
				{
					prev = suffixes.IndexOf(s);
				}
				ImGui.SetItemDefaultFocus();
			}
			ImGui.EndCombo();
		}
		ImGui.PopID();
		ImGui.PushID(id + 1);
		string current = suffixes[prev];
		ImGui.InputText("Edit " + affixType, ref current, 400);
		ImGui.PopID();
		ImGui.PushID(id + 2);
		if(ImGui.Button("Add " + affixType))
		{
			suffixes.Add("new_" + affixType);
			prev = suffixes.Count - 1;
			current = suffixes[prev];
		}
		ImGui.PopID();
		ImGui.PushID(id + 3);
		if(ImGui.Button("Remove Selected " + affixType))
		{
			suffixes.RemoveAt(prev);
			prev--;
			if(prev < 0)
				prev = 0;
		}
		ImGui.PopID();
		suffixes[prev] = current;
	}

	public void Save()
	{
		if(!string.IsNullOrEmpty(LoadedPath))
		{
			string newJson = JsonConvert.SerializeObject(loadedName, Formatting.Indented);
			File.WriteAllText(LoadedPath, newJson);
		}
	}
}