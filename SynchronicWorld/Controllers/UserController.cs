using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynchronicWorld.Models;

namespace SynchronicWorld.Controllers
{
    public class UserController : Controller
    {
        DbEntities db = new DbEntities();

      
        /* RETOURNE LA VUE INSCRIPTION (FORMULAIRE) */
        [HttpGet]
        public ActionResult Inscription()
        {
            User user = new User();
            return View(user);
        }

        /* TRAITE LA REQUETE POST POUR ENVREGISTRER L'USER */
        [HttpPost]
        public ActionResult Inscription(User perso)
        {
            /* Creation de la DB et table, ajout de la l'utilisateur grâce à un formulaire */
            perso.Role = "user";
            db.UserTable.Add(perso);
            db.SaveChanges();
            User user = db.UserTable.Where(p => p.UserName == perso.UserName && p.UserPassword == perso.UserPassword)
                    .FirstOrDefault<User>();
            Session["Id"] = user.Id;
            return Redirect("/User/Connexion");
        }



        /* Retourne la vue Connexion */
        public ActionResult Connexion()
        {
            return View();
        }

        /* Check avec la DB si les identifiants mdp et username sont bons + attribution de l'id de l'user à celui de la session */
        [HttpPost]
        public ActionResult Connexion (User u)
        {
            User user = db.UserTable.Where(p => p.UserName == u.UserName && p.UserPassword == u.UserPassword).FirstOrDefault<User>();
            if (user != null)
            {
                Session["Id"] = user.Id;
                FriendViewModel friendViewModel = new FriendViewModel(user.Id);
                return View("Account", friendViewModel);

            }
            else
            {
                return View();
            }
        }


        /* RETOURNE LA VUE ACCOUNT */
        [HttpGet]
        public ActionResult Account()
        {         
            int userid = (int)Session["Id"];          
            FriendViewModel friendViewModel = new FriendViewModel(userid);
            return View(friendViewModel);
          
        }

        /* RETOURNE LA VUE EDITPROFILE */
        [HttpGet]
        public ActionResult EditProfile()
        {
            int id = (int) Session["Id"];
            if (id != null)
            {
                User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();
                return View("EditProfile", u);
            }
            else
            {
                return View("Connexion");
            }
        }


        /* Check l'id de session à l'id de l'user si true alors il permet enregistre le nouveau profil */
        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            int id = (int)Session["Id"];
            User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();
            u.UserName = user.UserName;
            u.UserPassword = user.UserPassword;
            db.SaveChanges();
            FriendViewModel friendViewModel = new FriendViewModel(id);
            return View("Account",friendViewModel);
        }


        /* RETOURNE LA VUE LISTEVENT */
        public ActionResult ListEvent()
        {
            int id = (int)Session["Id"];
            User u = db.UserTable.Where(a => a.Id == id).FirstOrDefault();
            IEnumerable<Event> userEvents = db.EventTable.Where(e => e.UserId == u.Id).ToList<Event>();

            return View(userEvents);
        }



        [HttpPost]
        public ActionResult Search(String search)
        {
            int id = (int)Session["Id"];
            FriendViewModel friendViewModel = new FriendViewModel(search, id);
            return View("Account", friendViewModel);
        }

        /* AJOUT D'UN AMI */
        [HttpGet]
        public ActionResult Add(int id)
        {
            int userid = (int)Session["Id"];
            Friend friend = new Friend();
            friend.FriendId1 = userid;
            friend.FriendId2 = id;
            db.FriendsTable.Add(friend);
            db.SaveChanges();
            FriendViewModel friendViewModel = new FriendViewModel(userid);
            return View("Account", friendViewModel);
        }

        /* SUPPRESSION D'UN AMI */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int userid = (int)Session["Id"];
            Friend friend = db.FriendsTable.Where(f => f.FriendId1 == userid && f.FriendId2 == id).FirstOrDefault();
            db.FriendsTable.Remove(friend);
            db.SaveChanges();
            FriendViewModel friendViewModel = new FriendViewModel(userid);
            return View("Account", friendViewModel);
        }
    }
}