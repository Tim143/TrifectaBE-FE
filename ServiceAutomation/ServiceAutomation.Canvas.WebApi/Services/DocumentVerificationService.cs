using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.Canvas.WebApi.Models.SubRequestModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static ServiceAutomation.Canvas.WebApi.Constants.Requests;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class DocumentVerificationService : IDocumentVerificationService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        private const string VerificationBasePath = "/DocumentVerification/Verifications/";
        private const string IndividualEmpBasePath = "/DocumentVerification/IndividualEntrepreneur/";
        public DocumentVerificationService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResultModel> SendUserVerificationData(DocumentVerificationRequestModel requestModel)
        {
            var userOrganizationType = requestModel.EmploymentType;

            switch (userOrganizationType)
            {
                case TypeOfEmployment.LegalEntity:

                    var legalEntityModel = new LegalEntityModel()
                    {
                        UserId = requestModel.UserId,
                        Region = requestModel.DocumentVerificationModels.BankRequestModel.BankRegion,
                        Locality = requestModel.DocumentVerificationModels.BankRequestModel.BankLocality,
                        BankStreet = requestModel.DocumentVerificationModels.BankRequestModel.BankStreet,
                        BankHouseNumber = requestModel.DocumentVerificationModels.BankRequestModel.BankHouseNumber,
                        BeneficiaryBankName = requestModel.DocumentVerificationModels.BankRequestModel.BeneficiaryBankName,
                        CheckingAccount = requestModel.DocumentVerificationModels.BankRequestModel.CheckingAccount,
                        SWIFT = requestModel.DocumentVerificationModels.BankRequestModel.SWIFT,
                        Disctrict = requestModel.DocumentVerificationModels.PersonalAddressModel.District,
                        City = requestModel.DocumentVerificationModels.PersonalAddressModel.City,
                        Index = requestModel.DocumentVerificationModels.PersonalAddressModel.Index,
                        Street = requestModel.DocumentVerificationModels.PersonalAddressModel.Street,
                        HouseNumber = requestModel.DocumentVerificationModels.PersonalAddressModel.HouseNumber,
                        Flat = requestModel.DocumentVerificationModels.PersonalAddressModel.Flat
                    };

                    return await SendLegalEntityData(legalEntityModel);
                case TypeOfEmployment.IndividualEntity:

                    var individualEntityModel = new IndividualEntityModel()
                    {
                        UserId = requestModel.UserId,
                        LegalEntityFullName = requestModel.DocumentVerificationModels.LegalDataModel.LegalEntityFullName,
                        HeadFullName = requestModel.DocumentVerificationModels.LegalDataModel.HeadFullName,
                        LegalEntityAbbreviatedName = requestModel.DocumentVerificationModels.LegalDataModel.LegalEntityAbbreviatedName,
                        HeadPosition = requestModel.DocumentVerificationModels.LegalDataModel.HeadPosition,
                        UNP = requestModel.DocumentVerificationModels.LegalDataModel.UNP,
                        BaseOrganization = requestModel.DocumentVerificationModels.LegalDataModel.BaseOrganization,
                        AccountantName = requestModel.DocumentVerificationModels.LegalDataModel.AccountantName,
                        CertificateNumber = requestModel.DocumentVerificationModels.WitnessDataModel.CertificateNumber,
                        RegistrationAuthority = requestModel.DocumentVerificationModels.WitnessDataModel.RegistrationAuthority,
                        CertificateDateIssue = requestModel.DocumentVerificationModels.WitnessDataModel.CertificateDateIssue,
                        BankRegion = requestModel.DocumentVerificationModels.BankRequestModel.BankRegion,
                        BankLocality = requestModel.DocumentVerificationModels.BankRequestModel.BankLocality,
                        BankStreet = requestModel.DocumentVerificationModels.BankRequestModel.BankStreet,
                        BankHouseNumber = requestModel.DocumentVerificationModels.BankRequestModel.BankHouseNumber,
                        BeneficiaryBankName = requestModel.DocumentVerificationModels.BankRequestModel.BeneficiaryBankName,
                        CheckingAccount = requestModel.DocumentVerificationModels.BankRequestModel.CheckingAccount,
                        SWIFT = requestModel.DocumentVerificationModels.BankRequestModel.SWIFT,
                        Region = requestModel.DocumentVerificationModels.LegallyAddressModel.Region,
                        Locality = requestModel.DocumentVerificationModels.LegallyAddressModel.Locality,
                        Index = requestModel.DocumentVerificationModels.LegallyAddressModel.Index,
                        Street = requestModel.DocumentVerificationModels.LegallyAddressModel.Street,
                        HouseNumber = requestModel.DocumentVerificationModels.LegallyAddressModel.HouseNumber,
                        Location = requestModel.DocumentVerificationModels.LegallyAddressModel.Location,
                        RoomNumber = requestModel.DocumentVerificationModels.LegallyAddressModel.RoomNumber,
                        //VerificationData = requestModel.DocumentVerificationModels.WitnessDataModel.FileData
                    };

                    return await SendIndividualEntityData(individualEntityModel);
                case TypeOfEmployment.IndividualEntrepreneur:

                    var individualEntrepreneurEntityModel = new IndividualEntrepreneurEntityModel()
                    {
                        UserId = requestModel.UserId,
                        LegalEntityFullName = requestModel.DocumentVerificationModels.LegalDataModel.LegalEntityFullName,
                        HeadFullName = requestModel.DocumentVerificationModels.LegalDataModel.HeadFullName,
                        LegalEntityAbbreviatedName = requestModel.DocumentVerificationModels.LegalDataModel.LegalEntityAbbreviatedName,
                        HeadPosition = requestModel.DocumentVerificationModels.LegalDataModel.HeadPosition,
                        UNP = requestModel.DocumentVerificationModels.LegalDataModel.UNP,
                        BaseOrganization = requestModel.DocumentVerificationModels.LegalDataModel.BaseOrganization,
                        AccountantName = requestModel.DocumentVerificationModels.LegalDataModel.AccountantName,
                        CertificateNumber = requestModel.DocumentVerificationModels.WitnessDataModel.CertificateNumber,
                        RegistrationAuthority = requestModel.DocumentVerificationModels.WitnessDataModel.RegistrationAuthority,
                        CertificateDateIssue = requestModel.DocumentVerificationModels.WitnessDataModel.CertificateDateIssue,
                        BankRegion = requestModel.DocumentVerificationModels.BankRequestModel.BankRegion,
                        BankLocality = requestModel.DocumentVerificationModels.BankRequestModel.BankLocality,
                        BankStreet = requestModel.DocumentVerificationModels.BankRequestModel.BankStreet,
                        BankHouseNumber = requestModel.DocumentVerificationModels.BankRequestModel.BankHouseNumber,
                        BeneficiaryBankName = requestModel.DocumentVerificationModels.BankRequestModel.BeneficiaryBankName,
                        CheckingAccount = requestModel.DocumentVerificationModels.BankRequestModel.CheckingAccount,
                        SWIFT = requestModel.DocumentVerificationModels.BankRequestModel.SWIFT,
                        Region = requestModel.DocumentVerificationModels.LegallyAddressModel.Region,
                        Locality = requestModel.DocumentVerificationModels.LegallyAddressModel.Locality,
                        Index = requestModel.DocumentVerificationModels.LegallyAddressModel.Index,
                        Street = requestModel.DocumentVerificationModels.LegallyAddressModel.Street,
                        HouseNumber = requestModel.DocumentVerificationModels.LegallyAddressModel.HouseNumber,
                        Location = requestModel.DocumentVerificationModels.LegallyAddressModel.Location,
                        RoomNumber = requestModel.DocumentVerificationModels.LegallyAddressModel.RoomNumber,
                        //VerificationData = requestModel.DocumentVerificationModels.WitnessDataModel.FileData
                    };

                    return await SendIndividualEntrepreneurEntityData(individualEntrepreneurEntityModel);
                default:
                    return new ResultModel()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Invalid argument"
                        }
                    };
            }
        }

        private async Task<ResultModel> SendLegalEntityData(LegalEntityModel dataModel)
        {
            var userorganization = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == dataModel.UserId && x.TypeOfEmployment == dataModel.TypeOfEmployment);

            if (userorganization == null)
            {
                userorganization = new UserAccountOrganizationEntity()
                {
                    UserId = dataModel.UserId,
                    TypeOfEmployment = dataModel.TypeOfEmployment
                };

                var legalUserModel = new LegalUserOrganizationDataEntity()
                {
                    UserId = dataModel.UserId,
                    Region = dataModel.Region,
                    Locality = dataModel.Locality,
                    BankStreet = dataModel.BankStreet,
                    BankHouseNumber = dataModel.BankHouseNumber,
                    BeneficiaryBankName = dataModel.BeneficiaryBankName,
                    CheckingAccount = dataModel.CheckingAccount,
                    SWIFT = dataModel.SWIFT,
                    Disctrict = dataModel.Disctrict,
                    City = dataModel.City,
                    Index = dataModel.Index,
                    Street = dataModel.Street,
                    HouseNumber = dataModel.HouseNumber,
                    Flat = dataModel.Flat
                };

                try
                {
                    await dbContext.UserAccountOrganizations.AddAsync(userorganization);
                    await dbContext.LegalUserOrganizationsData.AddAsync(legalUserModel);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResultModel()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            ex.Message
                        }
                    };
                }

                return new ResultModel()
                {
                    Success = true
                };
            }

            return new ResultModel()
            {
                Success = false
            };
        }

        private async Task<ResultModel> SendIndividualEntityData(IndividualEntityModel dataModel)
        {
            var userorganization = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == dataModel.UserId && x.TypeOfEmployment == dataModel.TypeOfEmployment);

            if (userorganization == null)
            {
                userorganization = new UserAccountOrganizationEntity()
                {
                    UserId = dataModel.UserId,
                    TypeOfEmployment = dataModel.TypeOfEmployment
                };

                //var verificationPhotoName = dataModel.UserId + "Verification" + ".png";
                //var verificationPhotoFullPath = IndividualBasePath + verificationPhotoName;

                //using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + verificationPhotoFullPath, FileMode.Create))
                //{
                //    await dataModel.VerificationData.CopyToAsync(fileStream);
                //}

                var individualUserModel = new IndividualUserOrganizationDataEntity()
                {
                    UserId = dataModel.UserId,
                    LegalEntityFullName = dataModel.LegalEntityFullName,
                    HeadFullName = dataModel.HeadFullName,
                    LegalEntityAbbreviatedName = dataModel.LegalEntityAbbreviatedName,
                    HeadPosition = dataModel.HeadPosition,
                    UNP = dataModel.UNP,
                    BaseOrganization = dataModel.BaseOrganization,
                    AccountantName = dataModel.AccountantName,
                    CertificateNumber = dataModel.CertificateNumber,
                    RegistrationAuthority = dataModel.RegistrationAuthority,
                    CertificateDateIssue = dataModel.CertificateDateIssue,
                    BankRegion = dataModel.BankRegion,
                    BankLocality = dataModel.BankLocality,
                    BankStreet = dataModel.BankStreet,
                    BankHouseNumber = dataModel.BankHouseNumber,
                    BeneficiaryBankName = dataModel.BeneficiaryBankName,
                    CheckingAccount = dataModel.CheckingAccount,
                    SWIFT = dataModel.SWIFT,
                    Region = dataModel.Region,
                    Locality = dataModel.Locality,
                    Index = dataModel.Index,
                    Street = dataModel.Street,
                    HouseNumber = dataModel.HouseNumber,
                    Location = dataModel.Location,
                    RoomNumber = dataModel.RoomNumber,
                    //VerificationPhotoPath = verificationPhotoFullPath,
                };

                try
                {
                    await dbContext.UserAccountOrganizations.AddAsync(userorganization);
                    await dbContext.IndividualUserOrganizationsData.AddAsync(individualUserModel);
                    await dbContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return new ResultModel()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            ex.Message
                        }
                    };
                }

                return new ResultModel()
                {
                    Success = true
                };
            }

            return new ResultModel()
            {
                Success = false
            };
        }

        private async Task<ResultModel> SendIndividualEntrepreneurEntityData(IndividualEntrepreneurEntityModel dataModel)
        {
            var userorganization = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == dataModel.UserId && x.TypeOfEmployment == dataModel.TypeOfEmployment);

            if (userorganization == null)
            {
                userorganization = new UserAccountOrganizationEntity()
                {
                    UserId = dataModel.UserId,
                    TypeOfEmployment = dataModel.TypeOfEmployment
                };

                var individualUserModel = new IndividualEntrepreneurUserOrganizationDataEntity()
                {
                    UserId = dataModel.UserId,
                    LegalEntityFullName = dataModel.LegalEntityFullName,
                    HeadFullName = dataModel.HeadFullName,
                    LegalEntityAbbreviatedName = dataModel.LegalEntityAbbreviatedName,
                    HeadPosition = dataModel.HeadPosition,
                    UNP = dataModel.UNP,
                    BaseOrganization = dataModel.BaseOrganization,
                    AccountantName = dataModel.AccountantName,
                    CertificateNumber = dataModel.CertificateNumber,
                    RegistrationAuthority = dataModel.RegistrationAuthority,
                    CertificateDateIssue = dataModel.CertificateDateIssue,
                    BankRegion = dataModel.BankRegion,
                    BankLocality = dataModel.BankLocality,
                    BankStreet = dataModel.BankStreet,
                    BankHouseNumber = dataModel.BankHouseNumber,
                    BeneficiaryBankName = dataModel.BeneficiaryBankName,
                    CheckingAccount = dataModel.CheckingAccount,
                    SWIFT = dataModel.SWIFT,
                    Region = dataModel.Region,
                    Locality = dataModel.Locality,
                    Index = dataModel.Index,
                    Street = dataModel.Street,
                    HouseNumber = dataModel.HouseNumber,
                    Location = dataModel.Location,
                    RoomNumber = dataModel.RoomNumber,
                    //VerificationPhotoPath = verificationPhotoFullPath,
                };

                try
                {
                    await dbContext.UserAccountOrganizations.AddAsync(userorganization);
                    await dbContext.IndividualEntrepreneurUserOrganizationsData.AddAsync(individualUserModel);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResultModel()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            ex.Message
                        }
                    };
                }

                return new ResultModel()
                {
                    Success = true
                };
            }

            return new ResultModel()
            {
                Success = true
            };
        }

        public async Task<OneOf<IndividualEntityDataResponseModel, IndividualEntrepreneurEntityDataResponseModel, LegalEntityDataResponseModel, ResultModel>> GetUserVerifiedData(Guid userId)
        {
            var userOrganizationData = await dbContext.UserAccountOrganizations.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userOrganizationData == null)
            {
                return new ResultModel()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "There is no data for this user."
                    }
                };
            }

            var userOrganizationType = userOrganizationData.TypeOfEmployment;

            switch (userOrganizationType)
            {
                case TypeOfEmployment.LegalEntity:
                    var legalEntity = await dbContext.LegalUserOrganizationsData.FirstOrDefaultAsync(x => x.UserId == userId);
                    return mapper.Map<LegalEntityDataResponseModel>(legalEntity);
                case TypeOfEmployment.IndividualEntity:
                    var individualEntity = await dbContext.IndividualUserOrganizationsData.FirstOrDefaultAsync(x => x.UserId == userId);
                    return mapper.Map<IndividualEntityDataResponseModel>(individualEntity);
                case TypeOfEmployment.IndividualEntrepreneur:
                    var individualEntrepreneurEntity = await dbContext.IndividualEntrepreneurUserOrganizationsData.FirstOrDefaultAsync(x => x.UserId == userId);
                    return mapper.Map<IndividualEntrepreneurEntityDataResponseModel>(individualEntrepreneurEntity);
                default:
                    throw new ArgumentException("Invalid argument");
            }
        }

        public async Task<ResultModel> SendUserVerificationPhoto(IFormFile file, Guid userId)
        {
            var verificationPhotoName = userId + "Verification" + ".png";
            var verificationPhotoFullPath = VerificationBasePath + verificationPhotoName;

            try
            {
                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + verificationPhotoFullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var verificationPhoto = new UserVerificationPhotoEntity()
                {
                    UserId = userId,
                    FileName = verificationPhotoName,
                    FullPath = verificationPhotoFullPath
                };

                await dbContext.UserVerificationPhotos.AddAsync(verificationPhoto);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return new ResultModel()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        ex.Message
                    }
                };
            }


            return new ResultModel()
            {
                Success = true,
            };
        }
    }
}
