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
    public class EventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Events
        public IQueryable<Event> GetEvent()
        {
            return db.Event;
        }
		// GET: api/Events/forUser
		[ResponseType(typeof(Event))]
		[HttpGet, Route("api/Events/forUser")]
		public HttpResponseMessage GetEventsByUser()
		{
			var currentUser = User.Identity.GetUserId().ToString();
			var events = db.Event.Where(@event => @event.User.Id == currentUser);
			return Request.CreateResponse(HttpStatusCode.OK, events.ToList());
		}
		
		// GET: api/Events/5
		[ResponseType(typeof(Event))]
        public IHttpActionResult GetEvent(int id)
        {
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, Event @event)
        {
			
			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(Event @event)
        {
			var currentUser = User.Identity.GetUserId().ToString();
			@event.User = db.Users.Find(currentUser);

			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Event.Add(@event);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @event.EventId }, @event);
        }
		//POST: api/RecipeEvents/5/6
		[ResponseType(typeof(Event))]
		[HttpPost, Route("api/Events/{eventId}/{recipeId}")]
		public IHttpActionResult PostEvent(int eventId, int recipeId)
		{
			
			var @event = db.Event.Find(eventId);
			var recipe = db.Recipe.Find(recipeId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			@event.EventRecipes.Add(recipe);
			db.SaveChanges();
			return Ok();

		}

		// DELETE: api/Events/5
		[ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Event.Remove(@event);
            db.SaveChanges();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Event.Count(e => e.EventId == id) > 0;
        }
    }
}