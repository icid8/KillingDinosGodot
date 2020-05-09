using Godot;
using System;

public class Main : Node
{
	 // Don't forget to rebuild the project so the editor knows about the new export variable.

	[Export]
	public PackedScene Mob;
	
	[Export]
	public PackedScene UFO;
	
	[Export]
	public PackedScene Coin;
	
	[Export]
	public PackedScene Volc;
	
	public Random ran =new Random();

	private int _score;

	// We use 'System.Random' as an alternative to GDScript's random methods.
	private Random _random = new Random();

	public override void _Ready()
	{
	}

	// We'll use this later because C# doesn't support GDScript's randi().
	private float RandRange(float min, float max)
	{
		return (float)_random.NextDouble() * (max - min) + min;
	}
	
	private void _on_Dino_Hit()
	{	
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
	}
	private void _on_Dino_HitC()
	{
		_score=_score+20;
		GetNode<HUD>("HUD").UpdateScore(_score);
	}
	
	public void NewGame()
	{
		_score = 0;
	
		var player = GetNode<Dino>("Dino");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
	
		GetNode<Timer>("StartTimer").Start();
		
		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Readyyyy!");
			var volcInstance = (RigidBody2D)Volc.Instance();

		for (int i = 0; i<HUD.nVolc; i++)
			{
				generarVolc();		

			}
	}
	
	public void generarVolc(){
		if(GetNode<CheckBox>("HUD/VolcMove").Pressed==false){
		float x= ran.Next(1,480);
		float y= ran.Next(1,720);

		// Create a Mob instance and add it to the scene.
		var volcInstance = (RigidBody2D)Volc.Instance();
		AddChild(volcInstance);

		// Set the mob's position to a random location.
		volcInstance.Position=new Vector2(x,y);
		GetNode("HUD").Connect("StartGame", volcInstance, "OnStartGame");	
			
		}
		else{
				 // Choose a random location on Path2D.
				var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
				mobSpawnLocation.SetOffset(_random.Next());
			
				// Create a Mob instance and add it to the scene.
				var mobInstance = (RigidBody2D)Volc.Instance();
				AddChild(mobInstance);
				
				// Set the mob's direction perpendicular to the path direction.
				float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
			
				// Set the mob's position to a random location.
				mobInstance.Position = mobSpawnLocation.Position;
			
				// Add some randomness to the direction.
				direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
				mobInstance.Rotation = direction;
			
				// Choose the velocity.
				mobInstance.SetLinearVelocity(new Vector2(RandRange(150f, 250f), 0).Rotated(direction));
				_on_ScoreTimer_timeout();
				GetNode("HUD").Connect("StartGame", mobInstance, "OnStartGame");	
					
		}
		

	}
	
	private void _on_StartTimer_timeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
		
	}
	
	private void _on_ScoreTimer_timeout()
	{
		 _score++;
		GetNode<HUD>("HUD").UpdateScore(_score);
		
	}	
	
	private void _on_MobTimer_timeout()
	{	
		///////////////MONEDA///////////////////////
	if(_score%10==0&&_score>0){
		float x= ran.Next(1,480);
		float y= ran.Next(1,720);

		// Create a Mob instance and add it to the scene.
		var coinInstance = (RigidBody2D)Coin.Instance();
		AddChild(coinInstance);

		// Set the mob's position to a random location.
		coinInstance.Position=new Vector2(x,y);
		
	}
	
		////////////////////UFO/////////////////////
	if(ran.Next(1,9)<=2){
		
		_score++;
		
		 // Choose a random location on Path2D.
		var mobSpawnLocation2 = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation2.SetOffset(_random.Next());
	
		// Create a Mob instance and add it to the scene.
		var ufoInstance = (RigidBody2D)UFO.Instance();
		AddChild(ufoInstance);

		// Set the mob's direction perpendicular to the path direction.
		float direction2 = mobSpawnLocation2.Rotation + Mathf.Pi / 2;
	
		// Set the mob's position to a random location.
		ufoInstance.Position = mobSpawnLocation2.Position;
	
		// Add some randomness to the direction.
		direction2 += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		ufoInstance.Rotation = direction2;
	
		// Choose the velocity.
		ufoInstance.SetLinearVelocity(new Vector2(RandRange(150f, 250f), 0).Rotated(direction2));
		_on_ScoreTimer_timeout();
		GetNode("HUD").Connect("StartGame", ufoInstance, "OnStartGame");
		
	}		
	else{
		//////////MOB/////////////////////////////////
		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.SetOffset(_random.Next());
	
		// Create a Mob instance and add it to the scene.
		var mobInstance = (RigidBody2D)Mob.Instance();
		AddChild(mobInstance);
		
		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
	
		// Set the mob's position to a random location.
		mobInstance.Position = mobSpawnLocation.Position;
	
		// Add some randomness to the direction.
		direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;
	
		// Choose the velocity.
		mobInstance.SetLinearVelocity(new Vector2(RandRange(150f, 250f), 0).Rotated(direction));
		_on_ScoreTimer_timeout();
		GetNode("HUD").Connect("StartGame", mobInstance, "OnStartGame");	
		}
		
	}
	
	
	
	
}





