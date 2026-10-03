using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Events;
using CounterStrikeSharp.API.Modules.Timers;

namespace WaterCup;

public class WaterCup : BasePlugin
{
	public override string ModuleName => "Water Up";
	public override string ModuleVersion => "1.0.0";
	public override string ModuleAuthor => "Shane";
	public override string ModuleDescription => "Water collection game mode.";

	private MapSettings mapSettings = new MapSettings();

	private CupTracker cupTracker = new CupTracker();

	public override void Load(bool hotReload)
	{
		RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);

		RegisterEventHandler<EventPlayerTeam>(OnPlayerTeam);

		RegisterListener<Listeners.OnMapStart>(OnMapStart);

		StartCupHudTimer();
	}

	private void OnMapStart(string mapName)
	{
		AddTimer(3.0f, () =>
		{
			mapSettings.OnMapStart();

		},
		TimerFlags.STOP_ON_MAPCHANGE
		);
	}

	private void StartCupHudTimer()
	{
		AddTimer(2.0f, () =>
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

				if(player.TeamNum != 2)
				{
					continue;
				}

				cupTracker.ShowCupHud(player);
			}
		},
		TimerFlags.REPEAT |
		TimerFlags.STOP_ON_MAPCHANGE
		);
	}

	private HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
	{
		CCSPlayerController? player = @event.Userid;

		if(player == null)
		{
			return HookResult.Continue;
		}

		if(player.IsValid == false)
		{
			return HookResult.Continue;
		}

		if(player.TeamNum == 2)
		{
			cupTracker.AddPlayer(player);
		}
		else
		{
			cupTracker.RemovePlayer(player);
		}

		return HookResult.Continue;
	}

	private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
	{
		CCSPlayerController? player = @event.Userid;

		bool player_object_is_valid = true;

		if(player == null)
		{
			player_object_is_valid = false;
		}

		if(player_object_is_valid == true)
		{
			if(player.IsValid == false)
			{
				player_object_is_valid = false;
			}
		}

		if(player_object_is_valid == true)
		{
			int begin_delay_window = 1000;
			int end_delay_window = 5001;

			float delay =
			Random.Shared.Next(
				begin_delay_window,
				end_delay_window
			) / 1000.0f;

			AddTimer(delay, () =>
			{
				if(player == null)
				{
					return;
				}

				if(player.IsValid == false)
				{
					return;
				}
				else if(player.IsValid == true)
				{
					player.Respawn();

					cupTracker.AddPlayer(player);
				}

			},
			TimerFlags.STOP_ON_MAPCHANGE
			);
		}

		return HookResult.Continue;
	}
}
