using Godot;
using System;

public class HUD : CanvasLayer
{
	public static int nVolc=0;
	
	[Signal]
	public delegate void StartGame();
	
	public override void _Ready()
	{
	}
	
	public void ShowMessage(string text)
	{
		var messageLabel = GetNode<Label>("MessageLabel");
		messageLabel.Text = text;
		messageLabel.Show();
	
		GetNode<Timer>("MessageTimer").Start();
	}
	
	async public void ShowGameOver()
	{
	ShowMessage("Game Over");

	var messageTimer = GetNode<Timer>("MessageTimer");
	await ToSignal(messageTimer, "timeout");

	var messageLabel = GetNode<Label>("MessageLabel");
	messageLabel.Text = "Dodge the\nCreeps!";
	messageLabel.Show();

	GetNode<Button>("StartButton").Show();
	GetNode<Button>("+").Show();
	GetNode<Button>("-").Show();
	GetNode<Label>("VolcLabel").Show();
	GetNode<Label>("Label").Show();
	GetNode<CheckBox>("VolcMove").Show();
	}
	
	public void UpdateScore(int score)
	{
	GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	private void _on_MessageTimer_timeout()
	{
	GetNode<Label>("MessageLabel").Hide();
	}
	
	public void OnStartButtonPressed()
	{
	GetNode<Button>("StartButton").Hide();
	EmitSignal("StartGame");
	GetNode<Button>("+").Hide();
	GetNode<Button>("-").Hide();
	GetNode<Label>("VolcLabel").Hide();
	GetNode<Label>("Label").Hide();
	GetNode<CheckBox>("VolcMove").Hide();
	
	}
	
	private void minus_on__pressed()
	{	
		if(nVolc >=1){
			nVolc--;
			var VolcLabel = GetNode<Label>("VolcLabel");
			VolcLabel.Text = nVolc.ToString();
		}
	}

	private void plus_on__pressed()
	{
   		nVolc++;
		var VolcLabel = GetNode<Label>("VolcLabel");
		VolcLabel.Text = nVolc.ToString();
		
	}
}




