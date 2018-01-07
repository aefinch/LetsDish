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
using Microsoft.AspNet.Identity;

namespace LetsDish.Controllers
{
	[Authorize]
    public class RecipesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Recipes
        public IQueryable<Recipe> GetRecipe()
        {
            return db.Recipe;
        }
		// GET: api/Recipes/forUser/5
		[ResponseType(typeof(Recipe))]
		[HttpGet, Route("api/Recipes/forUser")]
		public HttpResponseMessage GetRecipesByUser()
		{
			var currentUser = User.Identity.GetUserId().ToString();
			var recipes = db.Recipe.Where(recipe => recipe.User.Id == currentUser);
			return Request.CreateResponse(HttpStatusCode.OK, recipes.ToList());
		}
		// GET: api/Recipes/5
		[ResponseType(typeof(Recipe))]
        public IHttpActionResult GetRecipe(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // PUT: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe(int id, [FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult PostRecipe(Recipe recipe)
        {
			var currentUser = User.Identity.GetUserId().ToString();
			recipe.User = db.Users.Find(currentUser);

			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recipe.Add(recipe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult DeleteRecipe(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            db.Recipe.Remove(recipe);
            db.SaveChanges();

            return Ok(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeExists(int id)
        {
            return db.Recipe.Count(e => e.RecipeId == id) > 0;
        }
    }
}