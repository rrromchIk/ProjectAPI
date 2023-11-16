using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAPI.Repository {
    public class TeamRepository : ITeamRepository {
        private readonly DataContext _dataContext;

        public TeamRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public ICollection<Team> GetAllTeams() {
            return _dataContext.Teams
                .Include(t => t.Employees)
                .Include(t => t.Projects)
                .ToList();
        }

        public Team GetTeamById(int id) {
            return _dataContext.Teams
                .Include(t => t.Employees)
                .Include(t => t.Projects)
                .FirstOrDefault(t => t.Id == id);
        }

        public bool TeamExists(int id) {
            return _dataContext.Teams.Any(t => t.Id == id);
        }

        public bool CreateTeam(Team team) {
            _dataContext.Add(team);
            return Save();
        }

        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateTeam(Team team) {
            _dataContext.Update(team);
            return Save();
        }

        public bool DeleteTeam(Team team) {
            _dataContext.Remove(team);
            return Save();
        }
    }
}