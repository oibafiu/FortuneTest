namespace FortuneTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoForum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fora", "InsertDate", c => c.DateTime());
            AlterColumn("dbo.Fora", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fora", "UpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Fora", "InsertDate", c => c.DateTime(nullable: false));
        }
    }
}
