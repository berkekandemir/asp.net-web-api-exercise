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
    public class UsersController : ApiController
    {
        private DeltaDbContext db = new DeltaDbContext();

        // GET: api/Users
        public ICollection<UserResponseWithTeam> GetUsers() //IQueryable<User>
        {
            User[] userArray = db.Users.Include(a => a.Team).ToArray();
            ICollection<UserResponseWithTeam> usersToReturn = new List<UserResponseWithTeam>();

            for (int i = 0; i < userArray.Length; i++)
            {
                UserResponseWithTeam response = new UserResponseWithTeam();
                response.Id = userArray[i].Id;
                response.Username = userArray[i].Username;
                response.Name = userArray[i].Name;
                response.Role = userArray[i].Role;
                response.TeamId = userArray[i].TeamId;

                Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
                Team team = null;
                for (int j = 0; j < teamArray.Length; j++)
                {
                    if (teamArray[j].Id == userArray[i].TeamId)
                    {
                        team = teamArray[j];
                    }
                }
                if (team != null)
                {
                    TeamResponse teamResponse = new TeamResponse();
                    teamResponse.Id = team.Id;
                    teamResponse.Name = team.Name;
                    teamResponse.PresidentId = team.PresidentId;
                    teamResponse.CoachId = team.CoachId;

                    UserResponse president = new UserResponse
                    {
                        Id = team.President.Id,
                        Username = team.President.Username,
                        Name = team.President.Name,
                        Role = team.President.Role
                    };
                    teamResponse.President = president;

                    if (team.CoachId != null)
                    {
                        UserResponse coach = new UserResponse
                        {
                            Id = team.Coach.Id,
                            Username = team.Coach.Username,
                            Name = team.Coach.Name,
                            Role = team.Coach.Role
                        };
                        teamResponse.Coach = coach;
                    }
                    //teamResponse.Coach = coach;

                    /*ICollection<UserResponse> players = new List<UserResponse>();

                    for (int k = 0; k < team.Players.Count(); k++)
                    {
                        UserResponse player = new UserResponse
                        {
                            Id = team.Players.ElementAt(k).Id,
                            Username = team.Players.ElementAt(k).Username,
                            Name = team.Players.ElementAt(k).Name,
                            Role = team.Players.ElementAt(k).Role
                        };
                        players.Add(player);
                    }
                    teamResponse.Players = players;*/
                    //teamResponse.PlayerIds = team.PlayerIds;
                    response.Team = teamResponse;
                }
                else
                {
                    response.Team = null;
                }

                usersToReturn.Add(response);
            }

            return usersToReturn; /*db.Users
                .Include(a => a.Team);*/
        }

        // GET: api/Users/5
        [Route("api/Users/{id}")]
        [ResponseType(typeof(UserResponseWithTeam))]
        public IHttpActionResult GetUserById(int id)
        {
            /*User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            UserResponseWithTeam response = new UserResponseWithTeam();
            response.Id = user.Id;
            response.Username = user.Username;
            response.Name = user.Name;
            response.Role = user.Role;
            response.TeamId = user.TeamId;
            response.Team = user.Team;

            return Ok(response);*/

            User user = null;
            User[] userArray = db.Users.Include(a => a.Team).ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i].Id == id)
                {
                    user = userArray[i];
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            UserResponseWithTeam response = new UserResponseWithTeam();
            response.Id = user.Id;
            response.Username = user.Username;
            response.Name = user.Name;
            response.Role = user.Role;
            response.TeamId = user.TeamId;

            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            Team team = null;
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].Id == user.TeamId)
                {
                    team = teamArray[i];
                }
            }
            if (team != null)
            {
                TeamResponse teamResponse = new TeamResponse();
                teamResponse.Id = team.Id;
                teamResponse.Name = team.Name;
                teamResponse.PresidentId = team.PresidentId;
                teamResponse.CoachId = team.CoachId;

                UserResponse president = new UserResponse
                {
                    Id = team.President.Id,
                    Username = team.President.Username,
                    Name = team.President.Name,
                    Role = team.President.Role
                };
                teamResponse.President = president;

                if (team.CoachId != null)
                {
                    UserResponse coach = new UserResponse
                    {
                        Id = team.Coach.Id,
                        Username = team.Coach.Username,
                        Name = team.Coach.Name,
                        Role = team.Coach.Role
                    };
                    teamResponse.Coach = coach;
                }
                //teamResponse.Coach = coach;

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
                teamResponse.Players = players;*/
                //teamResponse.PlayerIds = team.PlayerIds;
                response.Team = teamResponse;
            }
            else
            {
                response.Team = null;
            }
          
            return Ok(response);
        }

        // GET: api/Users/by/Username
        [Route("api/Users/by/{username}")]
        [ResponseType(typeof(UserResponseWithTeam))]
        public IHttpActionResult GetUserByUsername(string username)
        {
            User user = null;
            User[] userArray = db.Users.Include(a => a.Team).ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i].Username == username)
                {
                    user = userArray[i];
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            UserResponseWithTeam response = new UserResponseWithTeam();
            response.Id = user.Id;
            response.Username = user.Username;
            response.Name = user.Name;
            response.Role = user.Role;
            response.TeamId = user.TeamId;

            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            Team team = null;
            for (int j = 0; j < teamArray.Length; j++)
            {
                if (teamArray[j].Id == user.TeamId)
                {
                    team = teamArray[j];
                }
            }
            if (team != null)
            {
                TeamResponse teamResponse = new TeamResponse();
                teamResponse.Id = team.Id;
                teamResponse.Name = team.Name;
                teamResponse.PresidentId = team.PresidentId;
                teamResponse.CoachId = team.CoachId;

                UserResponse president = new UserResponse
                {
                    Id = team.President.Id,
                    Username = team.President.Username,
                    Name = team.President.Name,
                    Role = team.President.Role
                };
                teamResponse.President = president;

                if (team.CoachId != null)
                {
                    UserResponse coach = new UserResponse
                    {
                        Id = team.Coach.Id,
                        Username = team.Coach.Username,
                        Name = team.Coach.Name,
                        Role = team.Coach.Role
                    };
                    teamResponse.Coach = coach;
                }
                //teamResponse.Coach = coach;

                /*ICollection<UserResponse> players = new List<UserResponse>();

                for (int k = 0; k < team.Players.Count(); k++)
                {
                    UserResponse player = new UserResponse
                    {
                        Id = team.Players.ElementAt(k).Id,
                        Username = team.Players.ElementAt(k).Username,
                        Name = team.Players.ElementAt(k).Name,
                        Role = team.Players.ElementAt(k).Role
                    };
                    players.Add(player);
                }
                teamResponse.Players = players;*/
                //teamResponse.PlayerIds = team.PlayerIds;
                response.Team = teamResponse;
            }
            else
            {
                response.Team = null;
            }

            return Ok(response);
        }

        // GET: api/Users/Username/Password
        [Route("api/Users/by/{username}/{password}")]
        [ResponseType(typeof(UserResponseWithTeam))]
        public IHttpActionResult GetUserByUsernameAndPassword(string username, string password)
        {
            User user = null;
            User[] userArray = db.Users.Include(a => a.Team).ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if ((userArray[i].Username == username) && (userArray[i].Password == password))
                {
                    user = userArray[i];
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            UserResponseWithTeam response = new UserResponseWithTeam();
            response.Id = user.Id;
            response.Username = user.Username;
            response.Name = user.Name;
            response.Role = user.Role;
            response.TeamId = user.TeamId;

            Team[] teamArray = db.Teams.Include(a => a.Coach).Include(b => b.President)/*.Include(c => c.PlayerIds)*/.ToArray();
            Team team = null;
            for (int j = 0; j < teamArray.Length; j++)
            {
                if (teamArray[j].Id == user.TeamId)
                {
                    team = teamArray[j];
                }
            }
            if (team != null)
            {
                TeamResponse teamResponse = new TeamResponse();
                teamResponse.Id = team.Id;
                teamResponse.Name = team.Name;
                teamResponse.PresidentId = team.PresidentId;
                teamResponse.CoachId = team.CoachId;

                UserResponse president = new UserResponse
                {
                    Id = team.President.Id,
                    Username = team.President.Username,
                    Name = team.President.Name,
                    Role = team.President.Role
                };
                teamResponse.President = president;

                if (team.CoachId != null)
                {
                    UserResponse coach = new UserResponse
                    {
                        Id = team.Coach.Id,
                        Username = team.Coach.Username,
                        Name = team.Coach.Name,
                        Role = team.Coach.Role
                    };
                    teamResponse.Coach = coach;
                }
                //teamResponse.Coach = coach;

                /*ICollection<UserResponse> players = new List<UserResponse>();

                for (int k = 0; k < team.Players.Count(); k++)
                {
                    UserResponse player = new UserResponse
                    {
                        Id = team.Players.ElementAt(k).Id,
                        Username = team.Players.ElementAt(k).Username,
                        Name = team.Players.ElementAt(k).Name,
                        Role = team.Players.ElementAt(k).Role
                    };
                    players.Add(player);
                }
                teamResponse.Players = players;*/
                //teamResponse.PlayerIds = team.PlayerIds;
                response.Team = teamResponse;
            }
            else
            {
                response.Team = null;
            }

            return Ok(response);
        }

        // GET: api/Users/Team/TeamId
        [Route("api/Users/Team/{teamId}")]
        [ResponseType(typeof(ICollection<UserResponse>))]
        public IHttpActionResult GetUsersByTeamId(int teamId)
        {
            if (db.Teams.Find(teamId) == null)
            {
                return NotFound();
            }

            ICollection<UserResponse> usersToReturn = new List<UserResponse>();

            UserResponse president = new UserResponse();
            president.Id = db.Teams.Find(teamId).President.Id;
            president.Username = db.Teams.Find(teamId).President.Username;
            president.Name = db.Teams.Find(teamId).President.Name;
            president.Role = db.Teams.Find(teamId).President.Role;
            usersToReturn.Add(president);

            UserResponse coach = new UserResponse();
            if (db.Teams.Find(teamId).CoachId != null)
            {
                coach.Id = db.Teams.Find(teamId).Coach.Id;
                coach.Username = db.Teams.Find(teamId).Coach.Username;
                coach.Name = db.Teams.Find(teamId).Coach.Name;
                coach.Role = db.Teams.Find(teamId).Coach.Role;
                usersToReturn.Add(coach);
            }
            else
            {
                usersToReturn.Add(null);
            }
            

            User[] userArray = db.Users.ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if ((userArray[i].TeamId == teamId) && (userArray[i].Role == "player"))
                {
                    UserResponse player = new UserResponse();
                    player.Id = userArray[i].Id;
                    player.Username = userArray[i].Username;
                    player.Name = userArray[i].Name;
                    player.Role = userArray[i].Role;
                    usersToReturn.Add(player);
                }
            }

            return Ok(usersToReturn);
        }

        // GET: api/Users/FreeCoaches
        [Route("api/Users/FreeCoaches")]
        [ResponseType(typeof(ICollection<UserResponse>))]
        public IHttpActionResult GetFreeCoaches()
        {
            ICollection<UserResponse> usersToReturn = new List<UserResponse>();
            ICollection<int> coachIds = new List<int>();
            
            User[] userArray = db.Users.ToArray();
            for (int i = 0; i < userArray.Length; i++)
            {
                if ((userArray[i].Role == "coach"))
                {
                    coachIds.Add(userArray[i].Id);
                }
            }

            Team[] teamArray = db.Teams.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                for ( int j = 0; j < coachIds.Count(); j++)
                {
                    if (teamArray[i].CoachId == coachIds.ElementAt(j))
                    {
                        coachIds.Remove(coachIds.ElementAt(j));
                    }
                }
            }

            for (int i = 0; i < coachIds.Count(); i++)
            {
                UserResponse coach = new UserResponse();

                coach.Id = db.Users.Find(coachIds.ElementAt(i)).Id;
                coach.Username = db.Users.Find(coachIds.ElementAt(i)).Username;
                coach.Name = db.Users.Find(coachIds.ElementAt(i)).Name;
                coach.Role = db.Users.Find(coachIds.ElementAt(i)).Role;
                usersToReturn.Add(coach);
            }
            

            return Ok(usersToReturn);
        }

        // PUT: api/Users/5/userRequest
        [Route("api/Users/{id}/userRequest")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, UserRequest userRequest)
        {
            int count = 0;
            User[] userArray = db.Users.ToArray();
            if (userRequest.TeamId != null)
            {
                for (int i = 0; i < userArray.Length; i++)
                {
                    if ((userArray[i].TeamId == userRequest.TeamId) && (userArray[i].Role == "player"))
                    {
                        count++;
                    }
                }

                if (count > 12)
                {
                    return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError("A team cannot have more than 12 players!")
                    )
                );
                }
            }
            

            User user = db.Users.Find(userRequest.Id);
            //user.Id = userRequest.Id;
            user.Username = userRequest.Username;
            user.Password = userRequest.Password;
            user.Name = userRequest.Name;
            //user.Role = userRequest.Role;
            user.TeamId = userRequest.TeamId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/5/userRequestWithoutPassword
        [Route("api/Users/{id}/userRequestWithoutPassword")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserWithoutPassword(int id, UserRequestWithoutPassword userRequest)
        {
            int count = 0;
            User[] userArray = db.Users.ToArray();
            if (userRequest.TeamId != null)
            {
                for (int i = 0; i < userArray.Length; i++)
                {
                    if ((userArray[i].TeamId == userRequest.TeamId) && (userArray[i].Role == "player"))
                    {
                        count++;
                    }
                }

                if (count > 12)
                {
                    return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                        (HttpStatusCode)422,
                        new HttpError("A team cannot have more than 12 players!")
                    )
                );
                }
            }


            User user = db.Users.Find(userRequest.Id);
            //user.Id = userRequest.Id;
            user.Username = userRequest.Username;
            //user.Password = userRequest.Password;
            user.Name = userRequest.Name;
            //user.Role = userRequest.Role;
            user.TeamId = userRequest.TeamId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserRequest userRequest)
        {
            int count = 0;
            User[] userArray = db.Users.ToArray();
            if (userRequest.TeamId != null)
            {
                for (int i = 0; i < userArray.Length; i++)
                {
                    if ((userArray[i].TeamId == userRequest.TeamId) && (userArray[i].Role == "player"))
                    {
                        count++;
                    }
                }
            }

            if (count > 12)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("A team cannot have more than 12 players!")
                )
            );
            }

            User user = new User();
            user.Username = userRequest.Username;
            user.Password = userRequest.Password;
            user.Name = userRequest.Name;
            user.Role = userRequest.Role;
            if (userRequest.TeamId != null)
            {
                user.TeamId = userRequest.TeamId;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [Route("api/Users/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            Team[] teamArray = db.Teams.ToArray();
            for (int i = 0; i < teamArray.Length; i++)
            {
                if (teamArray[i].CoachId == id)
                {
                    teamArray[i].CoachId = null;
                } 
                else if (teamArray[i].PresidentId == id)
                {
                    return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError("The president has a team so, you cannot delete!")
                    )
                    );
                }
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}