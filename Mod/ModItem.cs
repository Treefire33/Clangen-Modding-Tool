using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClangenModTool.Mod
{
	public class ModItem
	{
		#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Name { get; set; }
		public string ModPath { get; set; }
		public string SpritesPath { get; set; }
		public string PatrolsPath { get; set; }
	}
}
