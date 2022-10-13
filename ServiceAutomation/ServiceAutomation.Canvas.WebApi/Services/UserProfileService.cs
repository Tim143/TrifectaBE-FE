using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ServiceAutomation.Canvas.WebApi.Constants;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static ServiceAutomation.Canvas.WebApi.Constants.Requests;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        private const string BasePath = "/ProfilePhotos/";

        public UserProfileService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<UserProfileResponseModel> GetUserInfo(Guid userId)
        {
            var user = await dbContext.Users
                .Include(x => x.ProfilePhoto)
                .Include(x => x.UserContact)
                .Include(x => x.BasicLevel)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var package = await dbContext.UsersPurchases
                                .AsNoTracking()
                                .Where(x => x.UserId == userId)
                                .OrderByDescending(x => x.PurchaseDate)
                                .Include(x => x.Package)
                                .ThenInclude(x => x.PackageBonuses)
                                .ThenInclude(x => x.Bonus)
                                .Select(x => x.Package)
                                .FirstOrDefaultAsync();

            var response = mapper.Map<UserProfileResponseModel>(user);

            if(package != null)
            {
                response.PackageName = package.Name;
                response.PackageId = package.Id;
            }

            return response;
        }

        public async Task<ResultModel> UploadProfilePhoto(Guid userId, IFormFile data)
        {
            var response = new ResultModel();
            var photo = await dbContext.ProfilePhotos.FirstOrDefaultAsync(x => x.UserId == userId);

            var profilePhotoName = userId.ToString() + ".png";

            var profilePhotoFullPath = BasePath + profilePhotoName;

            if (photo == null)
            {
                photo = new ProfilePhotoEntity()
                {
                    UserId = userId,
                    Name = profilePhotoName,
                    FullPath = profilePhotoFullPath                    
                };

                try
                {
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + profilePhotoFullPath, FileMode.Create))
                    {
                        await data.CopyToAsync(fileStream);
                    }

                    await dbContext.ProfilePhotos.AddAsync(photo);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.Message);
                    response.Success = false;
                }
            }
            else
            {
                try
                {
                    System.IO.File.Delete(webHostEnvironment.WebRootPath + photo.FullPath);
                    dbContext.ProfilePhotos.Remove(photo);
                    await dbContext.SaveChangesAsync();

                    var newPhoto = new ProfilePhotoEntity()
                    {
                        UserId = userId,
                        Name = profilePhotoName,
                        FullPath = profilePhotoFullPath
                    };


                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + profilePhotoFullPath, FileMode.Create))
                    {
                        await data.CopyToAsync(fileStream);
                    }

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.Message);
                    response.Success = false;
                }
            }

            response.Success = response.Errors != null ? false : true;

            return response;
        }

        public async Task<ResultModel> UploadProfileInfo(Guid userId, string firstName, string lastName, string patronymic, DateTime dateOfBirth)
        {
            var response = new ResultModel();

            var userProfileData = await dbContext.UserContacts.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfileData == null)
            {
                userProfileData = new UserProfileInfoEntity()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Patronymic = patronymic,
                    DateOfBirth = dateOfBirth,
                    UserId = userId
                };

                try
                {
                    await dbContext.UserContacts.AddAsync(userProfileData);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.Message);
                    response.Success = false;
                }
            }
            else
            {
                try
                {
                    userProfileData.FirstName = firstName;
                    userProfileData.LastName = lastName;
                    userProfileData.Patronymic = patronymic;
                    userProfileData.DateOfBirth = dateOfBirth;

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.Message);
                    response.Success = false;
                }
            }

            response.Success = response.Errors != null ? false : true;

            return response;
        }

        public async Task<ResultModel> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var response = new ResultModel();
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                response.Errors.Add("User is null");
                response.Success = false;

                return response;
            }

            var isPasswordCorrect = VerifyPasswordhash(oldPassword, user.PasswordHash, user.PasswordSalt);

            if (isPasswordCorrect)
            {
                var updatedPasswordModel = CreatePasswordHash(newPassword);

                user.PasswordSalt = updatedPasswordModel.PasswordSalt;
                user.PasswordHash = updatedPasswordModel.PasswordHash;

                await dbContext.SaveChangesAsync();

                response.Success = true;
            }
            else
            {
                response.Errors.Add("Password is incorrect");
                response.Success = false;
            }

            return response;
        }

        private bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private PasswordHashModel CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new PasswordHashModel()
                {
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                };
            }
        }

        public async Task<ResultModel> ChangeEmailAdress(Guid userId, string newEmail)
        {
            var result = new ResultModel();

            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var isEmailExists = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == newEmail);

            if (user != null)
            {
                if (isEmailExists == null)
                {
                    var userContactVerificationRequest = new UserContactVerificationEntity()
                    {
                        UserId = userId,
                        User = user,
                        OldData = user.Email,
                        NewData = newEmail,
                        VerificationType = DataAccess.Models.Enums.ContactVerificationType.EmailAdress,
                        IsVerified = false
                    };

                    await dbContext.UserContactVerifications.AddAsync(userContactVerificationRequest);
                    await dbContext.SaveChangesAsync();

                    result.Success = true;
                    return result;
                }
            }

            result.Success = false;
            return result;
        }

        public async Task<ResultModel> UploadPhoneNumber(Guid userId, string newPhoneNumber)
        {
            var result = new ResultModel();
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                ////if (user.PhoneNumber == null)
                ////{
                ////    user.PhoneNumber = newPhoneNumber;
                ////    await dbContext.SaveChangesAsync();

                ////    result.Success = true;
                ////    return result;
                ////}

                ////user.PhoneNumber = newPhoneNumber;
                //await dbContext.SaveChangesAsync();

                var userContactVerificationRequest = new UserContactVerificationEntity()
                {
                    UserId = userId,
                    User = user,
                    OldData = user.PhoneNumber,
                    NewData = newPhoneNumber,
                    VerificationType = DataAccess.Models.Enums.ContactVerificationType.PhoneNumber,
                    IsVerified = false
                };

                await dbContext.UserContactVerifications.AddAsync(userContactVerificationRequest);
                await dbContext.SaveChangesAsync();

                result.Success = true;
                return result;
            }

            result.Success = false;
            return result;
        }
    }
}
