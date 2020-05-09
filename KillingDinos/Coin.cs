using Godot;
using System;

public class Coin : RigidBody2D
{
	public Random ran = new Random();
	
	private String[] coinTypes = {"coin"};
	static private Random _random = new Random();

	public override void _Ready()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Animation = coinTypes[_random.Next(0, coinTypes.Length)];
		int n = ran.Next(2,5);
		GetNode<Timer>("Timer").SetWaitTime((float)n);
		GetNode<Timer>("Timer").Start();
	}
	private void _on_Timer_timeout()
	{
		Hide();
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






