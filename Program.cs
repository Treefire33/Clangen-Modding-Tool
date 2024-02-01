using ClanGenModTool.ObjectTypes;
using ClanGenModTool.UI;
using Newtonsoft.Json;
using Silk.NET.Windowing;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
if(File.Exists(StartWindow.configPath))
{
	Console.WriteLine("Located config file!");
}
else
{
	Console.WriteLine("Creating config file");
	EditorConfig _cfg = new EditorConfig
	{
		patrolPath = "",
		clanPath = "",
		thoughtPath = "",
		patrolImagesPath = "",
		sessionHistory = new List<SessionHistory>()
	};
	Directory.CreateDirectory(StartWindow.configPath.Replace("editor.config", null));
	File.WriteAllText(StartWindow.configPath, JsonConvert.SerializeObject(_cfg));
}
StartWindow _ = new StartWindow();