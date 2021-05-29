﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public QuestionsRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<List<Question>> Find(String SearchParam)
        {
            var data = await applicationDbContext.Questions.Where(q => q.Title.ToUpper().Contains(SearchParam)).ToListAsync();
            return data;
        }
        public async Task<List<Question>> FindAll()
        {
            return await applicationDbContext.Questions.ToListAsync();
        }
        [Authorize]
        public async Task<List<Question>> FindMine(String UserName)
        {
            var data = await applicationDbContext.Questions.Where(q => q.User.Email == UserName).ToListAsync();
            return data;
        }
        public async Task AddQuestion(Question question)
        {
            await applicationDbContext.Questions.AddAsync(question);
            await applicationDbContext.SaveChangesAsync();
        }
        //todo: DeleteQuestion
    }
}