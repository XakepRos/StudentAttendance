namespace StudentAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StdId = c.Int(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        OutTime = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Information = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AttendanceViewModels");
        }
    }
}
