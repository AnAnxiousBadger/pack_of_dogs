using Godot;
using System;
using System.Collections.Generic;

public partial class GoatController : CritterController
{
	public void SpawnFootPrintDecal()
	{
		if (IsActive)
		{
			GoatFootPrintDecalController footPrint = Pool.GetPoolable("goat_print", GlobalPosition) as GoatFootPrintDecalController;
			footPrint.GlobalPosition = GlobalPosition;
			footPrint.GlobalRotation = GlobalRotation;
			footPrint.OnSpawn();
		}
	}
	protected override void EmitCriterClickedSignal()
	{
		if (GlobalHelper.Instance.GameController.currPlayer is RealPlayerController player)
		{
			player.EmitSignal(BasePlayerController.SignalName.GoatClicked, this);
		}
	}
}
