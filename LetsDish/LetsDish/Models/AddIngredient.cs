using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LetsDish.Models
{
	public class AddIngredient
	{
		public int IngredientId { get; set; }
		public string IngredientDescription { get; set; }
		public bool OnShoppingList { get; set; }
		public int RecipeId { get; set; }
	}
}