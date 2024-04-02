using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Components
{
    [ViewComponent]
    public class SectionNumberViewComponent : ViewComponent 
	{
		public string Invoke()
		{
			return "this worked";
		}
	}
}
