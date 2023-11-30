using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	public class Thought
	{
		public string id;
		public List<string> thoughts;
		public List<string> random_status_constraint;
		public List<string> random_living_status;
		public List<string> relationship_constraint;
		public List<string> random_age_constraint;
		public List<string> main_backstory_constraint;
		public List<string> main_trait_constraint;
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
}
