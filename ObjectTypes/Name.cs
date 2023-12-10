using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.ObjectTypes
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class BiomePrefixes
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Forest;
		public List<string> Beach;
		public List<string> Plains;
		public List<string> Mountainous;
		public List<string> Wetlands;
		public List<string> Desert;
	}

	public class BiomeSuffixes
	{
		public List<string> Forest;
		public List<string> Beach;
		public List<string> Plains;
		public List<string> Mountainous;
		public List<string> Wetlands;
		public List<string> Desert;
	}

	public class ColourPrefixes
	{
		public List<string> WHITE;
		public List<string> PALEGREY;
		public List<string> SILVER;
		public List<string> GREY;
		public List<string> DARKGREY;
		public List<string> GHOST;
		public List<string> PALEGINGER;
		public List<string> GOLDEN;
		public List<string> GINGER;
		public List<string> DARKGINGER;
		public List<string> SIENNA;
		public List<string> CREAM;
		public List<string> LIGHTBROWN;
		public List<string> LILAC;
		public List<string> BROWN;

		[JsonProperty("GOLDEN-BROWN")]
		public List<string> GOLDENBROWN;
		public List<string> DARKBROWN;
		public List<string> CHOCOLATE;
		public List<string> BLACK;
	}

	public class EyePrefixes
	{
		public List<string> YELLOW;
		public List<string> AMBER;
		public List<string> HAZEL;
		public List<string> PALEGREEN;
		public List<string> GREEN;
		public List<string> BLUE;
		public List<string> DARKBLUE;
		public List<string> GREY;
		public List<string> CYAN;
		public List<string> EMERALD;
		public List<string> PALEBLUE;
		public List<string> PALEYELLOW;
		public List<string> GOLD;
		public List<string> HEATHERBLUE;
		public List<string> COPPER;
		public List<string> SAGE;
		public List<string> COBALT;
		public List<string> SUNLITICE;
		public List<string> GREENYELLOW;
	}

	public class PeltSuffixes
	{
		public List<string> TwoColour;
		public List<string> Tabby;
		public List<string> Marbled;
		public List<string> Speckled;
		public List<string> Bengal;
		public List<string> Tortie;
		public List<string> Rosette;
		public List<string> Calico;
		public List<string> Smoke;
		public List<string> Ticked;
		public List<string> Mackerel;
		public List<string> Classic;
		public List<string> Sokoke;
		public List<string> Agouti;
		public List<string> Singlestripe;
		public List<string> Masked;
		public List<List<string>> Enumerate()
		{
			List<List<string>> strings =
			[
				TwoColour, 
				Tabby, 
				Marbled, 
				Speckled, 
				Bengal, 
				Tortie, 
				Rosette,
				Calico, 
				Smoke, 
				Ticked, 
				Mackerel, 
				Classic, 
				Sokoke, 
				Agouti, 
				Singlestripe, 
				Masked
			];
			return strings;
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:
				return "TwoColour";
				case 1:
				return "Tabby";
				case 2:
				return "Marbled";
				case 3:
				return "Speckled";
				case 4:
				return "Bengal";
				case 5:
				return "Tortie";
				case 6:
				return "Rosette";
				case 7:
				return "Calico";
				case 8:
				return "Smoke";
				case 9:
				return "Ticked";
				case 10:
				return "Mackerel";
				case 11:
				return "Classic";
				case 12:
				return "Sokoke";
				case 13:
				return "Agouti";
				case 14:
				return "Singlestripe";
				case 15:
				return "Masked";
				default:
				return "unknown";
			}
		}
	}

	public class Name
	{
		public SpecialSuffixes special_suffixes;
		public List<string> normal_suffixes;
		public PeltSuffixes pelt_suffixes;
		public TortiePeltSuffixes tortie_pelt_suffixes;
		public List<string> normal_prefixes;
		public ColourPrefixes colour_prefixes;
		public BiomePrefixes biome_prefixes;
		public BiomeSuffixes biome_suffixes;
		public EyePrefixes eye_prefixes;
		public List<string> loner_names;
		public List<string> animal_suffixes;
		public List<string> animal_prefixes;
		public List<string> inappropriate_names;
	}

	public class SpecialSuffixes
	{
		public string newborn;
		public string kitten;
		public string apprentice;

		[JsonProperty("medicine cat apprentice")]
		public string medicinecatapprentice;

		[JsonProperty("mediator apprentice")]
		public string mediatorapprentice;
		public string leader;
	}

	public class TortiePeltSuffixes
	{
		public List<string> solid;
		public List<string> tabby;
		public List<string> bengal;
		public List<string> marbled;
		public List<string> ticked;
		public List<string> smoke;
		public List<string> rosette;
		public List<string> speckled;
		public List<string> mackerel;
		public List<string> classic;
		public List<string> sokoke;
		public List<string> agouti;
		public List<string> masked;
	}
}
