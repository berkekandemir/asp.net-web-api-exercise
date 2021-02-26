using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Delta.Data;
using Delta.Models;

namespace Delta.Controllers
{
    public class TeamsController : ApiController
    {
        private DeltaDbContext db = new DeltaDbContext();

        // GET: api/Teams
        public ICollection<TeamResponse> GetTeams()
        {
            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            ICollection<TeamResponse> teamsToReturn = new List<TeamResponse>();

            for (int i = 0; i < teamArray.Length; i++)
            {
                TeamResponse response = new TeamResponse();
                response.Id = teamArray[i].Id;
                response.Name = teamArray[i].Name;
                response.PresidentId = teamArray[i].PresidentId;
                response.CoachId = teamArray[i].CoachId;

                UserResponse president = new UserResponse
                {
                    Id = teamArray[i].President.Id,
                    Username = teamArray[i].President.Username,
                    Name = teamArray[i].President.Name,
                    Role = teamArray[i].President.Role
                };

                response.President = president;

                if (teamArray[i].CoachId != null)
                {
                    UserResponse coach = new UserResponse
                    {
                        Id = teamArray[i].Coach.Id,
                        Username = teamArray[i].Coach.Username,
                        Name = teamArray[i].Coach.Name,
                        Role = teamArray[i].Coach.Role
                    };
                    response.Coach = coach;
                }
               
                /*ICollection<UserResponse> players = new List<UserResponse>();

                for (int j = 0; j < teamArray[i].Players.Count(); j++)
                {
                    UserResponse player = new UserResponse
                    {
                        Id = teamArray[i].Players.ElementAt(j).Id,
                        Username = teamArray[i].Players.ElementAt(j).Username,
                        Name = teamArray[i].Players.ElementAt(j).Name,
                        Role = teamArray[i].Players.ElementAt(j).Role
                    };
                    players.Add(player);
                }
                response.Players = players;*/
                //response.PlayerIds = teamArray[i].PlayerIds;

                teamsToReturn.Add(response);
            }

            return teamsToReturn;


            /*return db.Teams
                .Include(a => a.Coach)
                .Include(b => b.President)
                .Include(c => c.Players);*/
        }

