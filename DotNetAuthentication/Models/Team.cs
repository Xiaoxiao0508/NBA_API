﻿namespace DotNetAuthentication.Models
{
    public class Team
    {        
        public string TeamName { get; set; }
        public int UserId { get; set; }
        
        public Team(string teamName)
        {
            this.TeamName = teamName;
        }

        public Team()
        {
        }
    }
}