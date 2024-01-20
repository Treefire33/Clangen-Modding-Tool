using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public class Outcome
	{
		public string text;
		public int exp;
		public int weight;
		public List<string> stat_skill;
		public List<string> stat_trait;
		public List<string> can_have_stat;
		public List<string> lost_cats;
		public List<string> dead_cats;
		public int outsider_rep;
		public int other_clan_rep;
		public List<Injury> injury;
		public HistoryText history_text;
		public List<Relationship> relationships;
		public List<List<string>> new_cat;
		public string art;
		public string art_clean;
	}

	public class HistoryText
	{
		public string scar;
		public string reg_death;
		public string lead_death;
	}

	public class Injury
	{
		public List<string> cats;
		public List<string> injuries;
		public bool no_results;
		public List<string> scars;
	}

	public class Relationship
	{
		public List<string> cats_to;
		public List<string> cats_from;
		public bool mutual;
		public List<string> values;
		public int amount;
	}

	public class MinMaxStatus
	{
		public List<int> apprentice;
		[JsonProperty("medicine cat apprentice")]
		public List<int> medicine_cat_apprentice;
		[JsonProperty("medicine cat")]
		public List<int> medicine_cat;
		public List<int> deputy;
		public List<int> warrior;
		public List<int> leader;
		[JsonProperty("healer cats")]
		public List<int> healer_cats;
		[JsonProperty("normal adult")]
		public List<int> normal_adult;
		[JsonProperty("all apprentices")]
		public List<int> all_apprentices;
	}

	public class Patrol
	{
		public string patrol_id;
		public List<string> biome;
		public List<string> season;
		public List<string> types;
		public List<string> tags;
		public string patrol_art;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string patrol_art_clean;
		public int min_cats;
		public int max_cats;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public MinMaxStatus min_max_status;
		public int weight;
		public int chance_of_success;
		public string intro_text;
		public string decline_text;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<string> relationship_constraint;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<string> pl_skill_constraint;
		public List<Outcome> success_outcomes;
		public List<Outcome> fail_outcomes;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Outcome> antag_fail_outcomes;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Outcome> antag_success_outcomes;

		public Patrol()
		{
			/*patrol_id = "";
			biome = ["Any"];
			season = ["Any"];
			types = ["Any"];
			tags = [];*/
		}

		public void DefaultPatrol()
		{
			patrol_id = "default_patrol_obj";
			biome = ["Any"];
			season = ["Any"];
			types = ["hunting"];
			tags = [];
			patrol_art = "gen_bord_intro";
			min_cats = 1;
			max_cats = 6;
			weight = 20;
			chance_of_success = 50;
			intro_text = "The patrol stumbles upon some placeholder text";
			decline_text = "The patrol ignores it and continues on with their day.";
			success_outcomes = [new SuccessOutcome { text="successful_patrol", exp=0, weight=0 }];
			fail_outcomes = [new FailOutcome { text = "failed_patrol", exp = 0, weight = 0 }];
		}
	}
}
