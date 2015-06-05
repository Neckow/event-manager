using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynchronicWorld.Models
{
    public class FriendViewModel
    {
        public List<User> usersToFollowList; // User pas encore amis
        public List<User> usersFollowedList; // User amis

        public FriendViewModel(String searchResult,int currId)
        {
            DbEntities db = new DbEntities();
            usersToFollowList = db.UserTable.Where(u => u.UserName.Contains(searchResult) || u.Name.Contains(searchResult)).ToList();
            usersFollowedList = new List<User>();
            List<Friend> friendList = db.FriendsTable.Where(u => u.FriendId1 == currId).ToList(); // recupere tous les champs de la table ou le champs friend1 == CurrId

            foreach (Friend f in friendList)
            {
                User u = db.UserTable.Where(user => user.Id == f.FriendId2).FirstOrDefault(); // recupere les noms correspondant à chaque friendId2
                usersFollowedList.Add(u);
                if (usersToFollowList.Contains(u)) 
                {
                    usersToFollowList.Remove(u); // Supprime les amis dans les resulats de recherche
                }
            }
        }

        public FriendViewModel(int currId)
        {
            DbEntities db = new DbEntities();
            usersToFollowList = new List<User>();
            usersFollowedList = new List<User>();
            List<Friend> friendList = db.FriendsTable.Where(u => u.FriendId1 == currId).ToList(); // recupere tous les champs de la table ou le champs friend1 == CurrId

            foreach (Friend f in friendList)
            {
                User u = db.UserTable.Where(user => user.Id == f.FriendId2).FirstOrDefault(); // recupere les noms correspondant à chaque friendId2
                usersFollowedList.Add(u);
            }
        }
    }
}