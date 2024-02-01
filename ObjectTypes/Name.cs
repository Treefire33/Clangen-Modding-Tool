using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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
		public List<List<string>> Enumerate()
		{
			return new List<List<string>>
			{
				Forest, Beach, Plains, Mountainous, Wetlands, Desert
			};
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:
				return nameof(Forest);
				case 1:
				return nameof(Beach);
				case 2:
				return nameof(Plains);
				case 3:
				return nameof(Mountainous);
				case 4:
				return nameof(Wetlands);
				case 5:
				return nameof(Desert);
				default:
				return "unknown";
			}
		}
	}

	public class BiomeSuffixes
	{
		public List<string> Forest;
		public List<string> Beach;
		public List<string> Plains;
		public List<string> Mountainous;
		public List<string> Wetlands;
		public List<string> Desert;
		public List<List<string>> Enumerate()
		{
			return new List<List<string>>
			{
				Forest, Beach, Plains, Mountainous, Wetlands, Desert
			};
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:
				return nameof(Forest);
				case 1:
				return nameof(Beach);
				case 2:
				return nameof(Plains);
				case 3:
				return nameof(Mountainous);
				case 4:
				return nameof(Wetlands);
				case 5:
				return nameof(Desert);
				default:
				return "unknown";
			}
		}
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
		public List<List<string>> Enumerate()
		{
			return [WHITE, PALEGREY, SILVER, GREY,
					DARKGREY, GHOST, PALEGINGER, GOLDEN,
					GINGER, DARKGINGER, SIENNA, CREAM,
					LIGHTBROWN, LILAC, BROWN];
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0: return "White";
				case 1: return "Pale Grey";
				case 2: return "Silver";
				case 3: return "Grey";
				case 4: return "Dark Grey";
				case 5: return "Ghost";
				case 6: return "Pale Ginger";
				case 7: return "Golden";
				case 8: return "Ginger";
				case 9: return "Dark Ginger";
				case 10: return "Sienna";
				case 11: return "Cream";
				case 12: return "Light Brown";
				case 13: return "Lilac";
				case 14: return "Brown";
				default: return "unknown";
			}
		}
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
		public List<List<string>> Enumerate()
		{
			return [YELLOW, AMBER, HAZEL, PALEGREEN,
					GREEN, BLUE, DARKBLUE, GREY, CYAN, 
					EMERALD, PALEBLUE, PALEYELLOW, GOLD, 
					HEATHERBLUE, COPPER, SAGE, COBALT, SUNLITICE, 
					GREENYELLOW
			];
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:return "Yellow";
				case 1:return "Amber";
				case 2:return "Hazel";
				case 3:return "Pale Green";
				case 4:return "Green";
				case 5:return "Blue";
				case 6:return "Dark Blue";
				case 7:return "Grey";
				case 8:return "Cyan";
				case 9:return "Emerald";
				case 10:return "Pale Blue";
				case 11:return "Pale Yellow";
				case 12:return "Gold";
				case 13:return "Heather Blue";
				case 14:return "Copper";
				case 15:return "Sage";
				case 16:return "Cobalt";
				case 17:return "Sunlit Ice";
				case 18:return "Green-Yellow";
				default:return "unknown";
			}
		}
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

		public List<string> Enumerate()
		{
			return new List<string>
			{
				newborn,
				kitten,
				apprentice,
				medicinecatapprentice,
				mediatorapprentice,
				leader
			};
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:
				return nameof(newborn);
				case 1:
				return nameof(kitten);
				case 2:
				return nameof(apprentice);
				case 3:
				return nameof(medicinecatapprentice);
				case 4:
				return nameof(mediatorapprentice);
				case 5:
				return nameof(leader);
				default:
				return "unknown";
			}
		}
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
		public List<List<string>> Enumerate()
		{
			List<List<string>> strings =
			[
				solid,
				tabby,
				bengal,
				marbled,
				ticked,
				smoke,
				rosette,
				speckled,
				mackerel,
				classic,
				sokoke,
				agouti,
				masked
			];
			return strings;
		}
		public string LookUp(int index)
		{
			switch(index)
			{
				case 0:
				return nameof(solid);
				case 1:
				return nameof(tabby);
				case 2:
				return nameof(bengal);
				case 3:
				return nameof(marbled);
				case 4:
				return nameof(ticked);
				case 5:
				return nameof(smoke);
				case 6:
				return nameof(rosette);
				case 7:
				return nameof(speckled);
				case 8:
				return nameof(mackerel);
				case 9:
				return nameof(classic);
				case 10:
				return nameof(sokoke);
				case 11:
				return nameof(agouti);
				case 12:
				return nameof(masked);
				default:
				return "unknown";
			}
		}
	}
}
