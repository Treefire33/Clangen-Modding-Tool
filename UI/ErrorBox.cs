using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;

namespace ClanGenModTool.UI;

public class ErrorBox
{
	static bool draw = false;
	public static void Draw(string error)
	{
		ImGui.SetNextWindowSize(new(450, 200), ImGuiCond.Once);
		if(draw)
			if(ImGui.Begin("Error!", ImGuiWindowFlags.None))
			{
				ImGui.Text("What happened:\n" + error);

				if(ImGui.Button("Ok"))
				{
					draw = false;
				}

				ImGui.End();
			}
	}
	public static void Draw(string error, ref bool draw)
	{
		ImGui.SetNextWindowSize(new(450,200), ImGuiCond.Once);
		if(draw)
			if(ImGui.Begin("Error!", ImGuiWindowFlags.None))
			{
				ImGui.Text("What happened:\n"+error);

				if(ImGui.Button("Ok"))
				{
					draw = false;
				}

				ImGui.End();
			}
	}
}