using CounterStrikeSharp.API.Core;

namespace WaterCup;

public class CupTracker
{
	private const int TerroristTeam = 2;
	private const int MaximumCupWater = 100;

	private Dictionary<int, int> player_cup_water = new Dictionary<int, int>();

	public void AddPlayer(CCSPlayerController player)
	{
		if(player == null)
		{
			return;
		}

		if(player.IsValid == false)
		{
			return;
		}

		if(player.TeamNum != TerroristTeam)
		{
			return;
		}

		if(player_cup_water.ContainsKey(player.Slot) == false)
		{
			player_cup_water[player.Slot] = 0;
		}
	}

	public void RemovePlayer(CCSPlayerController player)
	{
		if(player == null)
		{
			return;
		}

		if(player_cup_water.ContainsKey(player.Slot))
		{
			player_cup_water.Remove(player.Slot);
		}
	}

	public void SetWater(CCSPlayerController player, int water)
	{
		if(player == null)
		{
			return;
		}

		if(player.IsValid == false)
		{
			return;
		}

		if(player.TeamNum != TerroristTeam)
		{
			return;
		}

		if(water < 0)
		{
			water = 0;
		}

		if(water > MaximumCupWater)
		{
			water = MaximumCupWater;
		}

		player_cup_water[player.Slot] = water;
	}

	public int GetWater(CCSPlayerController player)
	{
		if(player == null)
		{
			return 0;
		}

		if(player_cup_water.ContainsKey(player.Slot) == false)
		{
			return 0;
		}

		return player_cup_water[player.Slot];
	}

	public float GetPercentage(CCSPlayerController player)
	{
		int water = GetWater(player);

		float percentage =
			(float)water /
			(float)MaximumCupWater *
			100.0f;

		return percentage;
	}

	public void ShowCupHud(CCSPlayerController player)
	{
		if(player == null)
		{
			return;
		}

		if(player.IsValid == false)
		{
			return;
		}

		if(player.TeamNum != TerroristTeam)
		{
			return;
		}

		float cup_percentage = GetPercentage(player);

		string html =
			"<div style='text-align:left;'>" +
			"<font color='#00BFFF' size='2'>" +
			"CUP" +
			"</font><br>" +
			"<font color='#FFFFFF' size='3'>" +
			$"{cup_percentage:F2}%" +
			"</font>" +
			"</div>";

		player.PrintToCenterHtml(html, 1);
	}

	public void Clear()
	{
		player_cup_water.Clear();
	}
}
