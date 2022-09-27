using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.Canvas.WebApi.Extensions
{
    public static class PackageEntityExtensions
    {
        public static string GetPackageBonusTitle(this PackageBonusAssociationEntity packageBonusAssociation)
        {
            if (packageBonusAssociation.FromLevel != 1)
                return $"{packageBonusAssociation.Bonus.Name} с {packageBonusAssociation.FromLevel} уровня";

            if (packageBonusAssociation.PayablePercent != null)
                return $"{packageBonusAssociation.Bonus.Name} {packageBonusAssociation.PayablePercent}%";

            return packageBonusAssociation.Bonus.Name;

        }
    }
}
