using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace CachePipelineExample.Core
{
    public class ContextCore : DbContext
    {
        public ContextCore(DbContextOptions<ContextCore> options) : base(options)
        {
            if (_types == null)
            {
                AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
                var ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndCollect);
                var mod = ab.DefineDynamicModule("module");
                _types = new List<Type>();
                foreach (var entity in typeof(ContextCore).GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).Select(x => x.PropertyType.GetGenericArguments().First()).ToList())
                {
                    var tb = mod.DefineType($"History_{entity.Name}");

                    foreach (var mem in entity.GetProperties())
                    {
                        DefineProperty(tb, mem.Name, mem.PropertyType);
                    }
                    DefineProperty(tb, "HistoryId", typeof(Guid));
                    DefineProperty(tb, "HistoryInsertDate", typeof(DateTime));
                    DefineProperty(tb, "HistoryTransactionId", typeof(Guid));
                    DefineProperty(tb, "HistoryChangerId", typeof(Guid));
                    DefineProperty(tb, "HistoryStatus", typeof(string));
                    _types.Add(tb.CreateType());
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        private static List<Type> _types;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Student>().HasMany(x => x.CourseJoinStudents).WithOne(x => x.Student).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>().HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
            modelBuilder.Entity<Professor>().HasKey(x => x.Id);
            modelBuilder.Entity<Professor>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Professor>().HasMany(x => x.CourseJoinProfessor).WithOne(x => x.Professor).HasForeignKey(x => x.ProfessorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>().HasKey(x => x.Id);
            modelBuilder.Entity<Course>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Course>().HasMany(x => x.CourseJoinStudents).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Course>().HasMany(x => x.CourseJoinProfessors).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseJoinProfessor>().HasKey(x => new { x.CourseId, x.ProfessorId });
            modelBuilder.Entity<CourseJoinStudent>().HasKey(x => new { x.CourseId, x.StudentId });

            foreach (var created in _types)
            {
                var ent = modelBuilder.Entity(created);
                ent.HasKey("HistoryId");
                ent.Property("HistoryId").HasDefaultValueSql("NEWID()");
                ent.Property("HistoryInsertDate").HasDefaultValueSql("GETDATE()");
            }
        }

        private void DefineProperty(TypeBuilder tb, string name, Type type)
        {
            FieldBuilder fb = tb.DefineField(
                $"m_{name}",
                type,
                FieldAttributes.Private);
            MethodAttributes getSetAttr = MethodAttributes.Public |
  MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            MethodBuilder mbNumberGetAccessor = tb.DefineMethod(
   $"get_{name}",
   getSetAttr,
   type,
   Type.EmptyTypes);
            ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();
            // For an instance property, argument zero is the instance. Load the
            // instance, then load the private field and return, leaving the
            // field value on the stack.
            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, fb);
            numberGetIL.Emit(OpCodes.Ret);
            MethodBuilder mbNumberSetAccessor = tb.DefineMethod(
                $"set_{name}r",
                getSetAttr,
                null,
                new Type[] { type });
            ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
            // Load the instance and then the numeric argument, then store the
            // argument in the field.
            numberSetIL.Emit(OpCodes.Ldarg_0);
            numberSetIL.Emit(OpCodes.Ldarg_1);
            numberSetIL.Emit(OpCodes.Stfld, fb);
            numberSetIL.Emit(OpCodes.Ret);

            var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            prop.SetGetMethod(mbNumberGetAccessor);
            prop.SetSetMethod(mbNumberSetAccessor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().ToList();
            var transactionId = Guid.NewGuid();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified ||
                    entry.State == EntityState.Added ||
                    entry.State == EntityState.Deleted)
                {
                    var type = _types.First(x=>x.Name == $"History_{entry.Entity.GetType().Name}");
                    var instance = Activator.CreateInstance(type);
                    var values = entry.CurrentValues.ToObject();
                    foreach (var prop in entry.Entity.GetType().GetProperties())
                    {
                        type.GetProperty(prop.Name).GetSetMethod().Invoke(instance, new object[1] { prop.GetGetMethod().Invoke(values, null) });
                    }
                    type.GetProperty("HistoryTransactionId").GetSetMethod().Invoke(instance, new object[1] { transactionId });
                    this.Add(instance);
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        //entities
        public DbSet<Student> Students { get; set; }

        public DbSet<BasicEntityImpl> BasicEntityImpls { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<CourseJoinProfessor> CourseJoinProfessors { get; set; }
        public DbSet<CourseJoinStudent> CourseJoinStudents { get; set; }
    }
}

/*
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Student>().HasMany(x => x.CourseJoinStudents).WithOne(x => x.Student).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>().HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
            modelBuilder.Entity<Professor>().HasKey(x => x.Id);
            modelBuilder.Entity<Professor>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Professor>().HasMany(x => x.CourseJoinProfessor).WithOne(x => x.Professor).HasForeignKey(x => x.ProfessorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>().HasKey(x => x.Id);
            modelBuilder.Entity<Course>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Course>().HasMany(x => x.CourseJoinStudents).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Course>().HasMany(x => x.CourseJoinProfessors).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseJoinProfessor>().HasKey(x => new { x.CourseId, x.ProfessorId });
            modelBuilder.Entity<CourseJoinStudent>().HasKey(x => new { x.CourseId, x.StudentId });

            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndCollect);
            var mod = ab.DefineDynamicModule("module");

            foreach (var entity in typeof(ContextCore).GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).Select(x => x.PropertyType.GetGenericArguments().First()).ToList())
            {
                var tb = mod.DefineType($"History_{entity.Name}");

                foreach (var mem in entity.GetProperties())
                {
                    DefineProperty(tb, mem.Name, mem.PropertyType);

                    // ent.Property(mem.PropertyType, mem.Name);
                }
                DefineProperty(tb, "HistoryId", typeof(Guid));
                DefineProperty(tb, "HistoryInsertDate", typeof(DateTime));

                var created = tb.CreateType();
                var ent = modelBuilder.Entity(created);
                ent.HasKey("HistoryId");
                ent.Property("HistoryId").HasDefaultValueSql("NEWID()");
                ent.Property("HistoryInsertDate").HasDefaultValueSql("GETDATE()");
            }
        }

        private void DefineProperty(TypeBuilder tb, string name, Type type)
{
    FieldBuilder fb = tb.DefineField(
        $"m_{name}",
        type,
        FieldAttributes.Private);
    MethodAttributes getSetAttr = MethodAttributes.Public |
MethodAttributes.SpecialName | MethodAttributes.HideBySig;
    MethodBuilder mbNumberGetAccessor = tb.DefineMethod(
$"get_{name}",
getSetAttr,
type,
Type.EmptyTypes);
    ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();
    // For an instance property, argument zero is the instance. Load the
    // instance, then load the private field and return, leaving the
    // field value on the stack.
    numberGetIL.Emit(OpCodes.Ldarg_0);
    numberGetIL.Emit(OpCodes.Ldfld, fb);
    numberGetIL.Emit(OpCodes.Ret);
    MethodBuilder mbNumberSetAccessor = tb.DefineMethod(
        $"set_{name}r",
        getSetAttr,
        null,
        new Type[] { type });
    ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
    // Load the instance and then the numeric argument, then store the
    // argument in the field.
    numberSetIL.Emit(OpCodes.Ldarg_0);
    numberSetIL.Emit(OpCodes.Ldarg_1);
    numberSetIL.Emit(OpCodes.Stfld, fb);
    numberSetIL.Emit(OpCodes.Ret);

    var prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
    prop.SetGetMethod(mbNumberGetAccessor);
    prop.SetSetMethod(mbNumberSetAccessor);
}

public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var entries = ChangeTracker.Entries().ToList();
    var sb = new StringBuilder();
    foreach (var entry in entries)
    {
        if (entry.State == EntityState.Modified ||
            entry.State == EntityState.Added ||
            entry.State == EntityState.Deleted)
        {
            var command = ($"INSERT INTO History_{entry.Entity.GetType().Name}({string.Join(',', entry.CurrentValues.Properties.Select(x => $"[{x.Name}]"))}) " +
                $"VALUES ({string.Join(',', entry.CurrentValues.Properties.Select(x => { var b = entry.Entity.GetType().GetProperty(x.Name).GetValue(entry.CurrentValues.ToObject(), null); if (b.GetType() == typeof(string)) { return $"'{((string)b).Replace("'", "''")}'"; }; if (b.GetType() != typeof(int)) return $"'{b}'"; return b; }))   });");
            sb.Append(command);
        }
    }
    if (sb.Length > 0) await this.Database.ExecuteSqlCommandAsync(sb.ToString());
    return await base.SaveChangesAsync(cancellationToken);
}

*/