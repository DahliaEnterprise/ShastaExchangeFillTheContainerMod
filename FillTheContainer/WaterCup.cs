using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Events;
using CounterStrikeSharp.API.Modules.Timers;

namespace WaterCup;

public class WaterCup : BasePlugin
{
	public override string ModuleName => "Water Cup";
	public override string ModuleVersion => "1.0.0";
	public override string ModuleAuthor => "Shane Betz shanebetz86@gmail.com";
	public override string ModuleDescription => "Water collection game mode.";

	private MapSettings mapSettings = new MapSettings();

	private CupTracker cupTracker = new CupTracker();

	private WaterSpoutText waterSpoutText = new WaterSpoutText();
	
	private PlayerCoordinates player_coordinates = new PlayerCoordinates();

	public override void Load(bool hotReload)
	{
		RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);

		RegisterEventHandler<EventPlayerTeam>(OnPlayerTeam);

		RegisterListener<Listeners.OnMapStart>(OnMapStart);
		
		RegisterEventHandler<EventRoundStart>(OnRoundStart);

		StartCupHudTimer();
		
	}

	private void OnMapStart(string mapName)
	{
		
		
		
		AddTimer(5.0f, () =>
			{
				cupTracker.Clear();
				
				mapSettings.OnMapStart();

				waterSpoutText.Create();

			},
			TimerFlags.STOP_ON_MAPCHANGE
		);
		
		AddTimer(5.0f, () =>
			{
				player_coordinates.ShowCoordinates();
			},
			TimerFlags.REPEAT |
			TimerFlags.STOP_ON_MAPCHANGE
		);
	}

	private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
	{
		//Display where the spout is and where the container to fill it is.
		AddTimer(1.0f, () =>
		   {
			   waterSpoutText.Create();
		   },
		   TimerFlags.STOP_ON_MAPCHANGE
		);

		return HookResult.Continue;
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
			if(player == null)
			{
				player_object_is_valid = false;
			}
			else if(player.IsValid == false)
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

	public override void Unload(bool hotReload)
	{
		waterSpoutText.Remove();
		cupTracker.Clear();
	}
}
