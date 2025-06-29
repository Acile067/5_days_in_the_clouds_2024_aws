using _5_days_in_the_clouds_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Domain.Contracts
{
    public interface ITeamRepository
    {
        Task<Team> CreateAsync(Team team);
        Task<Team?> GetByIdAsync(string id);
        Task<List<Player>> GetPlayersByGuidsAsync(List<string> playerIds);
        Task<Team> UpdateAsync(Team team);
        Task<bool> TeamExisist(string teamName);
    }
}
