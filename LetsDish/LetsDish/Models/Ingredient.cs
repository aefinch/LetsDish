using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LetsDish.Models
{
	public class Ingredient
	{
		[Key]
		public int IngredientId { get; set; }
		public string IngredientName { get; set; }
		public decimal Quantity { get; set; }
		public string UnitOfMeasure { get; set; }
		public bool OnShoppingList { get; set; }
		public virtual Recipe Recipe {get; set;}
	}
}