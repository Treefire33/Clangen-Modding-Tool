namespace ClanGenModTool
{
	public static class Constants
	{
		public static string[] backgrounds = {
						"clan_founder",
						"clanborn",
						"halfborn1", "halfborn2",
						"outsider_roots1",
						"outsider_roots2",
						"loner1",
						"loner2",
						"loner3",
						"loner4",
						"kittypet1",
						"kittypet2",
						"kittypet3",
						"kittypet4",
						"rogue1",
						"rogue2",
						"rogue3",
						"abandoned1",
						"abandoned2",
						"abandoned3",
						"abandoned4",
						"otherclan1",
						"otherclan2",
						"otherclan3",
						"otherclan4",
						"disgraced1",
						"disgraced2",
						"disgraced3",
						"retired_leader",
						"medicine_cat",
						"ostracized_warrior",
						"refugee1",
						"refugee2",
						"refugee3",
						"refugee4",
						"refugee5",
						"refugee6",
						"tragedy_survivor1",
						"tragedy_survivor2",
						"tragedy_survivor3",
						"tragedy_survivor4",
						"wandering_healer1",
						"wandering_healer2",
						"guided1",
						"guided2",
						"guided3",
						"guided4",
						"orphaned1",
						"orphaned2",
						"orphaned3",
						"orphaned4",
						"orphaned5",
						"orphaned6",
						"outsider1",
						"outsider2",
						"outsider3"
		};

		public static string[] statuses =
		{
			"leader",
			"deputy",
			"medicine cat",
			"medicine apprentice",
			"warrior",
			"apprentice",
			"queen",
			"elder",
			"kitten",
			"mediator",
			"mediator apprentice",
			"newborn"
		};

		public static string[] traits =
		{
			"troublesome", "lonesome", "fierce", "bloodthirsty",
			"cold", "childish", "playful", "charismatic",
			"bold", "daring", "nervous", "righteous",
			"insecure", "strict", "compassionate", "thoughtful",
			"ambitious", "confident", "adventurous", "calm",
			"gloomy", "faithful", "loving", "loyal", "responsible",
			"shameless", "sneaky", "strange", "vengeful",
			"wise", "arrogant", "competitive", "grumpy",
			"cunning", "oblivious", "flamboyant", "rebellious",
			"sincere", "careful"
		};

		public static string[] kitTraits =
		{
			"unruly", "shy", "implusive", "bullying",
			"attention-seeker", "charming", "self-conscious", "noisy",
			"skittish", "daydreamer", "fearless", "sweet", "know-it-all",
			"polite", "bossy"
		};

		public static string[] skills =
		{
			"TEACHER", "HUNTER", "FIGHTER", "CLIMBER",
			"SWIMMER", "SPEAKER", "MEDIATOR", "CLEVER",
			"INSIGHTFUL", "SENSE", "KIT", "HEALER",
			"STORY", "LORE", "CAMP", "STAR",
			"OMEN", "DREAM", "CLAIRVOYANT", "PROPHET",
			"GHOST", "RUNNER", "DARK"
		};

		public static Dictionary<string, List<(int, int)>> traitRanges = new Dictionary<string, List<(int, int)>>
		{
			{"troublesome", [(0, 8), (0, 8), (0, 8), (0, 8)]}, {"lonesome", [(0, 8), (0, 8), (0, 8), (8, 16)]},
			{"fierce", [(0, 8), (0, 8), (8, 11), (0, 8)]}, {"bloodthirsty", [(0, 8), (0, 8), (12, 16), (0, 8)]},
			{"cold", [(0, 8), (0, 8), (8, 16), (8, 16)]}, {"childish", [(0, 8), (8, 16), (0, 8), (0, 8)]},
			{"playful", [(0, 8), (8, 16), (0, 8), (8, 16)]}, {"charismatic", [(0, 12), (8, 16), (8, 16), (0, 8)]},
			{"bold", [(0, 8), (8, 16), (8, 12), (8, 16)]}, {"daring", [(0, 8), (8, 16), (13, 16), (8, 16)]},
			{"nervous", [(8, 16), (0, 8), (0, 8), (0, 8)]}, {"righteous", [(8, 16), (0, 8), (0, 8), (8, 16)]},
			{"insecure", [(8, 16), (0, 8), (8, 16), (0, 8)]}, {"strict", [(8, 16), (0, 8), (8, 16), (8, 16)]},
			{"compassionate", [(8, 16), (8, 16), (0, 8), (0, 8)]}, {"thoughtful", [(8, 16), (8, 16), (0, 8), (8, 16)]},
			{"ambitious", [(8, 16), (8, 16), (8, 16), (0, 8)]}, {"confident", [(8, 16), (8, 16), (8, 16), (8, 16)]},
			{"adventurous", [(0, 10), (3, 14), (3, 14), (8, 16)]}, {"calm", [(9, 16), (6, 14), (0, 5), (9, 16)]},
			{"gloomy", [(6, 12), (0, 5), (0, 6), (0, 8)]}, {"faithful", [(10, 16), (5, 11), (0, 6), (3, 13)]},
			{"loving", [(6, 13), (9, 16), (0, 8), (5, 14)]}, {"loyal", [(10, 16), (5, 16), (5, 13), (5, 13)]},
			{"responsible", [(8, 16), (5, 16), (0, 7), (4, 10)]}, {"shameless", [(0, 7), (4, 14), (7, 12), (9, 16)]},
			{"sneaky", [(0, 7), (0, 7), (6, 12), (8, 16)]}, {"strange", [(5, 10), (0, 8), (5, 10), (5, 10)]},
			{"vengeful", [(0, 7), (0, 7), (8, 14), (0, 4)]}, {"wise", [(8, 16), (4, 16), (0, 6), (8, 16)]},
			{"arrogant", [(5, 8), (8, 16), (7, 12), (0, 5)]}, {"competitive", [(6, 12), (12, 16), (6, 12), (8, 16)]},
			{"grumpy", [(0, 16), (0, 5), (5, 9), (0, 7)]}, {"cunning", [(0, 5), (8, 16), (0, 6), (8, 16)]},
			{"flamboyant", [(6, 10), (13, 16), (6, 12), (10, 16)]}, {"rebellious", [(0, 4), (0, 16), (6, 10), (10, 16)]},
			{"sincere", [(12, 16), (6, 12), (0, 5), (12, 16)]}, {"careful", [(8, 12), (4, 12), (0, 6), (10, 16)]},
			{"oblivious", [(6, 12), (10, 16), (0, 6), (6, 12)]}
		};

		public static string[] PeltNames =
		[
			"Tabby",
			"Ticked",
			"Mackerel",
			"Classic",
			"Sokoke",
			"Agouti",
			"Speckled",
			"Rosette",
			"SingleColour",
			"TwoColour",
			"Smoke",
			"Singlestripe",
			"Masked",
			"Bengal",
			"Marbled",
			"Tortie",
			"Calico"
		];

		public static string[] TortiePeltNames =
		[
			"tabby",
			"ticked",
			"mackerel",
			"classic",
			"sokoke",
			"agouti",
			"speckled",
			"rosette",
			"single",
			"smoke",
			"singlestripe",
			"masked",
			"bengal",
			"marbled"
		];

		public static string[] PeltColours =
		[
			"WHITE",
			"PALEGREY",
			"SILVER",
			"GREY",
			"DARKGREY",
			"GHOST",
			"BLACK",
			"CREAM",
			"PALEGINGER",
			"GOLDEN",
			"GINGER",
			"DARKGINGER",
			"SIENNA",
			"LIGHTBROWN",
			"LILAC",
			"BROWN",
			"GOLDEN-BROWN",
			"DARKBROWN",
			"CHOCOLATE"
		];

		public static string[] EyeColours =
		[
			"YELLOW",
			"AMBER",
			"HAZEL",
			"PALEGREEN",
			"GREEN",
			"BLUE",
			"DARKBLUE",
			"GREY",
			"CYAN",
			"EMERALD",
			"HEATHERBLUE",
			"SUNLITICE",
			"COPPER",
			"SAGE",
			"COBALT",
			"PALEBLUE",
			"BRONZE",
			"SILVER",
			"PALEYELLOW",
			"GOLD",
			"GREENYELLOW"
		];

		public static string[] WhitePatches =
		{
			"BACKSPOT", "BELLY", "BIB", "BLAZE",
			"BLAZEMASK", "BROKENBLAZE", "BUZZARDFANG", "ESTRELLA",
			"EXTRA", "EYEBAGS", "HONEY", "LEFTEAR",
			"LIGHTTUXEDO", "LILTWO", "LITTLE", "LOCKET",
			"LUNA", "MUSTACHE", "PAWS", "RAVENPAW",
			"REVERSEEYE", "REVERSEHEART", "RIGHTEAR", "SCOURGE",
			"SPARKLE", "TAILTIP", "TEARS", "TIP",
			"TOES", "TOESTAIL", "VEE", "APRON",
			"BLACKSTAR", "BLACKSTAR", "BOOTS", "BUDDY",
			"CAPSADDLE", "CHESTSPECK", "COW", "COWTWO",
			"EYESPOT", "HEART", "HEARTTWO", "KROPKA",
			"LIGHTSONG", "LOVEBUG", "MOORISH", "ONEEAR",
			"PEBBLE", "PEBBLESHINE", "PETAL", "SHOOTINGSTAR",
			"TAIL", "TAILTWO", "VAN", "FULLWHITE",
			"BEARD", "BOWTIE", "DAMIEN", "DAPPLEPAW",
			"DIGIT", "DIVA", "DOUGIE", "FADEBELLY",
			"FADESPOTS", "FANCY", "FCONE", "FCTWO",
			"MIA", "MISS", "MITAINE", "PRINCESS",
			"ROSINA", "SAVANNAH", "SKUNK", "SQUEAKS",
			"STAR", "TOPCOVER", "TUXEDO", "UNDERS",
			"VEST", "WINGS", "WOODPECKER",
			"ANY", "ANYTWO", "APPALOOSA", "BLOSSOMSTEP",
			"BROKEN", "BUB", "BULLSEYE", "BUSTER",
			"CAKE", "CURVED", "FAROFA", "FINN",
			"FRECKLES", "FRONT", "GLASS", "GOATEE",
			"HALFFACE", "HALFWHITE", "HAWKBLAZE", "MAO",
			"MASKMANTLE", "MISTER", "OWL", "PAINTED",
			"PANTS", "PANTSTWO", "PIEBALD", "PRINCE",
			"REVERSEPANTS", "RINGTAIL", "SAMMY", "SCAR",
			"SHIBAINU", "SPARROW", "TRIXIE"
		};

		public static string[] Vitiligo =
		{
			"BLEACHED", "KARPATI", "MOON", "PHANTOM", "POWDER",
			"SMOKEY", "VITILIGO", "VITILIGOTWO"
		};

		public static string[] PointMarkings =
		{
			"SEPIAPOINT", "MINKPOINT", "SEALPOINT", "COLOURPOINT",
			"RAGDOLL"
		};

		public static string[] PeltTints =
		{
			"none",
			"pink",
			"gray",
			"red",
			"black",
			"orange",
			"yellow",
			"purple",
			"blue"
		};

		public static string[] WhitePatchesTints =
		{
			"none",
			"cream",
			"darkcream",
			"gray",
			"offwhite",
			"pink"
		};

		public static string[] TortiePatterns =
		{
			"ONE",
			"TWO",
			"THREE",
			"FOUR",
			"ARMTAIL",
			"BANDANA",
			"BELOVED",
			"BLANKET",
			"BODY",
			"BRIE",
			"BRINDLE",
			"CHEST",
			"CHIMERA",
			"DAPPLENIGHT",
			"DAUB",
			"DELILAH",
			"EMBER",
			"EYEDOT",
			"FRECKLED",
			"GRUMPYFACE",
			"HALF",
			"HEARTBEAT",
			"MASK",
			"MINIMALONE",
			"MINIMALTWO",
			"MINIMALTHREE",
			"MINIMALFOUR",
			"MOTTLED",
			"OREO",
			"ORIOLE",
			"PACMAN",
			"PAIGE",
			"REDTAIL",
			"ROBIN",
			"ROSETAIL",
			"SAFI",
			"SHILOH",
			"SIDEMASK",
			"SMOKE",
			"SMUDGED",
			"STREAK",
			"STREAMSTRIKE",
			"SWOOP"
		};

		public static string[] SkinColours =
		{
			"BLACK",
			"RED",
			"PINK",
			"DARKBROWN",
			"BROWN",
			"LIGHTBROWN",
			"DARK",
			"DARKGREY",
			"GREY",
			"DARKSALMON",
			"SALMON",
			"PEACH",
			"DARKMARBLED",
			"MARBLED",
			"LIGHTMARBLED",
			"DARKBLUE",
			"BLUE",
			"LIGHTBLUE"
		};

		public static string[] scars =
		[
			"ONE", "TWO", "THREE", "FOUR",
			"LEFTEAR", "RIGHTEAR", "NOLEFTEAR", "NORIGHTEAR",
			"NOEAR", "NOPAW", "NOTAIL", "HALFTAIL",
			"BRIGHTHEART", "LEFTBLIND", "RIGHTBLIND", "BOTHBLIND",
			"BURNBELLY", "BURNRUMP", "BURNPAWS", "BURNTAIL",
			"FROSTFACE", "FROSTMITT", "FROSTSOCK", "FROSTTAIL",
			"MANLEG", "MANTAIL", "SNAKE", "SNAKETWO",
			"BEAKCHEEK", "BEAKLOWER", "BEAKSIDE", "QUILLCHUNK",
			"QUILLSCRATCH", "QUILLSIDE", "RATBITE", "NECKBITE",
			"LEGBITE", "CATBITE", "CATBITETWO", "BRIDGE",
			"FACE", "SNOUT", "CHEEK", "THROAT",
			"TAILBASE", "TAILSCAR", "BACK", "SCRATCHSIDE",
			"SIDE", "BELLY", "TOE", "TOETRAP"
		];

		public static string[] accessories =
		[
			"CRIMSON", "BLUE", "YELLOW", "CYAN", "RED", "LIME", "GREEN",
			"RAINBOW", "BLACK", "SPIKES", "WHITE", "PINK", "PURPLE", "MULTI", "INDIGO",

			"CRIMSONBELL","BLUEBELL","YELLOWBELL","CYANBELL","REDBELL","LIMEBELL","GREENBELL",
			"RAINBOWBELL","BLACKBELL","SPIKESBELL","WHITEBELL","PINKBELL","PURPLEBELL","MULTIBELL",
			"INDIGOBELL",
			
			"CRIMSONBOW","BLUEBOW","YELLOWBOW","CYANBOW","REDBOW","LIMEBOW","GREENBOW",
			"RAINBOWBOW","BLACKBOW","SPIKESBOW","WHITEBOW","PINKBOW","PURPLEBOW","MULTIBOW",
			"INDIGOBOW",

			"CRIMSONNYLON","BLUENYLON","YELLOWNYLON","CYANNYLON","REDNYLON","LIMENYLON","GREENNYLON",
			"RAINBOWNYLON","BLACKNYLON","SPIKESNYLON","WHITENYLON","PINKNYLON","PURPLENYLON",
			"MULTINYLON","INDIGONYLON",

			"MAPLE LEAF", "HOLLY", "BLUE BERRIES", "FORGET ME NOTS", "RYE STACK", "LAUREL",
			"BLUEBERRIES", "NETTLE", "POPPY", "LAVENDER", "HERBS", "PETALS", "RED FEATHERS",
			"BLUE FEATHERS", "JAY FEATHERS", "MOTH WINGS", "CICADA WINGS", "DRY HERBS",
			"OAK LEAVES", "CATMINT", "MAPLE SEED", "JUNIPER"
		];
	}
}
