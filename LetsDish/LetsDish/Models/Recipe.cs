using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsDish.Models
{
	public class Recipe
	{
		[Key]
		public int RecipeId { get; set; }
		public string RecipeName { get; set; }
		public string Instructions { get; set; }
		public string Yield { get; set; }
		public string RecipeSource { get; set; }
		public string MinutesToMake { get; set; }
		public int Rating { get; set; }
		public string Picture { get; set; }
		public string Notes { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual List<Event> Events { get; set; }
	}
}