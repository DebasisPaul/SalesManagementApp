﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace SalesManagementApp.Migrations
{
    public partial class SeedUserAndRolesData : Migration
    {
        const string ADMIN_ROLE_GUID = "f0235b5a-d703-4e88-94be-b40866862bc2";
        const string SM_ROLE_GUID = "5a1c0998-7bc8-4d08-ab83-03f8034d3ce9";
        const string TL_ROLE_GUID = "b0a153e8-346c-4e45-9a82-12736af3b78a";
        const string SR_ROLE_GUID = "53b33194-140f-468d-989b-f26a357ea5d4";

        const string ADMIN_USER_GUID = "c757b302-2e31-4308-bdc1-a2359fc6cc46";
        const string SM_USER_GUID = "e5c93ff4-090d-4921-8931-f94a276fc741";
        const string TL_USER_GUID = "a77483c8-001f-483b-93cb-d37d03fbcdff";
        const string SR1_USER_GUID = "32c635d3-8fd0-4637-a29b-268d0e0f8516";
        const string SR2_USER_GUID = "d8b50445-d78b-4f1c-9355-5ccfd4386012";
        const string SR3_USER_GUID = "df993c35-eb6a-4608-a161-4aaf91951b09";
        const string SR4_USER_GUID = "48139745-4e18-4d46-b23f-24163e17ca52";


        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var passwordHash = hasher.HashPassword(null, "Password1!");

            AddUser(migrationBuilder, "admin@oexl.com", passwordHash, ADMIN_USER_GUID);

            AddUser(migrationBuilder, "bob.jones@oexl.com", passwordHash, SM_USER_GUID);

            AddUser(migrationBuilder, "henry.andrews@oexl.com", passwordHash, TL_USER_GUID);

            AddUser(migrationBuilder, "olivia.mills@oexl.com", passwordHash, SR1_USER_GUID);
            AddUser(migrationBuilder, "noah.robinson@oexl.com", passwordHash, SR2_USER_GUID);
            AddUser(migrationBuilder, "benjamin.lucas@oexl.com", passwordHash, SR3_USER_GUID);
            AddUser(migrationBuilder, "sarah.henderson@oexl.com", passwordHash, SR4_USER_GUID);

            AddRole(migrationBuilder, "Admin", ADMIN_ROLE_GUID);
            AddRole(migrationBuilder, "SM", SM_ROLE_GUID);
            AddRole(migrationBuilder, "TL", TL_ROLE_GUID);
            AddRole(migrationBuilder, "SR", SR_ROLE_GUID);

            AddUserToRole(migrationBuilder, ADMIN_USER_GUID, ADMIN_ROLE_GUID);
            AddUserToRole(migrationBuilder, SM_USER_GUID, SM_ROLE_GUID);

            AddUserToRole(migrationBuilder, TL_USER_GUID, TL_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR1_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR2_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR3_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR4_USER_GUID, SR_ROLE_GUID);
        }
        private void AddUser(MigrationBuilder migrationBuilder, string email, string passwordHash, string userGuid)
        {
            StringBuilder sb = new StringBuilder();

            string emailCaps = email.ToUpper();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{userGuid}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());
        }
        private void AddRole(MigrationBuilder migrationBuilder, string roleName, string roleGuid)
        {
            string roleNameCaps = roleName.ToUpper();

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{roleGuid}','{roleName}','{roleNameCaps}')");


        }

        private void AddUserToRole(MigrationBuilder migrationBuilder, string userGuid, string roleGuid)
        {
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{userGuid}','{roleGuid}')");


        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RemoveUser(migrationBuilder, ADMIN_USER_GUID, ADMIN_ROLE_GUID);
            RemoveUser(migrationBuilder, SM_USER_GUID, SM_ROLE_GUID);
            RemoveUser(migrationBuilder, TL_USER_GUID, TL_ROLE_GUID);
            RemoveUser(migrationBuilder, SR1_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR2_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR3_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR4_USER_GUID, SR_ROLE_GUID);

            RemoveRole(migrationBuilder, ADMIN_ROLE_GUID);
            RemoveRole(migrationBuilder, SM_ROLE_GUID);
            RemoveRole(migrationBuilder, TL_ROLE_GUID);
            RemoveRole(migrationBuilder, SR_ROLE_GUID);
        }
        private void RemoveUser(MigrationBuilder migrationBuilder, string userGuid, string roleGuid)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{userGuid}' AND RoleId = '{roleGuid}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{userGuid}'");
        }
        private void RemoveRole(MigrationBuilder migrationBuilder, string roleGuid)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{roleGuid}'");
        }
    }
}