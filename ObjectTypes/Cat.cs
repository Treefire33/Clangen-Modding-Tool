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
		public string? parent1;
		public string? parent2;
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
		public string? eye_colour2;
		public bool reverse;
		public string? white_patches;
		public string? vitiligo;
		public string? points;
		public string white_patches_tint;
		public string? pattern;
		public string? tortie_base;
		public string? tortie_color;
		public string? tortie_pattern;
		public string skin;
		public string tint;
		public SkillDict skill_dict;
		public List<string> scars;
		public string? accessory;
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
		public string? secondary;
		public string? hidden;
	}
}