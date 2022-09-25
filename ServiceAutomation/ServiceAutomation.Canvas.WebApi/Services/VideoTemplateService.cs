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
    public class VideoTemplateService : IVideoTemplateService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public VideoTemplateService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<VideoLessonResponseModel> GetVideo(Guid id)
        {
            var videoLesson = await dbContext.VideoLessons.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<VideoLessonResponseModel>(videoLesson);
        }

        public async Task<IEnumerable<VideoLessonResponseModel>> GetVideos()
        {
            var videoLessons = await dbContext.VideoLessons.ToListAsync();
            return videoLessons.Select(x => mapper.Map<VideoLessonResponseModel>(x));
        }
    }
}
