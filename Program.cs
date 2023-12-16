using ClanGenModTool.ObjectTypes;
using ClanGenModTool.UI;
using Newtonsoft.Json;
using Silk.NET.Windowing;

System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
if(File.Exists(StartWindow.configPath))
{
	Console.WriteLine("Located config file!");
}
else
{
	Console.WriteLine("Creating config file");
	EditorConfig _cfg = new EditorConfig();
	_cfg.patrolPath = "";
	_cfg.clanPath = "";
	_cfg.thoughtPath = "";
	_cfg.patrolImagesPath = "";
	Directory.CreateDirectory(StartWindow.configPath.Replace("editor.config", null));
	File.WriteAllText(StartWindow.configPath, JsonConvert.SerializeObject(_cfg));
}
StartWindow window = new StartWindow();