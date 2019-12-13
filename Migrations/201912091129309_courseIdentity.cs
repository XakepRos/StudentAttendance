namespace StudentAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseIdentity : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CourseViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
