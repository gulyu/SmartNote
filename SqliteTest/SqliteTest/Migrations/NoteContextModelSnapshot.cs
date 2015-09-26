using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using SqliteTest.DAL;

namespace SqliteTestMigrations
{
    [ContextType(typeof(NoteContext))]
    partial class NoteContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
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
