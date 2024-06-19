using System.Collections.Generic;
using System.Web.Mvc;
namespace GYMProject.Models
{
    

    public class SessionViewModel
    {
        public List<Session> Sessions { get; set; }
        public Session NewSession { get; set; }
        public IEnumerable<SelectListItem> Coaches { get; set; }
        public IEnumerable<SelectListItem> SessionTypes { get; set; }
    }
}
