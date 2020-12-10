using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sppo.Areas.Identity.Data;
using sppo.Data;
using SPPO.EntityModels;

namespace sppo.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<Profile> _userManager;


        public ReviewController(MyContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void SaveRating(int grade,string reciverId)
        {
            var agV = _context.accountGrades.Where(a => a.RecieverID == reciverId && a.GiversID == _userManager.GetUserId(User)).FirstOrDefault();
            if (agV == null)
            {

                AccountGrade ag = new AccountGrade
                {
                    GiversID = _userManager.GetUserId(User),
                    Givers=_context.profiles.Find(_userManager.GetUserId(User)),
                    RecieverID = reciverId,
                    Reciever=_context.profiles.Find(reciverId),
                    Grade=grade  
                };
            _context.Add(ag);
            }
            else
            {
                agV.Grade = grade;
            }
            _context.SaveChanges();
        }
        public string SaveComment(string comment, string reciverId )
        {
            Review r = new Review
            {
                Commentary = comment,
                ReciverId = reciverId,
                Giver = _context.profiles.Find(_userManager.GetUserId(User)),
                GiverId = _userManager.GetUserId(User),
                Reciver = _context.profiles.Find(reciverId),
                PostDate = DateTime.Now

            };
            _context.Add(r);
            _context.SaveChanges();
            var user = _userManager.GetUserId(User);
            var user1 = _context.profiles.Find(user);
            return user1.ProfilePicture;
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.reviews.Where(x => x.Id == commentId).FirstOrDefault();
            _context.Remove(comment);
            _context.SaveChanges();
        }
        public void Edit(int id, string comment)
        { 
            var com = _context.reviews.Where(x => x.Id == id).FirstOrDefault();
            com.Commentary = comment;
            _context.SaveChanges();
        
            
        }
    }
}
