namespace LetsDish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviseRecipeInputTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Yield", c => c.String());
            AlterColumn("dbo.Recipes", "MinutesToMake", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "MinutesToMake", c => c.Int(nullable: false));
            AlterColumn("dbo.Recipes", "Yield", c => c.Int(nullable: false));
        }
    }
}
