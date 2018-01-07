namespace LetsDish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviseIngredientTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredients", "IngredientDescription", c => c.String());
            DropColumn("dbo.Ingredients", "IngredientName");
            DropColumn("dbo.Ingredients", "Quantity");
            DropColumn("dbo.Ingredients", "UnitOfMeasure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "UnitOfMeasure", c => c.String());
            AddColumn("dbo.Ingredients", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Ingredients", "IngredientName", c => c.String());
            DropColumn("dbo.Ingredients", "IngredientDescription");
        }
    }
}
