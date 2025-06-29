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
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Team> CreateAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> GetByIdAsync(string id)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(x => x.Id.ToLower() == id.ToLower());
        }

        public async Task<List<Player>> GetPlayersByGuidsAsync(List<string> playerIds)
        {
            return await _context.Players
                .Where(p => playerIds.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<bool> TeamExisist(string teamName)
        {
            return await _context.Teams.AnyAsync(x => x.TeamName == teamName);
        }

        public async Task<Team> UpdateAsync(Team team)
        {
            var existingTeam = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id.ToLower() == team.Id.ToLower());

            if (existingTeam == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            existingTeam.TeamName = team.TeamName;

            var existingPlayerIds = existingTeam.Players.Select(p => p.Id).ToList();
            var newPlayerIds = team.Players.Select(p => p.Id).ToList();

            var playersToRemove = existingTeam.Players
                .Where(p => !newPlayerIds.Contains(p.Id))
                .ToList();
            foreach (var player in playersToRemove)
            {
                existingTeam.Players.Remove(player);
            }

            var playersToAdd = team.Players
                .Where(p => !existingPlayerIds.Contains(p.Id))
                .ToList();
            foreach (var player in playersToAdd)
            {
                existingTeam.Players.Add(player);
            }

            _context.Teams.Update(existingTeam);
            await _context.SaveChangesAsync();

            return existingTeam;
        }
    }
}
