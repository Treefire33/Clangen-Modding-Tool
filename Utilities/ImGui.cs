using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using System.Numerics;

namespace ClanGenModTool;

public static class ImExtended
{
	public static void CenteredText(string text) 
	{
		float xPos = 0;
		if(text != "")
		{
			float windowWidth = ImGui.GetWindowSize().X;
			float textWidth = ImGui.CalcTextSize(text).X;

			xPos = (windowWidth - textWidth) * 0.5f;
		}
		ImGui.SetCursorPosX(xPos);
		ImGui.Text(text);
	} 

	public static void CenteredText(string text, Vector2 bounds)
	{
		float xPos = 0;
		if(text != "")
		{
			float windowWidth = bounds.X;
			float textWidth = ImGui.CalcTextSize(text).X;

			xPos = (windowWidth - textWidth) * 0.5f;
		}
		ImGui.SetCursorPosX(xPos);
		ImGui.Text(text);
	}

	public static void CenteredColoredText(Vector4 color, string text)
	{
		float xPos = 0;
		if(text != "")
		{
			float windowWidth = ImGui.GetWindowSize().X;
			float textWidth = 0;
			try
			{
				textWidth = ImGui.CalcTextSize(text).X;
			}
			catch { return; }

				xPos = (windowWidth - textWidth) * 0.5f;
		}
		ImGui.SetCursorPosX(xPos);
		ImGui.TextColored(color, text);
	}

	public static void CenteredColoredText(Vector4 color, string text, Vector2 bounds)
	{
		float xPos = 0;
		if(text != "")
		{
			float windowWidth = bounds.X;
			float textWidth = 0;
			try
			{
				textWidth = ImGui.CalcTextSize(text).X;
			}
			catch { return; }

			xPos = (windowWidth - textWidth) * 0.5f;
		}
		ImGui.SetCursorPosX(xPos);
		ImGui.TextColored(color, text);
	}

	public static void Combo(string title, ref string? field, string[] list)
	{
		if(ImGui.BeginCombo(title, field))
		{
			foreach(string s in list)
			{
				bool selected = field != null && field.Equals(s);
				ImGui.Selectable(s, ref selected); 
				if(selected) 
					field = s; 
				ImGui.SetItemDefaultFocus();
			}
			ImGui.EndCombo();
		}
	}

	public static void Combo(string title, ref string? field, string[] list, int id)
	{
		ImGui.PushID(id);
		if(ImGui.BeginCombo(title, field))
		{
			foreach(string s in list)
			{
				bool selected = field != null && field.Equals(s);
				ImGui.Selectable(s, ref selected);
				if(selected)
					field = s;
				ImGui.SetItemDefaultFocus();
			}
			ImGui.EndCombo();
		}
		ImGui.PopID();
	}
}