        // GET: api/Teams/5
        [ResponseType(typeof(TeamResponse))]
        public IHttpActionResult GetTeam(int id)
        {
            /*Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);*/

            Team team = null;
            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].Id == id)
                {
                    team = teamArray[i];
                }
            }
            if (team == null)
            {
                return NotFound();
            }

            TeamResponse response = new TeamResponse();
            response.Id = team.Id;
            response.Name = team.Name;
            response.PresidentId = team.PresidentId;
            response.CoachId = team.CoachId;

            UserResponse president = new UserResponse
            {
                Id = team.President.Id,
                Username = team.President.Username,
                Name = team.President.Name,
                Role = team.President.Role
            };
            response.President = president;

            if (team.CoachId != null)
            {
                UserResponse coach = new UserResponse
                {
                    Id = team.Coach.Id,
                    Username = team.Coach.Username,
                    Name = team.Coach.Name,
                    Role = team.Coach.Role
                };
                response.Coach = coach;
            }

            /*ICollection<UserResponse> players = new List<UserResponse>();

            for (int j = 0; j < team.Players.Count(); j++)
            {
                UserResponse player = new UserResponse
                {
                    Id = team.Players.ElementAt(j).Id,
                    Username = team.Players.ElementAt(j).Username,
                    Name = team.Players.ElementAt(j).Name,
                    Role = team.Players.ElementAt(j).Role
                };
                players.Add(player);
            }
            response.Players = players;*/
            //response.PlayerIds = team.PlayerIds;

            return Ok(response);
        }

        // GET: api/Teams/by/Name
        [Route("api/Teams/by/{name}")]
        [ResponseType(typeof(TeamResponse))]
        public IHttpActionResult GetTeamByName(string name)
        {
            /*Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);*/

            Team team = null;
            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].Name == name)
                {
                    team = teamArray[i];
                }
            }
            if (team == null)
            {
                return NotFound();
            }

            TeamResponse response = new TeamResponse();
            response.Id = team.Id;
            response.Name = team.Name;
            response.PresidentId = team.PresidentId;
            response.CoachId = team.CoachId;

            UserResponse president = new UserResponse
            {
                Id = team.President.Id,
                Username = team.President.Username,
                Name = team.President.Name,
                Role = team.President.Role
            };
            response.President = president;

            if (team.CoachId != null)
            {
                UserResponse coach = new UserResponse
                {
                    Id = team.Coach.Id,
                    Username = team.Coach.Username,
                    Name = team.Coach.Name,
                    Role = team.Coach.Role
                };
                response.Coach = coach;
            }

            /*ICollection<UserResponse> players = new List<UserResponse>();

            for (int j = 0; j < team.Players.Count(); j++)
            {
                UserResponse player = new UserResponse
                {
                    Id = team.Players.ElementAt(j).Id,
                    Username = team.Players.ElementAt(j).Username,
                    Name = team.Players.ElementAt(j).Name,
                    Role = team.Players.ElementAt(j).Role
                };
                players.Add(player);
            }
            response.Players = players;*/
            //response.PlayerIds = team.PlayerIds;

            return Ok(response);
        }

        // GET: api/Teams/by/CoachId
        [Route("api/Teams/by/Coach/{coachId}")]
        [ResponseType(typeof(TeamResponse))]
        public IHttpActionResult GetTeamByCoachId(int coachId)
        {

            Team team = null;
            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].CoachId == coachId)
                {
                    team = teamArray[i];
                }
            }
            if (team == null)
            {
                return NotFound();
            }

            TeamResponse response = new TeamResponse();
            response.Id = team.Id;
            response.Name = team.Name;
            response.PresidentId = team.PresidentId;
            response.CoachId = team.CoachId;

            UserResponse president = new UserResponse
            {
                Id = team.President.Id,
                Username = team.President.Username,
                Name = team.President.Name,
                Role = team.President.Role
            };
            response.President = president;

            UserResponse coach = new UserResponse
            {
                Id = team.Coach.Id,
                Username = team.Coach.Username,
                Name = team.Coach.Name,
                Role = team.Coach.Role
            };
            response.Coach = coach;

            return Ok(response);
        }

        // GET: api/Teams/by/Name
        [Route("api/Teams/by/President/{presidentId}")]
        [ResponseType(typeof(ICollection<TeamResponse>))]
        public IHttpActionResult GetTeamByPresidentId(int presidentId)
        {
            ICollection<TeamResponse> listToReturn = new List<TeamResponse>();
            Team team = null;
            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            ICollection<Team> teams = new List<Team>();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].PresidentId == presidentId)
                {
                    team = teamArray[i];
                    teams.Add(team);
                }
            }
            if (team == null)
            {
                return NotFound();
            }

            for (int i = 0; i < teams.Count(); i++)
            {
                TeamResponse response = new TeamResponse();
                response.Id = team.Id;
                response.Name = team.Name;
                response.PresidentId = team.PresidentId;
                response.CoachId = team.CoachId;

                UserResponse president = new UserResponse
                {
                    Id = team.President.Id,
                    Username = team.President.Username,
                    Name = team.President.Name,
                    Role = team.President.Role
                };
                response.President = president;

                if (teamArray[i].CoachId != null)
                {
                    UserResponse coach = new UserResponse
                    {
                        Id = teamArray[i].Coach.Id,
                        Username = teamArray[i].Coach.Username,
                        Name = teamArray[i].Coach.Name,
                        Role = teamArray[i].Coach.Role
                    };
                    response.Coach = coach;
                }

                listToReturn.Add(response);
            }

            return Ok(listToReturn);
        }

        // PUT: api/Teams/5
        [Route("api/Teams/{id}/teamRequest")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeam(int id, TeamRequest teamRequest, [FromUri] int[] playerIds = null)
        {
            User[] userArray = db.Users.ToArray();
            int count = 0;
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i].TeamId == id)
                {
                    count++;
                }
            }

            if ((count + playerIds.Count()) > 12)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("A team cannot have more than 12 players!")
                )
            );
            }

            Team[] teamArray = db.Teams.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if ((teamArray[i].CoachId == teamRequest.CoachId) && (teamArray[i].Id != teamRequest.Id) && (teamRequest.CoachId != null))
                {
                    return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("A coach cannot have more than one team!")
                )
            );
                }
            }

            Team team = db.Teams.Find(teamRequest.Id);
            //Team team = new Team();
            team.Id = teamRequest.Id;
            team.Name = teamRequest.Name;
            team.PresidentId = teamRequest.PresidentId;
            /*if (teamRequest.CoachId != null)
            {
                
            }*/
            team.CoachId = teamRequest.CoachId;

            //team.PlayerIds = playerIds;

            /*for (int i = 0; i < playerIds.Count(); i++)
            {
                team.PlayerIds[i] = playerIds[i];
                //team.PlayerIds.Add(playerIds.ElementAt(i));
            }*/
            //team.PlayerIds = teamRequest.PlayerIds;
            
            /*for (int i = 0; i < teamRequest.PlayerIds.Count; i++)
            {
                team.Players.Add(db.Users.Find(teamRequest.PlayerIds.ElementAt(i)));
            }*/

            for (int i = 0; i < playerIds.Length; i++)
            {
                User user = db.Users.Find(playerIds[i]);
                user.TeamId = team.Id;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Id)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teams
        [ResponseType(typeof(Team))]
        public IHttpActionResult PostTeam(TeamRequest teamRequest, [FromUri] int[] playerIds = null)
        {
            if (playerIds.Count() > 12)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("A team cannot have more than 12 players!")
                )
            );
            }

            Team[] teamArray = db.Teams.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if ((teamArray[i].CoachId == teamRequest.CoachId) && (teamArray[i].Id != teamRequest.Id) && (teamRequest.CoachId != null))
                {
                    return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("A coach cannot have more than one team!")
                )
            );
                }
            }

            Team team = new Team();
            team.Name = teamRequest.Name;
            team.PresidentId = teamRequest.PresidentId;
            /*if (teamRequest.CoachId != null)
            {
                
            }*/
            team.CoachId = teamRequest.CoachId;

            //team.PlayerIds = playerIds;

            /*for (int i = 0; i < playerIds.Count(); i++)
            {
                team.PlayerIds[i] = playerIds[i];
                //team.PlayerIds.Add(teamRequest.PlayerIds.ElementAt(i));
            }*/
            //team.PlayerIds = teamRequest.PlayerIds;

            /*if (teamRequest.PlayerIds != null)
            {
                User[] userArray = db.Users.ToArray();
                for (int i = 0; i < teamRequest.PlayerIds.Count; i++)
                {
                    team.Players.Add(db.Users.Find(teamRequest.PlayerIds.ElementAt(i)));
                }
            }*/

            for (int i = 0; i < playerIds.Length; i++)
            {
                User user = db.Users.Find(playerIds[i]);
                user.TeamId = team.Id;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [ResponseType(typeof(Team))]
        public IHttpActionResult DeleteTeam(int id)
        {
            User[] userArray = db.Users.ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i].TeamId == id)
                {
                    userArray[i].TeamId = null;
                }
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.Id == id) > 0;
        }
    }
}