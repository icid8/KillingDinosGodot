using Godot;
using System;

public class UFO : RigidBody2D
{
   // Don't forget to rebuild the project so the editor knows about the new export variables.
	
	[Export]
	public int MinSpeed = 150; // Minimum speed range.

	[Export]
	public int MaxSpeed = 250; // Maximum speed range.

	private String[] _mobTypes = {"UFO"};
	static private Random _random = new Random();

	public override void _Ready()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Animation = _mobTypes[_random.Next(0, _mobTypes.Length)];
		
	}
	
	private void _on_VisibilityNotifier2D_screen_exited()
	{
	QueueFree();
	}
	
	public void OnStartGame()
	{
		QueueFree();
	}	
}



