namespace LetsDish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPicsToEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Picture");
        }
    }
}
