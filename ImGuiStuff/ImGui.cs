using ClanGenModTool.ObjectTypes;
using ImGuiNET;
using Newtonsoft.Json;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool
{
	public static class ImGUI
	{
		public static void CenteredText(string text) 
		{
			float xPos = 0;
			if(text != null || text != "")
			{
				float windowWidth = ImGui.GetWindowSize().X;
				float textWidth = ImGui.CalcTextSize(text).X;

				xPos = (windowWidth - textWidth) * 0.5f;
			}
			ImGui.SetCursorPosX(xPos);
			ImGui.Text(text);
		}

		public static void CenteredColoredText(Vector4 color, string text)
		{
			float xPos = 0;
			if(text != null || text != "")
			{
				float windowWidth = ImGui.GetWindowSize().X;
				float textWidth = ImGui.CalcTextSize(text).X;

				xPos = (windowWidth - textWidth) * 0.5f;
			}
			ImGui.SetCursorPosX(xPos);
			ImGui.TextColored(color, text);
		}
	}
}
