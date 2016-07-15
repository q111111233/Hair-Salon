using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;
using HairSalonList.objects;

namespace HairSalonList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"]= _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Post["/"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        return View["index.cshtml", Stylist.GetAll()];
      };
    }
  }
}
