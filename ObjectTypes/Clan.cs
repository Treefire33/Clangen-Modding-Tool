using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	public class Clan
	{
		public string clanname;
		public int clanage;
		public string biome;
		public string camp_bg;
		public string gamemode;
		public string instructor;
		public int reputation;
		public List<object> mediated;
		public string starting_season;
		public string temperament;
		public int version_name;
		public string version_commit;
		public bool source_build;
		public string leader;
		public int leader_lives;
		public int leader_predecessors;
		public string deputy;
		public int deputy_predecessors;
		public string med_cat;
		public int med_cat_number;
		public int med_cat_predecessors;
		public string clan_cats;
		public string faded_cats;
		public List<object> patrolled_cats;
		public string other_clans_names;
		public string other_clans_relations;
		public string other_clan_temperament;
		public War war;
	}

	public class War
	{
		public bool at_war;
		public object enemy;
		public int duration;
	}


}
