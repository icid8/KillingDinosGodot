using Godot;
using System;

public class Volc : RigidBody2D
{
	public Random ran = new Random();
	
	private String[] VolcTypes = {"Volc"};
	static private Random _random = new Random();

	public override void _Ready()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Animation = VolcTypes[_random.Next(0, VolcTypes.Length)];
	
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



