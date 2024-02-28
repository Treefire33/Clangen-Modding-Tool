using ClanGenModTool.Util;
using static ClanGenModTool.Util.Utils;
using static ClanGenModTool.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes;

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
	public string? mentor;
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

	public static Cat Random(string id)
	{
			return new Cat
			{
				ID = id,
				name_prefix = "random",
				name_suffix = "cat",
				specsuffix_hidden = false,
				gender = new List<string> { "male", "female"}.PickRandom(),
				gender_align = new List<string> { "male", "female", "nonbinary" }.PickRandom(),
				birth_cooldown = 0,
				status = Statuses.PickRandom(),
				backstory = Backgrounds.PickRandom(),
				moons = Utils.Random.Next(0, 150),
				trait = Traits.PickRandom(),
				facets = "8,8,8,8",
				adoptive_parents = [],
				former_mentor = [],
				patrol_with_mentor = 0,
				mate = [],
				previous_mates = [],
				dead = false,
				paralyzed = false,
				no_kits = false,
				exiled = false,
				pelt_name = PeltNames.PickRandom(),
				pelt_color = PeltColours.PickRandom(),
				pelt_length = new List<string> { "short", "medium", "long" }.PickRandom(),
				sprite_kitten = 0,
				sprite_adolescent = 3,
				sprite_adult = 6,
				sprite_senior = 12,
				sprite_para_adult = 15,
				eye_colour = EyeColours.PickRandom(),
				reverse = false,
				white_patches_tint = "none",
				skin = SkinColours.PickRandom(),
				tint = PeltTints.PickRandom(),
				skill_dict = new SkillDict { primary = $"{Skills.PickRandom()},{Utils.Random.Next(1, 29)},False" },
				scars = [],
				experience = Utils.Random.Next(0, 321),
				dead_moons = 0,
				current_apprentice = [],
				former_apprentices = [],
				df = false,
				outside = false,
				faded_offspring = [],
				opacity = 100,
				prevent_fading = false,
				favourite = false
			};
		}

	public static Cat Default(string id)
	{
			return new Cat
			{
				ID = id,
				name_prefix = "new",
				name_suffix = "cat",
				specsuffix_hidden = false,
				gender = "male",
				gender_align = "female",
				birth_cooldown = 0,
				status = "warrior",
				backstory = "clan_founder",
				moons = 0,
				trait = "troublesome",
				facets = "8,8,8,8",
				adoptive_parents = [],
				former_mentor = [],
				patrol_with_mentor = 0,
				mate = [],
				previous_mates = [],
				dead = false,
				paralyzed = false,
				no_kits = false,
				exiled = false,
				pelt_name = PeltNames.First(),
				pelt_color = PeltColours.First(),
				pelt_length = "short",
				sprite_kitten = 0,
				sprite_adolescent = 3,
				sprite_adult = 6,
				sprite_senior = 9,
				sprite_para_adult = 18,
				eye_colour = EyeColours.First(),
				reverse = false,
				white_patches_tint = "none",
				skin = SkinColours.First(),
				tint = PeltTints.First(),
				skill_dict = new SkillDict { primary = "HUNTER,10,False" },
				scars = [],
				experience = 0,
				dead_moons = 0,
				current_apprentice = [],
				former_apprentices = [],
				df = false,
				outside = false,
				faded_offspring = [],
				opacity = 100,
				prevent_fading = false,
				favourite = false
			};
		}
}

public class SkillDict
{
	public string primary;
	public string? secondary;
	public string? hidden;
}