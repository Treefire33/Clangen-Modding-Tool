using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClanGenModTool.Utilities
{
	public class FromJsonTo
	{
		public static void GenerateDictEntryForFacet(string p)
		{
			//{"lonesome", [(0, 8), (0, 8), (0, 8), (0, 8)]}
			dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(p));
			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				Console.WriteLine(descriptor.Name);
				foreach(string trait in Constants.traits)
				{
					dynamic currentTrait = descriptor.GetValue(obj)[trait];
					//Console.WriteLine(currentTrait["lawfulness"]);
					//Console.WriteLine(trait + "\n" + );
					string finalLine = "";
					try
					{
						finalLine += $"{{\"{trait}\", ";
						finalLine += $"[({descriptor.GetValue(obj)[trait].lawfulness[0]}, {descriptor.GetValue(obj)[trait].lawfulness[1]}), ";
						finalLine += $"({descriptor.GetValue(obj)[trait].sociability[0]}, {descriptor.GetValue(obj)[trait].sociability[1]}), ";
						finalLine += $"({descriptor.GetValue(obj)[trait].aggression[0]}, {descriptor.GetValue(obj)[trait].aggression[1]}), ";
						finalLine += $"({descriptor.GetValue(obj)[trait].stability[0]}, {descriptor.GetValue(obj)[trait].stability[1]})";
						finalLine += "]},";
						Console.WriteLine(finalLine);
					} catch (Exception ex) {}
				}
			}
		}
	}
}
