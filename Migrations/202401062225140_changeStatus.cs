namespace todoproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Status", c => c.String());
        }
    }
}
