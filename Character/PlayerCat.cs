using Godot;

public partial class PlayerCat : CharacterBody2D
{
	//parameters/Idle/blend_position

	
	private AnimationTree _animationTree;
	private Vector2 velocity;

	private AnimationNodeStateMachinePlayback _stateMachine;
	[Export]
	private Vector2 _startPosition = new Vector2(0, 0.5f);

public override void _Ready()
{
	_animationTree = GetNode<AnimationTree>("AnimationTree");
	_stateMachine = GetNode<AnimationTree>("AnimationTree").Get("parameters/playback").Obj as AnimationNodeStateMachinePlayback;
	UpdateAnimationsParameters(_startPosition);
}
	[Export]
	public const float Speed = 1.0f;
	public override void _PhysicsProcess(double delta)
	{
		var input_direction = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			 Input.GetActionStrength("down") - Input.GetActionStrength("up")
		);
		var velocity = input_direction * Speed;
		if (Input.GetActionRawStrength("sprint") == 1){
			velocity *= 2;
		}
		UpdateAnimationsParameters(velocity);
		MoveAndCollide(velocity);
		Pick_new_state(velocity);

	}
	private void UpdateAnimationsParameters(Vector2 moveInput){
		if(moveInput != Vector2.Zero){
			_animationTree.Set("parameters/Idle/blend_position", moveInput);
			_animationTree.Set("parameters/Walk/blend_position", moveInput);
		}
	}
	public void Pick_new_state(Vector2 velocity){
		if(velocity != Vector2.Zero){
			_stateMachine.Travel("Walk");
			return;
		}
			_stateMachine.Travel("Idle");
	}
	
}
