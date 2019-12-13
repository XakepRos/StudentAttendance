namespace StudentAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Information", "Mobile", c => c.Long(nullable: false));
            AlterColumn("dbo.StdInformationViewModels", "Mobile", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StdInformationViewModels", "Mobile", c => c.Int(nullable: false));
            AlterColumn("dbo.Information", "Mobile", c => c.Int(nullable: false));
        }
    }
}
