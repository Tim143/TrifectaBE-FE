using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class InfoService : IInfoService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public InfoService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ThumbnailResponseModel>> GetThumbnails()
        {
            var thumbnails = await context.Thumbnails.ToListAsync();
            return thumbnails.Select(x => mapper.Map<ThumbnailResponseModel>(x));
        }

        public async Task<ThumbnailResponseModel> GetThumbnail(Guid id)
        {
            var thumbnail = await context.Thumbnails.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ThumbnailResponseModel>(thumbnail);
        }
    }
}
