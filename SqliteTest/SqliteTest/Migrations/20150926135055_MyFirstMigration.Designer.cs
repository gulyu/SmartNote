using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using SqliteTest.DAL;

namespace SqliteTestMigrations
{
    [ContextType(typeof(NoteContext))]
    partial class MyFirstMigration
    {
        public override string Id
        {
            get { return "20150926135055_MyFirstMigration"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("SqliteTest.Model.Note", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created");

                    b.Property<string>("text");

                    b.Key("id");
                });
        }
    }
}
