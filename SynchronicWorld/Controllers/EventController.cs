using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynchronicWorld.Models;

namespace SynchronicWorld.Controllers
{
    public class EventController : Controller
    {
        DbEntities db = new DbEntities();

        // GET: Event
        [HttpGet]
        public ActionResult CreateEvent()
        {
            if (Session["Id"] != null) // Check l'id de la session en cours 
            {
                Event e = new Event();
                
                // Liste des types d'event en les mettant dans une liste déroulante
                List<EventType> eventTypesList = db.EventTypeTable.OrderBy(d => d.Type).ToList<EventType>();
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (EventType ev in eventTypesList)
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Text = ev.Type,
                        Value = ev.Id.ToString()
                    });
                }

                ViewData["types"] = selectListItems;
                return View(e);
            }
            // Redirige vers l'action "Connexion"
            return RedirectToAction("Connexion", "User");
        }


        [HttpPost]
        public ActionResult CreateEvent(Event e, String Type)
        {
            if (ModelState.IsValid) // Check si le formulaire a bien été remplie
            {          
                    e.TypeId = int.Parse(Type);
                    e.UserId = (int)Session["Id"];
                    db.EventTable.Add(e);
                    db.SaveChanges();
                    return RedirectToAction("Account", "User");           
            }
            return View();
        }




        /* RENVOIE VERS LA VUE EDITEVENT */
        [HttpGet]
        public ActionResult EditEvent(int Id)
        {
            int id = (int)Session["Id"];
            User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();  // Prend le premier id correspondant trouvé
            Event ev = new Event();                                         // Pour savoir quelles sont les events lui appartenant
            ev = db.EventTable.Where(e => e.Id == Id).FirstOrDefault<Event>();
            return View(ev);
        }


        /* ENVOIE UN POST POUR EDITER EVENT DANS LA DB */
        [HttpPost]
        public ActionResult EditEvent(Event currEvent, int Id)
        {
            int id = (int)Session["Id"];
            User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();
            Event ev = db.EventTable.Where(e => e.Id == Id).FirstOrDefault<Event>();
            ev.EventName = currEvent.EventName;
            ev.Address = currEvent.Address;
            ev.DateDeDebut = currEvent.DateDeDebut;
            ev.DateDeFin = currEvent.DateDeFin;
            ev.Description = currEvent.Description;
            db.SaveChanges();
            List<Event> listEvent = db.EventTable.ToList<Event>();
            return RedirectToAction("ListEvent", "User",listEvent);
        }
      


        /* SUPPRESSION DE L'EVENT DE LA LIGNE EN COURS */
        public ActionResult DelEvent(int Id)
        {  
            int id = (int)Session["Id"];
            User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();
            Event ev = new Event();
            if (Session["Id"] != null)
            {
                ev = db.EventTable.Where(e => e.Id == Id).FirstOrDefault<Event>();
                db.EventTable.Remove(ev);
                db.SaveChanges();
            }
            return RedirectToAction("ListEvent", "User");
        }
    }
}