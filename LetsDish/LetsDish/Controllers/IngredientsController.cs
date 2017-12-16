using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LetsDish.Models;

namespace LetsDish.Controllers
{
	[Authorize]
    public class IngredientsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Ingredients
        public List<Ingredient> GetIngredient()
        {
            return db.Ingredient.ToList();
        }

        // GET: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult GetIngredient(int id)
        {
            Ingredient ingredient = db.Ingredient.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }
		// GET: api/Ingredients/forRecipe/5
		[ResponseType(typeof(Ingredient))]
		[HttpGet, Route("api/Ingredients/forRecipe/{recipeId}")]
		public HttpResponseMessage GetIngredientsByRecipe(int recipeId)
		{
			var db = new ApplicationDbContext();
			var ingredients = db.Ingredient.Where(ingredient => ingredient.Recipe.RecipeId == recipeId);
			return Request.CreateResponse(HttpStatusCode.OK, ingredients.ToList());
		}

		// PUT: api/Ingredients/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutIngredient(int id, Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ingredients
        [ResponseType(typeof(AddIngredient))]
        public IHttpActionResult PostIngredient(AddIngredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			var newIngredient = new Ingredient
			{
				IngredientDescription = ingredient.IngredientDescription,
				OnShoppingList = ingredient.OnShoppingList,
				Recipe = db.Recipe.Find(ingredient.RecipeId)
		
			};

            db.Ingredient.Add(newIngredient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult DeleteIngredient(int id)
        {
            Ingredient ingredient = db.Ingredient.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            db.Ingredient.Remove(ingredient);
            db.SaveChanges();

            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientExists(int id)
        {
            return db.Ingredient.Count(e => e.IngredientId == id) > 0;
        }
    }
}