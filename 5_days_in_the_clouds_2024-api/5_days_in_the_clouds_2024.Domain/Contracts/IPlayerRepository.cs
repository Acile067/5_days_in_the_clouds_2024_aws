using _5_days_in_the_clouds_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Domain.Contracts
{
    public interface IPlayerRepository
    {
        Task<Player> CreateAsync(Player player);
        Task<Player?> GetByIdAsync(string id);
        Task<bool> PlayerExisist(string nickname);
        Task<List<Player>> GetAllAsync();
        Task<Player> UpdateAsync(Player player);
        Task<bool> ArePlayersValid(List<string> playerIds);
        Task<bool> ArePlayersInAnotherTeam(List<string> playerIds);
    }
}
