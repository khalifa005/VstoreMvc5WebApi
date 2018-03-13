namespace Vstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ImagePath", c => c.String());
        }
    }
}
