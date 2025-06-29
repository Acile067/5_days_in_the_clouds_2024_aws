using _5_days_in_the_clouds_2024.Domain.Contracts;
using _5_days_in_the_clouds_2024.Domain.Entities;
using _5_days_in_the_clouds_2024.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Infrastructure.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;
        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Player> CreateAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(string id)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Id.ToLower() == id.ToLower());
        }

        public async Task<bool> PlayerExisist(string nickname)
        {
            return await _context.Players.AnyAsync(x => x.Nickname == nickname);
        }

        public async Task<Player> UpdateAsync(Player player)
        {
            var playerUpdated = await _context.Players.FirstOrDefaultAsync(x => x.Id == player.Id);

            if (player == null)
            {
                throw new ArgumentException("Player not found.");
            }

            playerUpdated.Nickname = player.Nickname;
            playerUpdated.Wins = player.Wins;
            playerUpdated.Losses = player.Losses;
            playerUpdated.Elo = player.Elo;
            playerUpdated.HoursPlayed = player.HoursPlayed;
            playerUpdated.Team = player.Team;
            playerUpdated.RatingAdjustment = player.RatingAdjustment;

            _context.Players.Update(playerUpdated);
            await _context.SaveChangesAsync();

            return playerUpdated;
        }
        public async Task<bool> ArePlayersInAnotherTeam(List<string> playerIds)
        {
            return !await _context.Players
                .AnyAsync(p => playerIds.Contains(p.Id) && p.Team != null);
        }

        public async Task<bool> ArePlayersValid(List<string> playerIds)
        {
            var check = await _context.Players
                .Where(p => playerIds.Contains(p.Id))
                .ToListAsync();

            return check.Count == playerIds.Count;
        }
    }
}
