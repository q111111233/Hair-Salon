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

      Delete["/delete_stylists/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();
        return View["index.cshtml", Stylist.GetAll()];
      };

      Get["/stylists/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["clients.cshtml", SelectedStylist];
      };

      Post["/stylists/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        Client newClient = new Client(Request.Form["client-name"], parameters.id);
        newClient.Save();
        return View["clients.cshtml", SelectedStylist];
      };

      Delete["/delete_clients/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Delete();
        return View["clients.cshtml", Stylist.Find(SelectedClient.GetStylistId())];
      };
    }
  }
}
