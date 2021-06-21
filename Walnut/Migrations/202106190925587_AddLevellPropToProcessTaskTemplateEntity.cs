namespace Walnut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevellPropToProcessTaskTemplateEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTaskTemplates", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessTaskTemplates", "Level");
        }
    }
}
