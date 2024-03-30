using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClanGenModTool.ObjectTypes;
using Newtonsoft.Json;

namespace ClanGenModTool.ObjectTypes;

public class Condition
{
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public Dictionary<string, Injury>? injuries;
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public Dictionary<string, Illness>? illnesses;
	[JsonProperty(propertyName: "permanent condition", NullValueHandling = NullValueHandling.Ignore)]
	public Dictionary<string, PermCondition>? permanentConditions;
} 

public class Injury
{
	public string severity;
	public int mortality;
	public int duration;
	public int moon_start;
	public List<object> illness_infectiousness;
	public List<Risk> risks;
	public string? complication;
	public List<string> cause_permanent;
	public bool event_triggered;
}

public class Illness
{
	public string severity;
	public int mortality;
	public int infectiousness;
	public int duration;
	public int moon_start;
	public List<Risk> risks;
	public bool event_triggered;
}

public class PermCondition
{
	public string severity;
	public bool born_with;
	public int moons_until;
	public int moon_start;
	public int mortality;
	public List<object> illness_infectiousness;
	public List<Risk> risks;
	public string? complication; //so this is always null. why? clangen devs answer me!
	public bool event_triggered;
}

public class Risk
{
	public string name;
	public int chance;
}

public class PresetIllnesses
{
	//All presets have their mortality set to the one listed under "young adult".
	//This is because every illness has a different mortality for each age range.
	public static Illness seizure = new() { severity = "severe", duration = 1, infectiousness = 0, mortality = 20, risks = new List<Risk>() { } };
	public static Illness diarrhea = new() { severity = "major", duration = 2, infectiousness = 0, mortality = 10, risks = new List<Risk>() { } };
	public static Illness fleas = new() { severity = "minor", duration = 4, infectiousness = 15, mortality = 0, risks = new List<Risk>() { new Risk() { name = "torn pelt", chance = 20 } } };
	public static Illness greencough = new() { severity = "severe", duration = 3, infectiousness = 30, mortality = 8, risks = new List<Risk>() { new Risk() { name = "yellowcough", chance = 60 } } };
	public static Illness kittencough = new() { severity = "severe", duration = 3, infectiousness = 15, mortality = 0, risks = new List<Risk>() { new Risk() { name = "whitecough", chance = 10 } } };
	public static Illness anInfectedWound = new() { severity = "major", duration = 3, infectiousness = 0, mortality = 8, risks = new List<Risk>() { new Risk() { name = "a festering wound", chance = 10 } } };
	public static Illness carrionplaceDisease = new() { severity = "severe", duration = 3, infectiousness = 0, mortality = 2, risks = new List<Risk>() { } };
	public static Illness redcough = new() { severity = "severe", duration = 50, infectiousness = 0, mortality = 2, risks = new List<Risk>() { } };
	public static Illness runningNose = new() { severity = "minor", duration = 3, infectiousness = 0, mortality = 0, risks = new List<Risk>() { new Risk() { name = "whitecough", chance = 15 } } };
	public static Illness whitecough = new() { severity = "major", duration = 3, infectiousness = 30, mortality = 60, risks = new List<Risk>() { new Risk() { name = "greencough", chance = 18 } } };
	public static Illness yellowcough = new() { severity = "major", duration = 3, infectiousness = 30, mortality = 5, risks = new List<Risk>() { new Risk() { name = "redcough", chance = 80 } } };
	public static Illness aFesteringWound = new() { severity = "severe", duration = 3, infectiousness = 0, mortality = 3, risks = new List<Risk>() { } };
	public static Illness heatStroke = new() { severity = "severe", duration = 1, infectiousness = 0, mortality = 5, risks = new List<Risk>() { } };
	public static Illness heatExhaustion = new() { severity = "major", duration = 2, infectiousness = 0, mortality = 0, risks = new List<Risk>() { new Risk() { name = "heat stroke", chance = 5 } } };
	public static Illness stomachache = new() { severity = "minor", duration = 2, infectiousness = 0, mortality = 0, risks = new List<Risk>() { new Risk() { name = "diarrhea", chance = 8 } } };
	public static Illness constantNightmares = new() { severity = "major", duration = 2, infectiousness = 0, mortality = 0, risks = new List<Risk>() { } };
	public static Illness griefStricken = new() { severity = "major", duration = 5, infectiousness = 0, mortality = 0, risks = new List<Risk>() { new Risk() { name = "lasting grief", chance = 100 }, new Risk() { name = "constant nightmares", chance = 15 } } };
	public static Illness malnourished = new() { severity = "minor", duration = 100, infectiousness = 0, mortality = 0, risks = new List<Risk>() { } };
	public static Illness starving = new() { severity = "severe", duration = 100, infectiousness = 0, mortality = 0, risks = new List<Risk>() { } };
	public static Dictionary<string, Illness> IllnessPresetDict = new()
	{
		{"seizure", seizure},
		{"diarrhea", diarrhea},
		{"fleas", fleas},
		{"greencough", greencough},
		{"kittencough", kittencough},
		{"an infected wound", anInfectedWound},
		{"carrionplace disease", carrionplaceDisease},
		{"redcough", redcough},
		{"running nose", runningNose},
		{"whitecough", whitecough},
		{"yellowcough", yellowcough},
		{"a festering wound", aFesteringWound},
		{"heat stroke", heatStroke},
		{"heat exhaustion", heatExhaustion},
		{"stomachache", stomachache},
		{"constant nightmares", constantNightmares},
		{"grief stricken", griefStricken},
		{"malnourished", malnourished},
		{"starving", starving},
	};
}

