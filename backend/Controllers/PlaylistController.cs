using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	public class Playlist : Controller
	{
		// GET: Playlist
		public ActionResult Index()
		{
			return View();
		}

	}
}
