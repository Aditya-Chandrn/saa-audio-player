using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	public class Audio : Controller
	{

		public ActionResult GetAudio()
		{
			return View();
		}

	}
}
