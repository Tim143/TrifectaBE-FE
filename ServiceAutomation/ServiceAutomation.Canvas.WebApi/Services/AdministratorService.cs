using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.AdministratorResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        public AdministratorService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task AcceptContactVerificationRequest(Guid requestId, Guid userId)
        {
            var verificationRequest = await dbContext.UserContactVerifications.FirstOrDefaultAsync(x => x.Id == requestId);
            var currentUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            switch (verificationRequest.VerificationType)
            {
                case ContactVerificationType.EmailAdress:
                    currentUser.Email = verificationRequest.NewData;
                    verificationRequest.IsVerified = true;
                    break;
                case ContactVerificationType.PhoneNumber:
                    currentUser.PhoneNumber = verificationRequest.NewData;
                    verificationRequest.IsVerified = true;
                    break;
                default:
                    break;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task AcceptVerificationRequest(Guid requestId, Guid userId)
        {
            var userOrganizationType = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == userId);

            switch (userOrganizationType.TypeOfEmployment)
            {
                case TypeOfEmployment.LegalEntity:
                    var legalEntityRequest = await dbContext.LegalUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    legalEntityRequest.IsVerivied = true;
                    break;
                case TypeOfEmployment.IndividualEntity:
                    var individualEntityRequest = await dbContext.IndividualUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    individualEntityRequest.IsVerivied = true;
                    break;
                case TypeOfEmployment.IndividualEntrepreneur:
                    var individualEntrepreneurEntityRequest = await dbContext.IndividualEntrepreneurUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    individualEntrepreneurEntityRequest.IsVerivied = true;
                    break;     
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<UserContactsVerificationResponseModel>> GetContactVerificationRequest()
        {
            var contactVerificationCollection = await dbContext.UserContactVerifications
                .Include(x => x.User)
                .Where(x => x.IsVerified == false)
                .ToListAsync();

            return contactVerificationCollection.Select(x => mapper.Map<UserContactsVerificationResponseModel>(x)).ToList();
        }

        public async Task<ICollection<UserVerificationResponseModel>> GetVerificationRequest()
        {
            var legalUsersRequests = await dbContext.LegalUserOrganizationsData.Where(x => x.IsVerivied == false).ToListAsync();
            var result1 = legalUsersRequests.Select(x => mapper.Map<UserVerificationResponseModel>(x)).ToArray();

            var individualUsersRequests = await dbContext.IndividualUserOrganizationsData.Where(x => x.IsVerivied == false).ToListAsync();
            var result2 = individualUsersRequests.Select(x => mapper.Map<UserVerificationResponseModel>(x)).ToArray();

            var individualEntrepreneursRequests = await dbContext.IndividualEntrepreneurUserOrganizationsData.Where(x => x.IsVerivied == false).ToListAsync();
            var result3 = individualEntrepreneursRequests.Select(x => mapper.Map<UserVerificationResponseModel>(x)).ToArray();

            for (int i=0; i < result1.Length; i++)
            {
                var itemExtraData = await dbContext.UserContacts
                    .Include(x => x.User)
                    .ThenInclude(x => x.UserAccountOrganization)
                    .FirstOrDefaultAsync(x => x.UserId == result1[i].UserId);

                if (itemExtraData != null)
                {
                    result1[i].Name = itemExtraData.FirstName + " " + itemExtraData.LastName;
                    result1[i].Email = itemExtraData?.User?.Email;
                    result1[i].PhoneNumber = itemExtraData?.User?.PhoneNumber;
                    result1[i].TypeOfEmployment = itemExtraData?.User?.UserAccountOrganization.TypeOfEmployment.ToString();
                }
            }

            for (int i = 0; i < result2.Length; i++)
            {
                var itemExtraData = await dbContext.UserContacts
                    .Include(x => x.User)
                    .ThenInclude(x => x.UserAccountOrganization)
                    .FirstOrDefaultAsync(x => x.UserId == result2[i].UserId);

                if (itemExtraData != null)
                {
                    result2[i].Name = itemExtraData.FirstName + " " + itemExtraData.LastName;
                    result2[i].Email = itemExtraData?.User?.Email;
                    result2[i].PhoneNumber = itemExtraData?.User?.PhoneNumber;
                    result2[i].TypeOfEmployment = itemExtraData?.User?.UserAccountOrganization.TypeOfEmployment.ToString();
                }
            }

            for (int i = 0; i < result3.Length; i++)
            {
                var itemExtraData = await dbContext.UserContacts
                    .Include(x => x.User)
                    .ThenInclude(x => x.UserAccountOrganization)
                    .FirstOrDefaultAsync(x => x.UserId == result3[i].UserId);

                if (itemExtraData != null)
                {
                    result3[i].Name = itemExtraData.FirstName + " " + itemExtraData.LastName;
                    result3[i].Email = itemExtraData?.User?.Email;
                    result3[i].PhoneNumber = itemExtraData?.User?.PhoneNumber;
                    result3[i].TypeOfEmployment = itemExtraData?.User?.UserAccountOrganization.TypeOfEmployment.ToString();
                }
            }

            var response = new List<UserVerificationResponseModel>();
            response.AddRange(result1);
            response.AddRange(result2);
            response.AddRange(result3);

            return response;
        }

        public async Task RejectContactVerificationRequest(Guid requestId, Guid userId)
        {
            var contactVerificationRequest = await dbContext.UserContactVerifications.FirstOrDefaultAsync(x => x.Id == requestId);
            
            dbContext.UserContactVerifications.Remove(contactVerificationRequest);
            await dbContext.SaveChangesAsync();
        }

        public async Task RejectVerificationRequest(Guid requestId, Guid userId)
        {
            var userOrganizationType = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == userId);

            switch (userOrganizationType.TypeOfEmployment)
            {
                case TypeOfEmployment.LegalEntity:
                    var legalEntityRequest = await dbContext.LegalUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    dbContext.LegalUserOrganizationsData.Remove(legalEntityRequest);
                    break;
                case TypeOfEmployment.IndividualEntity:
                    var individualEntityRequest = await dbContext.IndividualUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    dbContext.IndividualUserOrganizationsData.Remove(individualEntityRequest);
                    break;
                case TypeOfEmployment.IndividualEntrepreneur:
                    var individualEntrepreneurEntityRequest = await dbContext.IndividualEntrepreneurUserOrganizationsData.FirstOrDefaultAsync(x => x.Id == requestId && x.IsVerivied == false);
                    dbContext.IndividualEntrepreneurUserOrganizationsData.Remove(individualEntrepreneurEntityRequest);
                    break;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
