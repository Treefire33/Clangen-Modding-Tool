using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public class AntagFailOutcome
	{
		public string text;
		public int exp;
		public int weight;
		public int outsider_rep;
		public int other_clan_rep;
	}

	public class AntagSuccessOutcome
	{
		public string text;
		public int exp;
		public int weight;
		public int outsider_rep;
		public int other_clan_rep;
	}

	public class FailOutcome
	{
		public string text;
		public int exp;
		public int weight;
		public List<string> dead_cats;
		public HistoryText history_text;
		public List<Relationship> relationships;
		public List<Injury> injury;
		public List<string> stat_trait;
		public int outsider_rep;
		public int other_clan_rep;
		public List<string> stat_skill;
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

	public class MinMaxStatus
	{
		[JsonProperty("normal adult")]
		public List<int> normaladult;
		public List<int> leader;
		public List<int> apprentice;

		[JsonProperty("all apprentices")]
		public List<int> allapprentices;
	}

	public class Relationship
	{
		public List<string> cats_to;
		public List<string> cats_from;
		public bool mutual;
		public List<string> values;
		public int amount;
	}

	public class Patrol
	{
		public string patrol_id;
		public List<string> biome;
		public List<string> season;
		public List<string> types;
		public List<string> tags;
		public string patrol_art;
		public int min_cats;
		public int max_cats;
		public MinMaxStatus min_max_status;
		public int weight;
		public string intro_text;
		public string decline_text;
		public int chance_of_success;
		public List<SuccessOutcome> success_outcomes;
		public List<FailOutcome> fail_outcomes;
		public List<AntagFailOutcome> antag_fail_outcomes;
		public List<AntagSuccessOutcome> antag_success_outcomes;

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
			types = ["Any"];
			tags = [];
			patrol_art = "gen_bord_intro";
			min_cats = 0;
			max_cats = 0;
			weight = 0;
			intro_text = string.Empty;
			decline_text = string.Empty;
			success_outcomes = [new SuccessOutcome { text="successful_patrol", exp=0, weight=0 }];
			fail_outcomes = [new FailOutcome { text = "failed_patrol", exp = 0, weight = 0 }];
			antag_success_outcomes = [new AntagSuccessOutcome { text = "successful_patrol", exp = 0, weight = 0 }];
			antag_fail_outcomes = [new AntagFailOutcome { text = "failed_patrol", exp = 0, weight = 0 }];
		}
	}

	public class SuccessOutcome
	{
		public string text;
		public int exp;
		public int weight;
		public List<Relationship> relationships;
		public List<string> stat_skill;
		public List<string> stat_trait;
		public List<List<string>> new_cat;
		public int outsider_rep;
		public List<Injury> injury;
		public int other_clan_rep;
		public List<string> can_have_stat;
	}
}
