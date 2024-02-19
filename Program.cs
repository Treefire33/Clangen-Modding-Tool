using ClanGenModTool.ObjectTypes;
using ClanGenModTool.UI;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ImGuiNET;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
if(File.Exists(MainWindow.configPath))
{
	Console.WriteLine("Located config file!");
}
else
{
	Console.WriteLine("Creating config file");
	EditorConfig _cfg = new EditorConfig
	{
		sessionHistory = new List<SessionHistory>()
	};
	Directory.CreateDirectory(MainWindow.configPath.Replace("editor.config", null));
	File.WriteAllText(MainWindow.configPath, JsonConvert.SerializeObject(_cfg));
}
using(MainWindow _ = new MainWindow())
{
	_.Run();
}