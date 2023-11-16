using ProjectAPI.Models;

namespace ProjectAPI.Interfaces {
    public interface ITeamRepository {
        ICollection<Team> GetAllTeams();
        Team GetTeamById(int id);
        bool TeamExists(int id);
        bool CreateTeam(Team team);
        bool UpdateTeam(Team team);
        bool DeleteTeam(Team team);
        bool Save();
    }
}