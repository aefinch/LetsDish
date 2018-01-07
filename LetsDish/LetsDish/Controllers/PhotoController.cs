using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiAngularJsUploader.Controllers.Photo;

namespace WebApiAngularJsUploader.Controllers
{
	[RoutePrefix("api/photo")]
	public class PhotoController : ApiController
	{
		private IPhotoManager photoManager;

		public PhotoController()
			: this(new LocalPhotoManager(HttpRuntime.AppDomainAppPath + @"\images"))
		{
		}

		public PhotoController(IPhotoManager photoManager)
		{
			this.photoManager = photoManager;
		}

		// POST: api/Photo/5
		public async Task<IHttpActionResult> Post()
		{
			// Check if the request contains multipart/form-data.
			if (!Request.Content.IsMimeMultipartContent("form-data"))
			{
				return BadRequest("Unsupported media type");
			}

			try
			{
				var photos = await photoManager.Add(Request);
				//var newfilename = photos[0].name;
				//File.Move(@".\images\${photos[0].name}", @".\images\${newfilename}");
				return Ok(new { Message = "Photos uploaded ok", Photos = photos });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.GetBaseException().Message);
			}
		}
	}
}
