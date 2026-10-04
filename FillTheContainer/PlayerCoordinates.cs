using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace WaterCup;

public class PlayerCoordinates
{
	public void ShowCoordinates()
	{
		foreach(CCSPlayerController player in Utilities.GetPlayers())
		{
			if(player == null)
			{
				continue;
			}

			if(player.IsValid == false)
			{
				continue;
			}

			if(player.IsHLTV == true)
			{
				continue;
			}

			if(player.PlayerPawn == null)
			{
				continue;
			}

			if(player.PlayerPawn.IsValid == false)
			{
				continue;
			}

			Vector position = player.PlayerPawn.Value.AbsOrigin;

			player.PrintToChat(
				$"[Water Cup] X: {position.X:F2} Y: {position.Y:F2} Z: {position.Z:F2}"
			);
		}
	}
}
