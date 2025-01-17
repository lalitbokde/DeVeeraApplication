﻿using CRM.Core.Domain;
using CRM.Core.ViewModels;
using CRM.Data;
using CRM.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CRM.Services
{
    public class VideoMasterService : IVideoMasterService
    {
        #region fields
        private readonly IRepository<Video> _videoRepository;
        private readonly IS3BucketService _s3BucketService;
        protected readonly dbContextCRM _dbContext;



        #endregion
        public VideoMasterService(IRepository<Video> videoRepository,
                                 IS3BucketService s3BucketService,
                                 dbContextCRM dbContext)
        {
            _videoRepository = videoRepository;
            _s3BucketService = s3BucketService;
            _dbContext = dbContext;
        }

        #region ctor

        #endregion

        #region Method
        public void DeleteVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _videoRepository.Delete(model);
        }

        public IList<Video> GetAllVideos()
        {
            var query = from vdo in _videoRepository.Table
                        orderby vdo.VideoUrl
                        select vdo;
            var videos = query.ToList();
            return videos;
        }

        public virtual Video GetVideoById(int videoId)
        {
            if (videoId == 0)
                return null;

            var data = _videoRepository.GetById(videoId);
            if(data != null)
            {
                if (data.UpdatedOn.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    if(data.Key != null)
                    {
                        data.VideoUrl = _s3BucketService.GetPreSignedURL(data.Key);
                        data.UpdatedOn = DateTime.Now;
                        UpdateVideo(data);

                    }

                }
            }
            return data;
        }

        public IList<Video> GetVideoByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<Video>();

            var query = from c in _videoRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<Video>();
            foreach (var id in VideoIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _videoRepository.Insert(model);
        }

        public void UpdateVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _videoRepository.Update(model);
        }


        public List<VideoViewModel> GetAllVideoSp(
      int page_size = 0,
      int page_num = 0,
      bool GetAll = false,
      string SortBy = ""
    )
        {

            try
            {

                string query = @"exec [sp_GetAllVideos] @page_size = '" + ((page_size == 0) ? 12 : page_size) + "', " +
                                "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                "@sortBy ='" + SortBy + "' , " +
                                "@GetAll ='" + GetAll + "'";


                var data = _dbContext.VideoViewModel.FromSql(query).ToList();
                return (data.FirstOrDefault() != null) ? data : new List<VideoViewModel>();
                
            }
            catch(Exception e)
            {
                return new List<VideoViewModel>();
            }

        }


        #endregion

    }
}
