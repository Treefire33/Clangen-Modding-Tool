using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ClanGenModTool.ObjectTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClanGenModTool.ObjectTypes;

public class History
{
	public Beginning beginning;
    public MentorInfluence mentor_influence;
    public ApprenticeCeremony app_ceremony;
    public string? lead_ceremony;
    public PossibleHistory possible_history;
    public List<HistoryEvent> died_by;
    public List<HistoryEvent> scar_events;
    public MurderHistory murder;
}

public class Beginning
{
    public bool clan_born;
    public string birth_season;
    public int age;
    public int moon;
}

public class MentorInfluence
{
    public Dictionary<string, Influence> trait;
    public Dictionary<string, Influence> skill;
}

public class ApprenticeCeremony
{
    public string honor;
    public int graduation_age;
    public int moon;
}

public class PossibleHistory
{
    public Dictionary<string, ConditionEvent> events;
}

public class MurderHistory
{
    public List<IsMurderer> is_murderer;
    public List<IsVictim> is_victim;
}

public class IsMurderer 
{
    public string victim;
    public bool revealed;
    public int moon;
}

public class IsVictim
{
    public string murderer;
    public bool revealed;
    public string text;
    public string unrevealed_text;
    public int moon;
}

public class ConditionEvent
{
    public string involved;
    public string death_text;
    public string scar_test;
}

public class HistoryEvent
{
    public string involved;
    public string text;
    public int moon;
}

public class Influence
{
    [JsonExtensionData(ReadData = true, WriteData = true)]
    private Dictionary<string, JToken> privTrait;
    [OnDeserialized]
    public void UpdateTrait(StreamingContext context)
    {
        change = (int)privTrait.First().Value;
        facet = privTrait.First().Key;
    }
    [OnSerialized]
    public void UpdateTraitSerialize(StreamingContext context)
    {
        if(privTrait != null)
        {
            privTrait.Clear();
        }
        else
        {
            privTrait = new();
        }
        privTrait.Add(facet, change);
    }
    [JsonIgnore]
    public string facet;
	[JsonIgnore]
	public int change;
    public List<string> strings;
}