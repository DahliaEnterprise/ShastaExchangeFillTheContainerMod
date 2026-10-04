using System.Drawing;

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace WaterCup;

public class WaterSpoutText
{
	private CPointWorldText? water_spout_text = null;

	private float water_spout_x = -1161.05f;
	private float water_spout_y = -858.65f;
	private float water_spout_z = 216.40f;

	public void Create()
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

			player.PrintToChat(
				"[Water Cup] Creating 3D Water Spout text."
			);
		}

		if(water_spout_text != null)
		{
			if(water_spout_text.IsValid == true)
			{
				water_spout_text.Remove();
			}

			water_spout_text = null;
		}

		water_spout_text =
			Utilities.CreateEntityByName<CPointWorldText>(
				"point_worldtext"
			);

		if(water_spout_text == null)
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

				player.PrintToChat(
					"[Water Cup] ERROR: point_worldtext creation failed."
				);
			}

			return;
		}

		water_spout_text.MessageText = "WATER SPOUT";

		water_spout_text.Enabled = true;

		water_spout_text.FontSize = 128;

		water_spout_text.WorldUnitsPerPx = 0.25f;

		water_spout_text.Fullbright = true;

		water_spout_text.Color =
			ColorTranslator.FromHtml("#00FFFF");

		water_spout_text.DepthOffset = 0.0f;

		water_spout_text.JustifyHorizontal =
			PointWorldTextJustifyHorizontal_t
				.POINT_WORLD_TEXT_JUSTIFY_HORIZONTAL_CENTER;

		water_spout_text.JustifyVertical =
			PointWorldTextJustifyVertical_t
				.POINT_WORLD_TEXT_JUSTIFY_VERTICAL_CENTER;

		water_spout_text.DispatchSpawn();

		water_spout_text.Teleport(
			new Vector(
				water_spout_x,
				water_spout_y,
				water_spout_z
			),
			new QAngle(
				90.0f,
				0.0f,
				0.0f
			),
			new Vector(
				0.0f,
				0.0f,
				0.0f
			)
		);

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

			player.PrintToChat(
				"[Water Cup] 3D Water Spout entity created."
			);
		}
	}

	public void Remove()
	{
		if(water_spout_text == null)
		{
			return;
		}

		if(water_spout_text.IsValid == true)
		{
			water_spout_text.Remove();
		}

		water_spout_text = null;
	}
}
