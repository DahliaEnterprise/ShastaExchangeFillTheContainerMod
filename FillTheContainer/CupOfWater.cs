using CounterStrikeSharp.API.Core;

namespace WaterCup;

public class CupOfWater
{
	private const int MaximumWater = 100;

	private CCSPlayerController player;

	private int water_amount = 0;

	public CupOfWater(CCSPlayerController player)
	{
		this.player = player;
	}

	public CCSPlayerController GetPlayer()
	{
		return player;
	}

	public int GetWaterAmount()
	{
		return water_amount;
	}

	public float GetPercentage()
	{
		float percentage =
			(float)water_amount /
			(float)MaximumWater *
			100.0f;

		return percentage;
	}

	public void AddWater(int amount)
	{
		water_amount += amount;

		if(water_amount > MaximumWater)
		{
			water_amount = MaximumWater;
		}
	}

	public void RemoveWater(int amount)
	{
		water_amount -= amount;

		if(water_amount < 0)
		{
			water_amount = 0;
		}
	}

	public bool IsFull()
	{
		if(water_amount >= MaximumWater)
		{
			return true;
		}

		return false;
	}

	public void Empty()
	{
		water_amount = 0;
	}
}
