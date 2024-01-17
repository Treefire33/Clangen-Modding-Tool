using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	public class Cat
	{
		public string ID;
		public string name_prefix;
		public string name_suffix;
		public bool specsuffix_hidden;
		public string gender;
		public string gender_align;
		public int birth_cooldown;
		public string status;
		public string backstory;
		public int moons;
		public string trait;
		public string facets;
		public string parent1;
		public string parent2;
		public List<string> adoptive_parents;
		public string mentor;
		public List<string> former_mentor;
		public int patrol_with_mentor;
		public List<string> mate;
		public List<string> previous_mates;
		public bool dead;
		public bool paralyzed;
		public bool no_kits;
		public bool exiled;
		public string pelt_name;
		public string pelt_color;
		public string pelt_length;
		public int sprite_kitten;
		public int sprite_adolescent;
		public int sprite_adult;
		public int sprite_senior;
		public int sprite_para_adult;
		public string eye_colour;
		public object eye_colour2;
		public bool reverse;
		public string white_patches;
		public string vitiligo;
		public string points;
		public string white_patches_tint;
		public string pattern;
		public string tortie_base;
		public string tortie_color;
		public string tortie_pattern;
		public string skin;
		public string tint;
		public SkillDict skill_dict;
		public List<string> scars;
		public string accessory;
		public int experience;
		public int dead_moons;
		public List<string> current_apprentice;
		public List<string> former_apprentices;
		public bool df;
		public bool outside;
		public List<string> faded_offspring;
		public int opacity;
		public bool prevent_fading;
		public bool favourite;
	}

	public class SkillDict
	{
		public string primary;
		public string secondary;
		public string hidden;
	}


}

namespace ClanGenModTool
{
	public static class Background
	{
		public static string[] backgrounds = { 
						"clan_founder",
						"clanborn",
						"halfborn1", "halfborn2",
						"outsider_roots1",
						"outsider_roots2",
						"loner1",
						"loner2",
						"loner3",
						"loner4",
						"kittypet1",
						"kittypet2",
						"kittypet3",
						"kittypet4",
						"rogue1",
						"rogue2",
						"rogue3",
						"abandoned1",
						"abandoned2",
						"abandoned3",
						"abandoned4",
						"otherclan1",
						"otherclan2",
						"otherclan3",
						"otherclan4",
						"disgraced1",
						"disgraced2",
						"disgraced3",
						"retired_leader",
						"medicine_cat",
						"ostracized_warrior",
						"refugee1",
						"refugee2",
						"refugee3",
						"refugee4",
						"refugee5",
						"refugee6",
						"tragedy_survivor1",
						"tragedy_survivor2",
						"tragedy_survivor3",
						"tragedy_survivor4",
						"wandering_healer1",
						"wandering_healer2",
						"guided1",
						"guided2",
						"guided3",
						"guided4",
						"orphaned1",
						"orphaned2",
						"orphaned3",
						"orphaned4",
						"orphaned5",
						"orphaned6",
						"outsider1",
						"outsider2",
						"outsider3"
		};
	}
}