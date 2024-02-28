using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes;

public class EditorConfig
{
	public List<SessionHistory> SessionHistory;
}

public class SessionHistory
{
	public string Path;
	public string Type;
}