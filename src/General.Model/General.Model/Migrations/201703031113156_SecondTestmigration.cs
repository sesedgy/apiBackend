namespace PoiskIT.Okenit2.General.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondTestmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Phone");
        }
    }
}
