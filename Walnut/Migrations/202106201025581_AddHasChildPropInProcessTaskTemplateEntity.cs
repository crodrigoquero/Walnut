namespace Walnut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasChildPropInProcessTaskTemplateEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTaskTemplates", "HasChild", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessTaskTemplates", "HasChild");
        }
    }
}
