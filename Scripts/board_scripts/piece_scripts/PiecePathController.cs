using Godot;
using System;


public partial class PiecePathController : Path3D
{
	[Export] private PathFollow3D _pathFollow;
	[Export] private Node3D _pieceGuide;
	[Export] private float _speed = 4f;
	private bool _isSetUp = false;
	private PieceController _guidedPiece;

    public override void _PhysicsProcess(double delta)
    {
        if(_isSetUp){
			_pathFollow.ProgressRatio += (float)delta * _speed;
			if(Mathf.Abs(_pathFollow.ProgressRatio - 1) <= (float)delta * _speed /*0.1*/){
				_isSetUp = false;
				_guidedPiece.UnsubscribeFromGuide(ToGlobal(Curve.GetPointPosition(1)));
				_guidedPiece = null;
			}
		}
    }

    public void SetUpPiecePath(Vector3 from, Vector3 to, PieceController piece){
		// CLEAN UP (PROBABLY NOT NECCESSARY)
		_guidedPiece?.UnsubscribeFromGuide(ToGlobal(Curve.GetPointPosition(1)));
		
		// SET UP CURVE POINTS
		Curve.ClearPoints();
		Vector3 fromLocal = ToLocal(from);
		Vector3 toLocal = ToLocal(to);
		Curve.AddPoint(fromLocal);
		Curve.AddPoint(toLocal);

		// SET UP ARC
		float smoothingAngle = 45;
		Vector3 midPoint = (toLocal - fromLocal) * 0.5f;
		float helperPointDist = midPoint.Length() * Mathf.Tan(Mathf.DegToRad(smoothingAngle));
		Vector3 smoothingHelperPoint = fromLocal + midPoint + midPoint.Cross(midPoint.Rotated(Vector3.Up, Mathf.Pi / 2)).Normalized() * helperPointDist;

		float smoothStrength = 0.5f;

		Curve.SetPointOut(0, (smoothingHelperPoint - fromLocal) * smoothStrength);
		Curve.SetPointIn(1, (smoothingHelperPoint - toLocal) * smoothStrength);

		// SET UP GUIDE
		_pathFollow.ProgressRatio = 0;
		_isSetUp = true;

		// SUBSCRIBE GUIDE
		_guidedPiece = piece;
		CallDeferred("SubscribeGuidedPiece"); // Needed to be defferred because of a rogue frame where the guided piece's position is not at ProgressRation = 0
	}

	private void SubscribeGuidedPiece(){
		AudioManager.Instance.PlaySound(_guidedPiece.audioLibrary.GetSound("piece_moved"));
		_guidedPiece.SubscribeToGuide(_pieceGuide, Vector3.Zero);
	}
}
