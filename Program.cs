using ClanGenModTool.ObjectTypes;
using ClanGenModTool.UI;
using Newtonsoft.Json;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
if(File.Exists(MainWindow.ConfigPath))
{
	Console.WriteLine("Located config file!");
}
else
{
	Console.WriteLine("Creating config file");
	EditorConfig cfg = new EditorConfig
	{
		SessionHistory = []
	};
	Directory.CreateDirectory(MainWindow.ConfigPath.Replace("editor.config", null));
	File.WriteAllText(MainWindow.ConfigPath, JsonConvert.SerializeObject(cfg));
}

using MainWindow _ = new();
_.Run();