public class PresetInjuries
{
	public static Injury clawwound = new() { severity = "major", duration = 3, mortality = 50, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury bitewound = new() { severity = "major", duration = 3, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury catbite = new() { severity = "major", duration = 2, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury beakbite = new() { severity = "major", duration = 2, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury snakebite = new() { severity = "major", duration = 2, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury ratbite = new() { severity = "major", duration = 2, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 }, new Risk() { name = "a festering wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury tickbites = new() { severity = "minor", duration = 2, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 40 } }, cause_permanent = new List<string>() { } };
	public static Injury bloodloss = new() { severity = "minor", duration = 1, mortality = 20, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury brokenjaw = new() { severity = "severe", duration = 4, mortality = 50, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { "crooked jaw" } };
	public static Injury brokenbone = new() { severity = "severe", duration = 5, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { "weak leg", "twisted leg" } };
	public static Injury mangledleg = new() { severity = "severe", duration = 3, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { "lost a leg", "weak leg", "twisted leg" } };
	public static Injury dislocatedjoint = new() { severity = "major", duration = 2, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { "constant joint pain" } };
	public static Injury jointpain = new() { severity = "minor", duration = 2, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury sprain = new() { severity = "major", duration = 2, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury mangledtail = new() { severity = "severe", duration = 3, mortality = 60, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { "lost their tail" } };
	public static Injury bruises = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury crackedpads = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 20 } }, cause_permanent = new List<string>() { } };
	public static Injury sore = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury phantompain = new() { severity = "major", duration = 2, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury scrapes = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 30 } }, cause_permanent = new List<string>() { } };
	public static Injury smallcut = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 40 } }, cause_permanent = new List<string>() { } };
	public static Injury tornpelt = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 25 } }, cause_permanent = new List<string>() { } };
	public static Injury tornear = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 25 } }, cause_permanent = new List<string>() { "partial hearing loss" } };
	public static Injury frostbite = new() { severity = "major", duration = 2, mortality = 25, risks = new List<Risk>() { new Risk() { name = "running nose", chance = 4 } }, cause_permanent = new List<string>() { "lost a leg", "lost their tail" } };
	public static Injury recoveringfrombirth = new() { severity = "major", duration = 3, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 15 } }, cause_permanent = new List<string>() { } };
	public static Injury waterintheirlungs = new() { severity = "major", duration = 3, mortality = 60, risks = new List<Risk>() { new Risk() { name = "running nose", chance = 10 }, new Risk() { name = "whitecough", chance = 20 } }, cause_permanent = new List<string>() { "raspy lungs" } };
	public static Injury burn = new() { severity = "major", duration = 2, mortality = 50, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 25 } }, cause_permanent = new List<string>() { } };
	public static Injury severeburn = new() { severity = "severe", duration = 4, mortality = 30, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 15 } }, cause_permanent = new List<string>() { "lost their tail", "lost a leg" } };
	public static Injury shock = new() { severity = "major", duration = 1, mortality = 30, risks = new List<Risk>() { new Risk() { name = "lingering shock", chance = 15 } }, cause_permanent = new List<string>() { } };
	public static Injury lingeringshock = new() { severity = "major", duration = 12, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { "recurring shock" } };
	public static Injury shivering = new() { severity = "minor", duration = 2, mortality = 50, risks = new List<Risk>() { new Risk() { name = "frostbite", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury dehydrated = new() { severity = "major", duration = 1, mortality = 50, risks = new List<Risk>() { new Risk() { name = "heat exhaustion", chance = 10 } }, cause_permanent = new List<string>() { } };
	public static Injury headdamage = new() { severity = "major", duration = 5, mortality = 40, risks = new List<Risk>() { new Risk() { name = "headache", chance = 20 }, new Risk() { name = "severe headache", chance = 20 } }, cause_permanent = new List<string>() { "failing eyesight", "one bad eye", "blind", "constantly dizzy", "partial hearing loss", "persistent headaches" } };
	public static Injury damagedeyes = new() { severity = "major", duration = 6, mortality = 70, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 4 } }, cause_permanent = new List<string>() { "blind", "one bad eye", "failing eyesight" } };
	public static Injury quilledbyaporcupine = new() { severity = "minor", duration = 2, mortality = 50, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 5 } }, cause_permanent = new List<string>() { } };
	public static Injury brokenback = new() { severity = "severe", duration = 8, mortality = 30, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 10 } }, cause_permanent = new List<string>() { "paralyzed" } };
	public static Injury poisoned = new() { severity = "major", duration = 2, mortality = 20, risks = new List<Risk>() { new Risk() { name = "redcough", chance = 10 } }, cause_permanent = new List<string>() { } };
	public static Injury beesting = new() { severity = "minor", duration = 2, mortality = 50, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury headache = new() { severity = "minor", duration = 1, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury severeheadache = new() { severity = "major", duration = 1, mortality = 0, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };
	public static Injury pregnant = new() { severity = "major", duration = 2, mortality = 40, risks = new List<Risk>() { }, cause_permanent = new List<string>() { } };

	public static Dictionary<string, Injury> InjuryPresetDict = new()
	{
		{ "claw-wound", clawwound },
		{ "bite-wound", bitewound },
		{ "cat bite", catbite },
		{ "beak bite", beakbite },
		{ "snake bite", snakebite },
		{ "rat bite", ratbite },
		{ "tick bites", tickbites },
		{ "blood loss", bloodloss },
		{ "broken jaw", brokenjaw },
		{ "broken bone", brokenbone },
		{ "mangled leg", mangledleg },
		{ "dislocated joint", dislocatedjoint },
		{ "joint pain", jointpain },
		{ "sprain", sprain },
		{ "mangled tail", mangledtail },
		{ "bruises", bruises },
		{ "cracked pads", crackedpads },
		{ "sore", sore },
		{ "phantom pain", phantompain },
		{ "scrapes", scrapes },
		{ "small cut", smallcut },
		{ "torn pelt", tornpelt },
		{ "torn ear", tornear },
		{ "frostbite", frostbite },
		{ "recovering from birth", recoveringfrombirth },
		{ "water in their lungs", waterintheirlungs },
		{ "burn", burn },
		{ "severe burn", severeburn },
		{ "shock", shock },
		{ "lingering shock", lingeringshock },
		{ "shivering", shivering },
		{ "dehydrated", dehydrated },
		{ "head damage", headdamage },
		{ "damaged eyes", damagedeyes },
		{ "quilled by a porcupine", quilledbyaporcupine },
		{ "broken back", brokenback },
		{ "poisoned", poisoned },
		{ "bee sting", beesting },
		{ "headache", headache },
		{ "severe headache", severeheadache },
		{ "pregnant", pregnant },
	};
}

public class PresetPermConditions
{
	public static PermCondition crookedjaw = new() { severity = "major", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { }, };
	public static PermCondition lostaleg = new() { severity = "major", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 70 }, new Risk() { name = "phantom pain", chance = 20 }, new Risk() { name = "sore", chance = 20 } }, };
	public static PermCondition bornwithoutaleg = new() { severity = "major", moons_until = 0, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "sore", chance = 20 }, new Risk() { name = "joint pain", chance = 40 } }, };
	public static PermCondition weakleg = new() { severity = "minor", moons_until = 3, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "sore", chance = 20 }, new Risk() { name = "joint pain", chance = 40 } }, };
	public static PermCondition twistedleg = new() { severity = "major", moons_until = 1, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "sore", chance = 20 }, new Risk() { name = "joint pain", chance = 40 } }, };
	public static PermCondition losttheirtail = new() { severity = "minor", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 70 }, new Risk() { name = "phantom pain", chance = 10 } }, };
	public static PermCondition bornwithoutatail = new() { severity = "minor", moons_until = 0, born_with = true, mortality = 0, risks = new List<Risk>() { }, };
	public static PermCondition paralyzed = new() { severity = "severe", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "an infected wound", chance = 60 }, new Risk() { name = "torn pelt", chance = 30 }, new Risk() { name = "sore", chance = 20 }, new Risk() { name = "joint pain", chance = 20 } }, };
	public static PermCondition raspylungs = new() { severity = "minor", moons_until = 3, born_with = true, mortality = 80, risks = new List<Risk>() { new Risk() { name = "whitecough", chance = 20 } }, };
	public static PermCondition wastingdisease = new() { severity = "major", moons_until = 1, born_with = true, mortality = 75, risks = new List<Risk>() { new Risk() { name = "running nose", chance = 10 }, new Risk() { name = "yellowcough", chance = 50 }, new Risk() { name = "redcough", chance = 60 } }, };
	public static PermCondition blind = new() { severity = "major", moons_until = 1, born_with = false, mortality = 0, risks = new List<Risk>() { }, };
	public static PermCondition onebadeye = new() { severity = "major", moons_until = 2, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "failing eyesight", chance = 80 } }, };
	public static PermCondition failingeyesight = new() { severity = "major", moons_until = 3, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "blind", chance = 50 } }, };
	public static PermCondition partialhearingloss = new() { severity = "minor", moons_until = 2, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "deaf", chance = 80 } }, };
	public static PermCondition deaf = new() { severity = "major", moons_until = 1, born_with = true, mortality = 0, risks = new List<Risk>() { }, };
	public static PermCondition constantjointpain = new() { severity = "minor", moons_until = 2, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "joint pain", chance = 10 } }, };
	public static PermCondition seizureprone = new() { severity = "major", moons_until = 3, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "seizure", chance = 10 } }, };
	public static PermCondition allergies = new() { severity = "minor", moons_until = 2, born_with = true, mortality = 0, risks = new List<Risk>() { new Risk() { name = "running nose", chance = 20 } }, };
	public static PermCondition constantlydizzy = new() { severity = "major", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "headache", chance = 20 } }, };
	public static PermCondition recurringshock = new() { severity = "major", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "constant nightmares", chance = 20 } }, };
	public static PermCondition lastinggrief = new() { severity = "minor", moons_until = 0, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "grief stricken", chance = 50 } }, };
	public static PermCondition persistentheadaches = new() { severity = "major", moons_until = 4, born_with = false, mortality = 0, risks = new List<Risk>() { new Risk() { name = "severe headache", chance = 20 }, new Risk() { name = "headache", chance = 20 } }, };
	public static Dictionary<string, PermCondition> PermPresetDict = new()
	{
			{"crooked jaw", crookedjaw},
			{"lost a leg", lostaleg},
			{"born without a leg", bornwithoutaleg},
			{"weak leg", weakleg},
			{"twisted leg", twistedleg},
			{"lost their tail", losttheirtail},
			{"born without a tail", bornwithoutatail},
			{"paralyzed", paralyzed},
			{"raspy lungs", raspylungs},
			{"wasting disease", wastingdisease},
			{"blind", blind},
			{"one bad eye", onebadeye},
			{"failing eyesight", failingeyesight},
			{"partial hearing loss", partialhearingloss},
			{"deaf", deaf},
			{"constant joint pain", constantjointpain},
			{"seizure prone", seizureprone},
			{"allergies", allergies},
			{"constantly dizzy", constantlydizzy},
			{"recurring shock", recurringshock},
			{"lasting grief", lastinggrief},
			{"persistent headaches", persistentheadaches},
	};
}