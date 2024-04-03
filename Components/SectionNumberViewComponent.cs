using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Components
{
	// [ViewComponent]
	public class SectionNumberViewComponent : ViewComponent 
	{
		private IIntexRepository _repo;
        public SectionNumberViewComponent(IIntexRepository temp) //check to see if this is right repo
        {
			_repo = temp;
        }
        public IViewComponentResult Invoke()
		{
			IEnumerable<short> sections = _repo.Groups
				.Select(x => x.Section)
				.Distinct()
				.OrderBy(x => x);

			return View(sections);
		}
	}
}
