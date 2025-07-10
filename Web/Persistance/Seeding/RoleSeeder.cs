using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class RoleSeeder:IDataSeeder
{
    public static readonly int AdminRoleId=(int)RoleType.Admin;
    public static readonly int LibrarianRoleId=(int)RoleType.Liberian;
    public static readonly int MemberRoleId=(int)RoleType.Member;

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { Id = AdminRoleId, Name = nameof(RoleType.Admin) },
            new RoleEntity { Id = LibrarianRoleId, Name = nameof(RoleType.Liberian) },
            new RoleEntity { Id = MemberRoleId, Name = nameof(RoleType.Member) }
        );
    }
}