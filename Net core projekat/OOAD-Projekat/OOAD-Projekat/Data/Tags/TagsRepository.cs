﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Tags
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public TagsRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddTags(Tag t)
        {
           applicationDbContext.Tags.AddAsync(t);
           await applicationDbContext.SaveChangesAsync(); 
        }

        [HttpGet]
        public async Task<List<Tag>> GetTags(string searchParam)
        {
            // Defaultni search, kad se tek otvori stranica
            if(searchParam == "")
            {
                return await applicationDbContext.Tags.ToListAsync();
            }
            else return await applicationDbContext.Tags.Where(t => t.TagContent.ToUpper().Contains(searchParam)).ToListAsync();
        }
        public async Task<Tag> GetTagByName(string name)
        {
            return applicationDbContext.Tags.FirstOrDefault(p => p.TagContent == name);
        }
        public async Task<List<Tag>> GetPopular()
        {
            return applicationDbContext.Tags.OrderByDescending(x => x.NumOfUses).Take(10).ToList();
        }

    }
}
