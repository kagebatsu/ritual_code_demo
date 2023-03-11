function SGMStateGame(_event, _layer)
{
	//in this game state, depixelate the screen on start
	//handles spawning enemies every so often
	//checks if requirement has been reached to transition to win screen
  	switch(_event) 
	{
  		case TRUESTATE_NEW:
		g.gamePaused = false;
		final_pixelate = 100;
  		break;
	

  		case TRUESTATE_STEP:
		if !PAUSED
		{
			if final_pixelate != 0
				{
					var _pixelate = fx_create("_filter_pixelate");
					final_pixelate = lerp(final_pixelate, base_pixelate, .35);
					fx_set_parameter(_pixelate, "g_CellSize", final_pixelate);
					layer_set_fx("Pixelate", _pixelate);
		
		
				}
		
			//spawn a new enemy from the stack every 2 seconds until stack is empty
			if !ds_stack_empty(enemy_stack)
			{
				if enemy_spawn_timer != 0 { enemy_spawn_timer = max(0, enemy_spawn_timer - (1/room_speed)); } 
				else
				{
					if instance_number(pEnemy) < 40
					{
						enemy_spawn_timer = 2;
						var distance = irandom_range(200, 250); //Maximum distance from center that objects can spawn
										
						var _spawn_this_enemy = ds_stack_pop(enemy_stack);

						randomize();
						var dir = random_range(0, 359);
				
						play_sound(fxEnemySpawnIn);
						var spawn = instance_create_layer(oPlayer.x + lengthdir_x(distance, dir), oPlayer.y + lengthdir_y(distance, dir), "Instances",_spawn_this_enemy);
						with (spawn)
						{
							//BARRIER
							var barrierchance = irandom_range(0, 100);
							if barrierchance >= 85
							{
								spawn.barrier = true;
							}
							else
							{
								spawn.barrier = false;
							}
										
							//HP
							spawn.enemyHP = 5 + ( 2 * other.selection );
							//spawn.enemyHP = 200;
							spawn.currentHP = spawn.enemyHP; 
							spawn.nextHP = spawn.currentHP;
							//FIRE RATE
										
							//spawn.laziness = irandom_range(4, 5);
							//spawn.laziness_default = laziness;
							//spawn.belligerence = irandom_range(1, 2);
										
							//spawn.fireCooldown = 2 - .25;
							//var cooldown = irandom_range(4, 8); 
											
							//spawn.fireCooldown = cooldown - ( .05 * other.selection );
							//spawn.cooldownDefault = spawn.fireCooldown;
					
							spawn.laziness = irandom_range(4, 5);
							spawn.laziness_default = laziness;
							spawn.belligerence = irandom_range(1, 2);
										
							//spawn.fireCooldown = .1 + (spawn.laziness_default * .25) - (spawn.belligerence * .05);
							//var quickness = random_range(.2, .5);
							//spawn.fireCooldown = 2 - quickness;
			
							spawn.fireCooldown = .1 + (spawn.laziness_default * .25) - (spawn.belligerence * .05) + (3 / (other.selection + 1));
													
							spawn.cooldownDefault = spawn.fireCooldown;
											
							//DMG BOOST
							if other.master_params[| other.master_selection].dmgboost
							{
								array_push(entityDropList, oDmgBoost, oDmgBoost, oDmgBoost);
							}
										
							//TIMER BOOST
							if other.master_params[| other.master_selection].timeboost
							{
								array_push(entityDropList, oTimeBoost, oTimeBoost, oTimeBoost);
							}
										
							//HEALTH BOOST
							if other.master_params[| other.master_selection].healthboost
							{
								array_push(entityDropList, oSustis, oSustis, oSustis);
							}
										
							//ENERGY BOOST
							if other.master_params[| other.master_selection].energyboost
							{
								array_push(entityDropList, oEnergy, oEnergy, oEnergy);
							}
										
						}
					}
			
				}

			}
			//if all enemies are defeated
			if instance_number(pEnemy) == 0 and ds_stack_empty(enemy_stack)
			{
				enemies_defeated = true;
				behavior.state_switch(STATE_END);
			}
		
			if time_limit != -1
			{
				//show_debug_message("time ccounting down" + string(time_limit))
				if !(time_limit <= 0) time_limit -= 1 / room_speed
				else
				{
					behavior.state_switch(STATE_END);
					time_up = true;
				}
			}
		}

		break;
		
  		case TRUESTATE_DRAW:
 
  		break;
		
		case TRUESTATE_DRAW_GUI:
			draw_sprite_ext(sUINotify, 0, RESOLUTION_W/2, 80, .5, .5, 0, c_red, 1);
			draw_set_align(fa_center, fa_middle);
			draw_text(RESOLUTION_W/2, 80, string(time_limit));
			draw_reset();
		break;
  	}
}