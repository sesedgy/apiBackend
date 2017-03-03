namespace PoiskIT.Okenit2.General.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Lastname = c.String(),
                        Age = c.Int(nullable: false),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.BlogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blogs");
        }
    }
}
