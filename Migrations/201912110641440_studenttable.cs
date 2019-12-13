namespace StudentAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StdInformationViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StdName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        Mobile = c.Int(nullable: false),
                        Email = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Food = c.Boolean(nullable: false),
                        Description = c.String(),
                        CourseId = c.Int(nullable: false),
                        Course = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Information", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "Age");
            DropTable("dbo.StdInformationViewModels");
        }
    }
}
