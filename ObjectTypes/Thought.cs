using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes;

public class Thought
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public string id;
	public List<string> thoughts;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> random_status_constraint;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> random_living_status;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> relationship_constraint;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> random_age_constraint;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> main_backstory_constraint;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> main_trait_constraint;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public List<string> main_status_constraint;

	public void DefaultThought()
	{
		id = "newThoughtList";
		thoughts = new List<string>();
		random_status_constraint = new List<string>();
		random_living_status = new List<string>();
		relationship_constraint = new List<string>();
		random_age_constraint = new List<string>();
		main_backstory_constraint = new List<string>();
		main_trait_constraint = new List<string>();
		main_status_constraint = new List<string>();
	}
}