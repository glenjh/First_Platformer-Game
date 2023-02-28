using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
   public PlayerState() {}
   
   public virtual void Enter(Player player) {}
   
   public virtual void Update(Player player) {}
   
   public virtual void Exit(Player player) {}

   public virtual PlayerState HandleInput(PlayerStates ps)
   {
      if (ps != null)
      {
         switch (ps)
         {
            case PlayerStates.Idle:
               return new IdleState(ps);
            
            case PlayerStates.Run:
               return new RunState(ps);
            
            case PlayerStates.Climb:
               return new ClimbState(ps);

            default:
               return null;
         }
      }

      return null;
   }
}

public class IdleState : PlayerState
{
   public IdleState() {}

   public IdleState(PlayerStates ps)
   {
      ps = PlayerStates.Idle;
   }

   public override void Enter(Player player)
   {
      if (player != null)
      {
         player.anim.SetBool("isRuning", false);
      }
   }
   
   public override void Update(Player player)
   {
      
   }

   public override void Exit(Player player)
   {
      
   }

   public override PlayerState HandleInput(PlayerStates ps)
   {
      return base.HandleInput(ps);
   }
}

public class RunState : PlayerState
{
   public RunState() {}

   public RunState(PlayerStates ps)
   {
      ps = PlayerStates.Run;
   }

   public override void Enter(Player player)
   {
      if (player != null)
      {
         player.anim.SetBool("isRuning", true);
      }
   }
   
   public override void Update(Player player)
   {
      player.rigid.velocity = player.move;
   }

   public override void Exit(Player player)
   {
      if (player != null)
      {
         player.anim.SetBool("isRuning", false);
         player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);
      }
   }

   public override PlayerState HandleInput(PlayerStates ps)
   {
      return base.HandleInput(ps);
   }
}

public class ClimbState : PlayerState
{
   public ClimbState() {}

   public ClimbState(PlayerStates ps)
   {
      ps = PlayerStates.Climb;
   }

   public override void Enter(Player player)
   {
      if (player != null)
      {
         player.anim.SetBool("isClimbing", true);
      }
   }
   
   public override void Update(Player player)
   {
      if (player != null)
      {
         player.rigid.velocity = player.climb;
      }
   }

   public override void Exit(Player player)
   {
      if (player != null)
      {
         player.anim.SetBool("isClimbing", false);
      }
   }

   public override PlayerState HandleInput(PlayerStates ps)
   {
      return base.HandleInput(ps);
   }
}
