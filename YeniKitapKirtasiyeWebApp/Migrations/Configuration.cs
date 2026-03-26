namespace YeniKitapKirtasiyeWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YeniKitapKirtasiyeWebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<YeniKitapKirtasiyeWebApp.Models.YeniKitapKirtasiyeDBModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(YeniKitapKirtasiyeWebApp.Models.YeniKitapKirtasiyeDBModel context)
        {
            //context.Users.AddOrUpdate(new User() { ID = 1, Name = "User", Surname = "User", Mail = "user@user.com", Password = "1234", IsActive = true, CreationTime = DateTime.Now });
        }
    }
}
