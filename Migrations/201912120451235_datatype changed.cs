namespace StudentAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Information", "Gender", c => c.String());
            AlterColumn("dbo.StdInformationViewModels", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StdInformationViewModels", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Information", "Gender", c => c.Boolean(nullable: false));
        }
    }
}
