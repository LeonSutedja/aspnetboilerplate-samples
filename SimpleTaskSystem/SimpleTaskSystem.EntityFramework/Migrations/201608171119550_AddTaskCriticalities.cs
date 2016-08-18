namespace SimpleTaskSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCriticalities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StsTaskCriticalities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.StsTasks", "TaskCriticalityId", c => c.Int());
            CreateIndex("dbo.StsTasks", "TaskCriticalityId");
            AddForeignKey("dbo.StsTasks", "TaskCriticalityId", "dbo.StsTaskCriticalities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StsTasks", "TaskCriticalityId", "dbo.StsTaskCriticalities");
            DropIndex("dbo.StsTasks", new[] { "TaskCriticalityId" });
            DropColumn("dbo.StsTasks", "TaskCriticalityId");
            DropTable("dbo.StsTaskCriticalities");
        }
    }
}
