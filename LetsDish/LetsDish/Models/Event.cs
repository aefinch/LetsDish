using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsDish.Models
{
	public class Event
	{
		[Key]
		public int EventId { get; set; }
		public string EventName { get; set; }
		public virtual List<Recipe> Recipes { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}