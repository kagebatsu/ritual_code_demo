if (draw_ok) 
{
			
	#region -- draw menu bg
		//Draw Menu "Back" --black rectangle
	var c = c_black;
	draw_set(c, alpha);
	draw_rectangle_color(0, 0, RESOLUTION_W, RESOLUTION_H, c, c, c, c, false);
	draw_reset();

	if bg_warning_flash		 { var c = c_red   var blur = warning_blur ; }
	else					 {     c = c_white var blur = alpha        ; }

	#endregion
	
	////RING COORDINATES
	//ring_x = RESOLUTION_W/2;
	//ring_y = RESOLUTION_H/2 - 50;
	//ring_radius = 100;

	if !(prepping_convo)
	{
		for (var i = 0; i <= ring_size-1; i++)
		{
		//ICONS
		var _icon = icons[i];
		angle = i/ring_size;
		spr_x = ring_x + cos((rotation_animation + angle) * 2 * pi) * ring_radius;
		spr_y = ring_y + sin((rotation_animation + angle) * 2 * pi) * ring_radius;
		image_speed = 1;

		//INVESTED VALUES
		switch (_icon.display)
		{
			case DISPLAY_TYPE.SHIFT:
		
				if _icon.invested > 0 { draw_set($CC7A00, 1, fDisplay); }
				else				  { draw_set(c_gray, 1, fDisplay);  }

				draw_text(spr_x + 10, spr_y, _icon.invested);
				draw_reset()
			
			break;
		
			case DISPLAY_TYPE.TOGGLE:
		
				if _icon.invested > 0 
				{
					draw_set($CC7A00, 1, fDisplay); 
					draw_text(spr_x + 10, spr_y, "ON");
					draw_reset()
				}
				else				 
				{ 
					draw_set(c_gray, 1, fDisplay);  
					draw_text(spr_x + 10, spr_y, "OFF");
					draw_reset()
				}

			break;
			
			case DISPLAY_TYPE.JOB:
		
				if _icon.tracked[_icon.selected] == ON
				{
					draw_set($CC7A00, 1, fDisplay); 
					draw_text(spr_x + 20, spr_y + 20, "TRACKED");
					draw_reset()
				}
				else				 
				{ 
					draw_set(c_gray, 1, fDisplay);  
					draw_text(spr_x + 20, spr_y + 20, "DISREGARD");
					draw_reset()
				}

			break;
			
			case DISPLAY_TYPE.EQUIP:
		
				//
			break;
			
			case DISPLAY_TYPE.FLOPPY:
				
				
				
			break;
	
		}
	
	
		if i == selection
		{
			//WARNING BLUR ON ICON
			if bg_warning_flash		 { var c = c_red   var blur = warning_blur ; }
			else					 {     c = c_white var blur = talpha            ; }
			
			//ICON
			if (bg_warning_flash) { draw_sprite_ext(_icon.sprite, image_index, spr_x, spr_y, _icon.scale, _icon.scale, 0, c, blur); }
			else				  { draw_sprite_ext(_icon.sprite, image_index, spr_x, spr_y, _icon.scale, _icon.scale, 0, c_white, 1); }
			//NAME
			draw_set_align(fa_center, fa_middle)
			if (warning) { draw_text_color(ring_x + wobble, ring_y + slidein + wobble, _icon.name, c_red, c_red, c_red, c_red, blur); }
			else         { draw_text_color(ring_x + wobble, ring_y + slidein + wobble, _icon.name, c, c, c, c, 1);                 }
			
			draw_reset();
			
			//DIVIDING LINE
			draw_set_align(fa_center, fa_middle)
			draw_line(ring_x - 100, ring_y + 150, ring_x + 100, ring_y + 150)
			draw_reset();
		
			//DESCRIPTION
			draw_set_align(fa_center, fa_middle)
			draw_set(c_white, 1, fDisplay);
			draw_text(ring_x, ring_y + 200, _icon.description);
			draw_reset();
		
			//DISPLAY_TYPE DRAW
			switch(_icon.display)
			{
			
				
				#region --FLOPPY DISPLAY
				// FLOPPY
				case DISPLAY_TYPE.FLOPPY:
				
					if _icon.encrypted
					{
						
						draw_sprite_ext(txt_bg, 0, textbox_x - border, textbox_y, textbox_width/txtb_spr_w, textbox_height/txtb_spr_h, 0, c_white, .7);
						draw_text_ext_transformed_color(45, 150 + (20), "     <--ENCRYPTED-->", 20, 350, 1, 1, 0, c_white, c_white, c_white, c_white, 1);
						draw_set_font(fTalkText);
						draw_text_color(spr_x + 20, spr_y + 20, "NEW", c_green, c_green, c_green, c_green, .85);
						draw_reset();
						draw_set_halign(fa_center);
						draw_sprite(sController, 24, ring_x - 40, ring_y + 300);
						draw_text_transformed(ring_x, ring_y + 300, "      DECRYPT", 1, 1, 0);
					}
					else
					{
						draw_sprite_ext(txt_bg, 0, textbox_x - border, textbox_y, textbox_width/txtb_spr_w, textbox_height/txtb_spr_h, 0, c_white, .7);
						var _txt = _icon.text;
						var _scannable = _icon.scannable;
						var _callable  = _icon.callable;

						var c = c_white;
						var f = fFloppyText;

						
						_icon.fresh = false;
				
						if _scannable 
						{
							draw_sprite_ext(sScan, 0, ring_x, ring_y + 300, 1, 1, 0, c, 1);
							draw_set_font(f);
							draw_text_transformed(ring_x + 6, ring_y + 300, "DATASCAN (R1)", 1, 1, 0);
							draw_reset();
						}
						if _callable
						{
							var _call_text = _icon.call_text;
							draw_sprite_ext(sCallTeam, 0, ring_x, ring_y + 315, 1, 1, 0, c, 1);
							draw_set_font(f);
							draw_text_transformed(ring_x + 20, ring_y + 315, _call_text, 1, 1, 0);
							draw_reset();
						}
					
						if array_length(_txt) > 1
						{
							draw_set_font(f);
							for (var line = 0; line <= array_length(_txt) - 1; line++)
							{
								draw_text_ext_transformed_color(45, 150 + (20 * line), _txt[line], 20, 350, 1, 1, 0, c, c, c, c, 1);
							}
						}
						else
						{
							draw_set_font(f);
							draw_text_ext_transformed_color(45, 150 + (20), _txt[0], 20, 350, 1, 1, 0, c, c, c, c, 1);
						}

						draw_reset();
					}
				break;
				#endregion
				
				#region --SHIFT DISPLAY
				// SHIFT 
			
				case DISPLAY_TYPE.SHIFT:
		
					var current = _icon.selected;
					var range   = _icon.value_range;
					var left_shift = "<< ";
					var right_shift = " >>";
					c = c_white;
			
					if (current == 0)						left_shift = "";
					if (current == array_length(range)-1)	right_shift = "";
	
			
					if(inputting) 
					{
						if (warning)
						{
							c = c_red;
							//x_offset = -((x_buffer/2)/warn_wobble);
						}
						else
						{
							c = $CC7A00;
							//x_offset = -(x_buffer/2);
						}
						//c = $CC7A00;
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, ring_y + 250, left_shift + string(range[current]) + right_shift, c, c, c, c, 1);
						draw_text_color(ring_x, ring_y + 170, "INNOVATION: " + string(g.sm.status.stats.innovation.amount), c, c, c, c, 1);

						draw_reset();
				
					}
					else
					{
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, ring_y + 250, string(range[current]), c, c, c, c, 1);
						draw_reset();
					}
			
					if (warning)
					{
						c = c_red;
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, RESOLUTION_H-50, "INSUFFICIENT INNOVATION", c, c, c, c, 1);
						draw_reset();
					}
			
		
				break;
				#endregion
				
				#region --ECOLOGY DISPLAY
				// SHIFT 
			
				case DISPLAY_TYPE.ECOLOGY:
		
					var costs = _icon.costs;
					
					for (var _c = 0; _c <= array_length(costs) - 1; _c ++)
					{
						var _o = 5;
						var _n = costs[_c][0]; //name
						var _bary = ring_y + (16 * _c) - _o;
						var _bary2= ring_y + (16 * _c) - _o;
						
						//costs[_c][2] += 1;
						var _total = costs[_c][1]; //total resource required
						var _curr  = costs[_c][2]; //current resource carried
						
						var _amt = _curr/_total * 100;
						draw_set_font(fMap);
						draw_text_color(30, ring_y + (16 * _c), _n, c_white, c_white, c_white, c_white, 1);
						
						if _total != -1
						{
							draw_bar(fMap, "", 0, 0, 30, _bary, 400, _bary2, _amt, c_white, c_gray, c_green, 0, true, true);  
						}
						else
						{
							draw_bar(fMap, "", 0, 0, 30, _bary, 400, _bary2, _amt, c_white, c_gray, c_gray, 0, true, true);  
						}
						
						if incrementing 
						{
							
							draw_text_color(30 + 80, ring_y + (16 * _c), _curr, c_white, c_white, c_white, c_white, 1);
							draw_text_color(30 + 80 + string_width( string( _curr ) ), ring_y + (16 * _c), _total, c_gray, c_gray, c_gray, c_gray, 1);
							//draw_text_color(30 + 80 + string_width( string( _curr ) + string( _total )), ring_y + 200, IM.inventory.scrap.amount, c_white, c_white, c_white, c_white, 1);
							//draw_text_color(30 + string_width( _n ), ring_y + (16 * _c), _curr, c_white, c_white, c_white, c_white, 1);
							//draw_text_color(30 + string_width( _n ) + string_width( string( _curr ) ), ring_y + (16 * _c), _total, c_white, c_white, c_white, c_white, 1);
						}
						draw_reset();
					}
					
					//var range   = _icon.value_range;
					//var left_shift = "<< ";
					//var right_shift = " >>";
					c = c_white;
				
			
		
				break;
				#endregion
			
				#region --TOGGLE DISPLAY
				//TOGGLE
			
			
				case DISPLAY_TYPE.TOGGLE:
			
					var current = _icon.selected;
					var range   = _icon.value_range;
			
					c = c_white;
					var c1, c2
					if (inputting) { c = $CC7A00; }
					if (warning) { c = c_red ; } 
					if (current == 1) { c1 = c_dkgray; c2 = c; }
					else				  { c1 = c; c2 = c_dkgray; }
			
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x -40, ring_y + 250, "OFF", c1, c1, c1, c1, 1);
					draw_text_color(ring_x + 40, ring_y + 250, "ON", c2, c2, c2, c2, 1);
					if (inputting)
					{
						draw_text_color(ring_x, ring_y + 170, "INNOVATION: " + string(g.sm.status.stats.innovation.amount), c, c, c, c, 1);
					}
					draw_reset();
			
					if (warning)
					{
						c = c_red;
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, RESOLUTION_H-50, "INSUFFICIENT INNOVATION", c, c, c, c, 1);
						draw_text_color(ring_x, ring_y + 170, "INNOVATION: " + string(g.sm.status.stats.innovation.amount), c, c, c, c, 1);
						draw_reset();
					}
			
				break;
				#endregion 
				
				#region --EQUIP DISPLAY
				//EQUIP
				
				case DISPLAY_TYPE.EQUIP:
		
					var current = _icon.selected;
					var range   = _icon.value_range;
					var left_shift = "<< ";
					var right_shift = " >>";
					c = c_white;
			
					if (current == 0)						left_shift = "";
					if (current == array_length(range)-1)	right_shift = "";
	
			
					if(inputting) 
					{
						if (warning)
						{
							c = c_red;
							//x_offset = -((x_buffer/2)/warn_wobble);
						}
						else
						{
							c = $CC7A00;
							//x_offset = -(x_buffer/2);
						}
						//c = $CC7A00;
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, ring_y + 250, left_shift + string(range[current].name) + right_shift, c, c, c, c, 1);


						draw_reset();
				
					}
					else
					{
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, ring_y + 250, string(range[current].name), c, c, c, c, 1);
						draw_reset();
					}
			
					if (warning)
					{
						c = c_red;
						draw_set_align(fa_center, fa_middle);
						draw_text_color(ring_x, RESOLUTION_H-50, "INSUFFICIENT INNOVATION", c, c, c, c, 1);
						draw_reset();
					}
			
		
				break;
				#endregion
				
				#region --DISPLAY DISPLAY
				//DISPLAY
			
				case DISPLAY_TYPE.DISPLAY:
					
					c = c_white;
					var amt = _icon.amount;
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x, ring_y + 250, amt, c, c, c, c, 1);
					draw_reset();
				break;
				#endregion
				
				
				#region --JOB DISPLAY
				//JOB
			
				case DISPLAY_TYPE.JOB:
					
					c = c_white;
					var current_step_description = _icon.steps[_icon.current_job_step];
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x, ring_y + 250, current_step_description, c, c, c, c, 1);
					draw_reset();
					
					var current = _icon.selected;
					var range   = _icon.tracked;
			
					c = c_white;
					var c1, c2
					if (inputting) { c = $CC7A00; }
					if (current == 1) { c1 = c_dkgray; c2 = c; }
					else				  { c1 = c; c2 = c_dkgray; }
			
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x -40, ring_y + 280, "DISREGARD", c1, c1, c1, c1, 1);
					draw_text_color(ring_x + 80, ring_y + 280, "TRACK", c2, c2, c2, c2, 1);
					//if (inputting)
					//{
					//	draw_text_color(ring_x, ring_y + 170, "INNOVATION: " + string(g.sm.status.stats.innovation.amount), c, c, c, c, 1);
					//}
					draw_reset();
			
					//if (warning)
					//{
					//	c = c_red;
					//	draw_set_align(fa_center, fa_middle);
					//	draw_text_color(ring_x, RESOLUTION_H-50, "INSUFFICIENT INNOVATION", c, c, c, c, 1);
					//	draw_text_color(ring_x, ring_y + 170, "INNOVATION: " + string(g.sm.status.stats.innovation.amount), c, c, c, c, 1);
					//	draw_reset();
					//}
				break;
				#endregion
				
				#region --MAP DISPLAY
				case DISPLAY_TYPE.MAP:
					c = c_white;
					var amt = _icon.amount;
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x, ring_y + 250, amt, c, c, c, c, 1);
					draw_reset();
				break;
				#endregion
				
				#region --SCRIPT DISPLAY
				//SCRIPT RUN
			
				case DISPLAY_TYPE.SCRIPT_RUN:
					c = c_white;
					var scr = _icon.scr;
					draw_set_align(fa_center, fa_middle);
					draw_text_color(ring_x, ring_y + 250, scr(), c, c, c, c, 1);
					draw_reset();
				break;
				#endregion
			}
	
		}
		else
		{
			//draw_sprite_ext(_icon.sprite, 0, spr_x, spr_y, _icon.scale, _icon.scale, 0, c_white, 1); 
			draw_sprite_ext(_icon.sprite, image_index, spr_x, spr_y, .75, .75, 0, c_gray, 1);
		
			if _icon.display == DISPLAY_TYPE.FLOPPY and _icon.fresh
			{
				draw_set_font(fTalkText);
				draw_text_color(spr_x + 20, spr_y + 20, "NEW", c_green, c_green, c_green, c_green, .85);
				draw_reset();
			}
		}
		
		if menu_alert
		{
			//display menu alert messages here
			draw_reset();
			draw_set_font(fDisplay);
			draw_text(RESOLUTION_W - 300, RESOLUTION_H/2 + float_txt, "Gray has gained " + string(r_amt) + " " + r_type)
			draw_reset();
		}
		
	
	}
	}
	else
	{
		
		prepCovcom();
	}
}

