﻿using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Implementations
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly Cine_1W3_TPContext _context;

        public ShowTimeRepository(Cine_1W3_TPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync()
        {
            var currentDate = DateTime.Now;
            return await _context.Showtimes
                .Where(s => s.StartDate <= currentDate && s.EndDate >= currentDate)
                .ToListAsync();
        }

       

        public async Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId)
        {
            return await (from showtime in _context.Showtimes
                          join screen in _context.Screens on showtime.ScreenId equals screen.ScreenId
                          where screen.CinemaId == cinemaId
                          select showtime).ToListAsync();
        }

    }
}