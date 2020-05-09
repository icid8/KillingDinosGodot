using Godot;
using System;

public class Dino : Area2D
{
	[Signal]
	public delegate void Hit();
	
	[Signal]
	public delegate void HitC();
	
	[Export]
	public int Speed = 400; // How fast the player will move (pixels/sec).

	private Vector2 _screenSize; // Size of the game window.

	public override void _Ready()
	{
		
		_screenSize = GetViewport().Size;
		Hide();
	}
	
	public override void _Process(float delta)
	{
		var velocity = new Vector2(); // The player's movement vector.
	
		if (Input.IsActionPressed("ui_right"))
		{
			velocity.x += 1;
		}
	
		if (Input.IsActionPressed("ui_left"))
		{
			velocity.x -= 1;
		}
	
		if (Input.IsActionPressed("ui_down"))
		{
			velocity.y += 1;
		}
	
		if (Input.IsActionPressed("ui_up"))
		{
			velocity.y -= 1;
		}
	
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}
		
		Position += velocity * delta;
		Position = new Vector2( x: Mathf.Clamp(Position.x, 0, _screenSize.x), 
		y: Mathf.Clamp(Position.y, 0, _screenSize.y));
		
		if (velocity.x != 0)
		{
			animatedSprite.Animation = "right";
			animatedSprite.FlipV = false;
			// See the note below about boolean assignment
			animatedSprite.FlipH = velocity.x < 0;
		}
		else if (velocity.y != 0)
		{
			animatedSprite.Animation = "up";
			animatedSprite.FlipV = velocity.y > 0;
		}
		
	}
	
	private void _on_Dino_body_entered(object body)
	{
		// Player disappears after being hit.
		Coin c = body as Coin;
		if(c ==null){
			 	Hide(); 
			EmitSignal("Hit");
			GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		}
		else{
			c.QueueFree();
			EmitSignal("HitC");
			this.Speed=Speed+70;
		}
	}
	
	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	
	
}




