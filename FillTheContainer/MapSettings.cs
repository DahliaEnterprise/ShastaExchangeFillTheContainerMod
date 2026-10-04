using CounterStrikeSharp.API;

namespace WaterCup;

public class MapSettings
{
	public void OnMapStart()
	{
		Server.ExecuteCommand("bot_quota 20");
		Server.ExecuteCommand("bot_quota_mode fill");
		Server.ExecuteCommand("mp_warmuptime 0");
		Server.ExecuteCommand("mp_do_warmup_period 0");
		Server.ExecuteCommand("mp_buytime 9999");
		Server.ExecuteCommand("mp_give_player_c4 0");
	}
}
