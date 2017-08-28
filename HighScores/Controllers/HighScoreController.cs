using HighScore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HighScore.Controllers
{
    public class HighScoreController : ApiController
    {
        TravieIOEntities db = new TravieIOEntities();

        //get all scores
        [HttpGet]
        public IEnumerable<HighScore> Get()
        {
            return db.HighScores.AsEnumerable();
        }

        //get score by id
        public HighScores Get(int id)
        {
            HighScores highScores = db.HighScores.Find(id);
            if (highScore == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return highScore;
        }

        //insert score
        public HttpResponseMessage Post(HighScores highScores)
        {
            if (ModelState.IsValid)
            {
                db.HighScores.Add(highScore);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, highScores);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = highScores.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update score
        public HttpResponseMessage Put(int id, HighScore highScore)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != highScore.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            db.Entry(highScore).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete score by id
        public HttpResponseMessage Delete(int id)
        {
            HighScores highScore = db.HighScores.Find(id);
            if (highScore == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.HighScores.Remove(highScore);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, highScore);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
